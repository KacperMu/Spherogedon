using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts
{
    public class Movement : MonoBehaviourPun
    {
        public Transform Model;
        public Guide Guide;
        private AudioSource _audioS;

        public float GuideDistance;

        public float TurnSpeedFactor = 0.01f;
        public float MovementSpeedFactor = 3f;

        public float Threshold = 0.1f;

        private Animator _animator;

        private void Start()
        {
            _animator = Model.GetComponent<Animator>();
            _audioS = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
                return;

            GuideDistance = Guide.GetTaxicabDistance();
            _animator.SetFloat("Guide Distance", GuideDistance);

            if (GuideDistance < Threshold)
            {
                AudioCtrl.Instance.PlayerWalkSoundStop(_audioS);
                return;
            }

            var directionVector = Guide.transform.position - transform.position;
            var direction = directionVector.normalized;
            var rotation = Quaternion.LookRotation(direction);

            AudioCtrl.Instance.PlayerWalkSoundPlay(_audioS, directionVector.magnitude);

            Model.transform.rotation = Quaternion.Slerp(Model.transform.rotation, rotation, TurnSpeedFactor);
            transform.position += direction * GuideDistance * MovementSpeedFactor * Time.deltaTime;
        }

        public void Disable()
        {
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
                return;

            Guide.Disable();
        }
    }
}