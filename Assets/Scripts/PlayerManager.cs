using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerManager : MonoBehaviourPun
    {
        public static GameObject LocalPlayerInstance;

        public GameObject PlayerUIPrefab;
        public SkinnedMeshRenderer Skin;
        public float KillForce = 2000f;

        private Rigidbody _rigidBody;
        private Movement _movement;
        private GameObject _playerUI;
        private CameraWorker _cameraWorker;
        private Transform _spectatorTransform;

        private void Awake()
        {
            if (photonView.IsMine) LocalPlayerInstance = gameObject;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _movement = GetComponent<Movement>();
            _rigidBody = GetComponent<Rigidbody>();

            _spectatorTransform = GameObject.FindGameObjectWithTag("SpectatorTransform").transform;

            _playerUI = Instantiate(PlayerUIPrefab);
            _playerUI.GetComponent<PlayerUI>().SetTarget(this);

            _cameraWorker = gameObject.GetComponent<CameraWorker>();
            if (photonView.IsMine) _cameraWorker.OnStartFollowing();
        }

        public void Kill()
        {
            StartCoroutine(KillTransition());
        }

        private IEnumerator KillTransition()
        {
            _movement.Disable();
            gameObject.layer = LayerMask.NameToLayer("KilledPlayers");

            AudioCtrl.Instance.MuteAllSoundEffect();
            AudioCtrl.Instance.PlayFailMusic();

            _rigidBody.AddForce(0, KillForce, 0);
            transform.rotation = Quaternion.Euler(90f, 0, 0f);

            yield return new WaitForSeconds(1);

            for (float alpha = 1; alpha >= 0; alpha -= 0.05f)
            {
                SetAlpha(alpha);
                _playerUI.GetComponent<PlayerUI>().SetAlpha(alpha);

                yield return new WaitForSeconds(0.05f);
            }

            MoveToSpectator();
        }

        public void MoveToSpectator()
        {
            gameObject.SetActive(false);
            _cameraWorker.IsFollowing = false;
            _cameraWorker.SetTransform(_spectatorTransform);
        }

        private void SetAlpha(float alpha)
        {
            var color = Skin.material.color;
            color.a = alpha;
            Skin.material.color = color;
        }
    }
}