using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ButtonsScripts
{
    public class PhnButton : MonoBehaviour
    {
        public void TokenInp()
        {
            SceneManager.LoadScene(5);
        }
    }
}
