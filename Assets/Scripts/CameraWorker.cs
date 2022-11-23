using UnityEngine;

namespace Assets.Scripts
{
    public class CameraWorker : MonoBehaviour
    {
        public Vector3 CameraPosition = Vector3.zero;
        public Vector3 CenterOffset = Vector3.zero;

        public bool FollowOnStart = false;

        private Transform _cameraTransform;
        public bool IsFollowing;

        private void Start()
        {
            if (FollowOnStart) OnStartFollowing();
        }

        private void LateUpdate()
        {
            if (_cameraTransform == null && IsFollowing) OnStartFollowing();
            if (IsFollowing) Follow();
        }

        public void OnStartFollowing()
        {
            _cameraTransform = Camera.main.transform;
            IsFollowing = true;
        }

        private void Follow()
        {
            _cameraTransform.position = transform.position + CameraPosition;
            _cameraTransform.LookAt(transform.position + CenterOffset);
        }

        public void SetTransform(Transform newTransform)
        {
            _cameraTransform.position = newTransform.position;
            _cameraTransform.rotation = newTransform.rotation;
        }
    }
}