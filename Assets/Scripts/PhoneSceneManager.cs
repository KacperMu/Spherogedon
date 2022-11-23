using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PhoneSceneManager : MonoBehaviour
    {
        public Text TokenText;

        private string _token = "Generowanie...";
        private bool _connected = false;


        private void Start()
        {
            WebSocketClient.Start();
            WebSocketClient.OnConfigurationReceived += json => { WebSocketClient.GenerateToken(); };

            WebSocketClient.OnTokenReceived += json =>
            {
                Debug.Log(json["Data"].ToString());
                _token = json["Data"].ToString();
            };

            WebSocketClient.OnConnectedReceived += json =>
            {
                Debug.Log("CONNECTED");
                _connected = true;
            };
        }

        private void Update()
        {
            TokenText.text =              _token;

            if (_connected)
                SceneManager.LoadScene(2);
        }
    }
}