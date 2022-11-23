using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ButtonsScripts
{
    public class PlayButton : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }
    }
}