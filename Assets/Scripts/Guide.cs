using System;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts
{
    public class Guide : MonoBehaviourPun
    {
        public LayerMask MouseLayerMask;

        public float MouseXFactor = 40f;
        public float MouseYFactor = 40f;

        public float GyroscopeX = 0.0f;
        public float GyroscopeY = 0.0f;

        public float GyroscopeXFactor = 2.0f;
        public float GyroscopeYFactor = 2.0f;

        public float XLimit = 1.0f;
        public float YLimit = 1.0f;

        private bool _canMove = true;

        public Vector3 Position;

        private enum InputSource
        {
            Mouse,
            Phone
        }

        private void Start()
        {
            WebSocketClient.OnGyroscopeReceived += json =>
            {
                GyroscopeX = (float) json["Data"]["X"];
                GyroscopeY = (float) json["Data"]["Y"];
            };
        }

        private void Update()
        {
            //Sprwadzanie IsConnected pozwala na testowanie skryptu bez konieczności łączenie się z serwerem.
            //Sprawdzanie IsMine pozwala na wykonywanie skryptu tylko przez nasz komputer.
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
                return;

            CalculatePosition(WebSocketClient.IsConnected ? InputSource.Phone : InputSource.Mouse);
            LimitPosition();

            transform.localPosition = Position;
        }

        private void CalculatePosition(InputSource inputSource)
        {
            switch (inputSource)
            {
                case InputSource.Mouse:
                    CalculatePositionByMouseInput();
                    break;

                case InputSource.Phone:
                    CalculatePositionByPhoneInput();
                    break;
            }
        }

        private void CalculatePositionByMouseInput()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, MouseLayerMask)) return;

            var direction = hit.point - transform.position;
            Position = new Vector3(direction.x * MouseXFactor, 0, direction.z * MouseYFactor);
        }

        private void CalculatePositionByPhoneInput()
        {
            Position = new Vector3
            {
                x = GyroscopeX * GyroscopeXFactor,
                z = GyroscopeY * GyroscopeYFactor
            };
        }

        private void LimitPosition()
        {
            if (Position.x > XLimit)
                Position.x = XLimit;

            if (Position.x < -XLimit)
                Position.x = -XLimit;

            if (Position.z > YLimit)
                Position.z = YLimit;

            if (Position.z < -YLimit)
                Position.z = -YLimit;
        }

        public float GetTaxicabDistance()
        {
            if (!_canMove)
                return 0;

            var x = transform.localPosition.x;
            var z = transform.localPosition.z;

            return Math.Abs(x - z) + Math.Abs(x + z);
        }

        public void Disable()
        {
            if (PhotonNetwork.IsConnected && !photonView.IsMine)
                return;

            _canMove = false;
        }
    }
}