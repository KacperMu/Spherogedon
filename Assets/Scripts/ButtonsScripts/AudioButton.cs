using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ButtonsScripts
{
    public class AudioButton : MonoBehaviour
    {
        public Sprite MutedSprite;
        public Sprite UnMutedSprite;

        private void Start()
        {
            if (Settings.IsMuted) Mute();
            else UnMute();
        }

        public void ToggleMute()
        {
            if (Settings.IsMuted) UnMute();
            else Mute();
        }

        private void Mute()
        {
            Settings.IsMuted = true;
            AudioListener.volume = 0.0f;
            GetComponent<Image>().sprite = MutedSprite;
        }

        private void UnMute()
        {
            Settings.IsMuted = false;
            AudioListener.volume = 1.0f;
            GetComponent<Image>().sprite = UnMutedSprite;
        }
    }
}