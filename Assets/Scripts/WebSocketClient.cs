using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void OnMessageDelegate(JToken json);

    public static class WebSocketClient
    {
        private const string Url = "ws://77.55.237.247:6001";

        private static WebSocket _webSocket;

        public static event OnMessageDelegate OnConfigurationReceived;
        public static event OnMessageDelegate OnErrorReceived;
        public static event OnMessageDelegate OnTokenReceived;
        public static event OnMessageDelegate OnConnectedReceived;
        public static event OnMessageDelegate OnGyroscopeReceived;

        public static bool IsConnected = false;

        public static void Start()
        {
            Connect();

            _webSocket.OnMessage += bytes =>
            {
                var json = JToken.Parse(Encoding.UTF8.GetString(bytes));

                if ((int) json["Code"] == WebSocketConfiguration.ConfigurationCode)
                    ConfigurationReceived(json);
                else if ((int) json["Code"] == (int) WebSocketConfiguration.Codes["CODE_ERROR"])
                    ErrorReceived(json);
                else if ((int) json["Code"] == (int) WebSocketConfiguration.Codes["CODE_TOKEN"])
                    TokenReceived(json);
                else if ((int) json["Code"] == (int) WebSocketConfiguration.Codes["CODE_CONNECTED"])
                    ConnectedReceived(json);
                else if ((int) json["Code"] == (int) WebSocketConfiguration.Codes["CODE_GYROSCOPE"])
                    GyroscopeReceived(json);
            };

            if (_webSocket.GetState() == WebSocketState.Open) LoadConfiguration();
        }

        private static void ConfigurationReceived(JToken json)
        {
            HandleConfiguration(json);
            OnConfigurationReceived?.Invoke(json);
        }

        private static void ErrorReceived(JToken json)
        {
            HandleError(json);
            OnErrorReceived?.Invoke(json);
        }

        private static void TokenReceived(JToken json)
        {
            OnTokenReceived?.Invoke(json);
        }

        private static void ConnectedReceived(JToken json)
        {
            IsConnected = true;
            OnConnectedReceived?.Invoke(json);
        }

        private static void GyroscopeReceived(JToken json)
        {
            OnGyroscopeReceived?.Invoke(json);
        }

        public static void GenerateToken()
        {
            Debug.Log(WebSocketConfiguration.Types["TYPE_GAME"]);
            Debug.Log(WebSocketConfiguration.Codes["CODE_TOKEN"]);

            var json = new JObject
            {
                {"Type", WebSocketConfiguration.Types["TYPE_GAME"]},
                {"Code", WebSocketConfiguration.Codes["CODE_TOKEN"]}
            };


            _webSocket.Send(Encoding.UTF8.GetBytes(json.ToString()));
        }

        private static void HandleConfiguration(JToken json)
        {
            var data = json["Data"];

            WebSocketConfiguration.Types = data["Types"];
            WebSocketConfiguration.Codes = data["Codes"];
            WebSocketConfiguration.Settings = data["Settings"];
        }

        private static void HandleError(JToken json)
        {
            Debug.LogError("ERROR");
            Debug.LogError(json["Data"]);
        }

        private static void Connect()
        {
            _webSocket = WebSocketFactory.CreateInstance(Url);
            _webSocket.Connect();

            while (_webSocket.GetState() == WebSocketState.Connecting)
            {
                //Pusta petla powodujaca oczekiwanie na nawizanie polaczenie
            }
        }

        private static void LoadConfiguration()
        {
            var json = new JObject {{"Code", WebSocketConfiguration.ConfigurationCode}};
            _webSocket.Send(Encoding.UTF8.GetBytes(json.ToString()));
        }
    }
}