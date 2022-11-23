using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ButtonsScripts
{
    public class MsButton : MonoBehaviour
    {
        public void LoadGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}
