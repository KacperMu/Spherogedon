using UnityEngine;

namespace Assets.Scripts
{
    public class Panel : MonoBehaviour
    {
        public GameObject Model;
        public Material FullMaterial;
        public Material DamageMaterial;

        private bool _isDamaged = false;

        private void Start()
        {
            Model.GetComponent<Renderer>().material = FullMaterial;
        }

        public void DoDamage()
        {
            AudioCtrl.Instance.PanelDamagedSoundPlay(Model.transform.position); 
            if (_isDamaged)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                Model.GetComponent<Renderer>().material = DamageMaterial;
                _isDamaged = true;
            }
        }
    }
}