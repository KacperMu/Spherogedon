using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        private const string PlayerNamePrefKey = "PlayerName"; //TODO MP Wynieść do innego skryptu jako static

        private void Start()
        {
            var defaultName = string.Empty;
            var inputField = GetComponent<InputField>();

            if (PlayerPrefs.HasKey(PlayerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
                inputField.text = defaultName;
            }

            PhotonNetwork.NickName = defaultName;
        }

        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }

            PhotonNetwork.NickName = value;
            PlayerPrefs.SetString(PlayerNamePrefKey, value);
        }
    }
}