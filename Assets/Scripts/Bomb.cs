using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bomb : MonoBehaviour
    {
        public float MaxSpeed = 10f;

        private Rigidbody _rigidBody;
        private AudioSource _audioSource;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            AudioCtrl.Instance.BombFlySound(_audioSource);

            if (_rigidBody.velocity.magnitude > MaxSpeed)
                _rigidBody.velocity = _rigidBody.velocity.normalized * MaxSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Panel":
                    HandlePanel(collision.gameObject);
                    break;
                case "Player":
                    HandlePlayer(collision.gameObject);
                    break;
            }
        }

        private void HandlePanel(GameObject panel)
        {
            panel.GetComponent<Panel>().DoDamage();
            //PhotonNetwork.Destroy(gameObject);
            AudioCtrl.Instance.BombExplose(gameObject.transform.position);
            Destroy(gameObject);
        }

        private void HandlePlayer(GameObject player)
        {
            player.GetComponent<PlayerManager>().Kill();
            //PhotonNetwork.Destroy(gameObject);
            AudioCtrl.Instance.BombExplose(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}