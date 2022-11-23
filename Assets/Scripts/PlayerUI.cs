using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PlayerUI : MonoBehaviour
    {
        public Text PlayerNameText;
        public Vector3 ScreenOffset = new Vector3(0f, 30f, 0f);

        private PlayerManager _target;

        public float PlayerHeight = 9.0f;

        private CanvasGroup _canvasGroup;
        private Transform _targetTransform;
        private Renderer _targetRenderer;

        private Vector3 _targetPosition;

        public void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        public void SetTarget(PlayerManager target)
        {
            _target = target;
            PlayerNameText.text = target.photonView.Owner.NickName;

            _targetTransform = _target.GetComponent<Transform>();
            _targetRenderer = _target.GetComponent<Renderer>();
        }

        public void SetAlpha(float alpha)
        {
            var color = PlayerNameText.color;
            color.a = alpha;
            PlayerNameText.color = color;
        }

        private void Update()
        {
            if (_target == null) Destroy(gameObject);
        }

        private void LateUpdate()
        {
            if (_targetRenderer != null) _canvasGroup.alpha = _targetRenderer.isVisible ? 1f : 0f;

            _targetPosition = _targetTransform.position;
            _targetPosition.y += PlayerHeight;
            transform.position = Camera.main.WorldToScreenPoint(_targetPosition) + ScreenOffset;
        }
    }
}