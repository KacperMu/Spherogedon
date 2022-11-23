using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public GameObject ControlPanel;
        public GameObject ProgressLabel;

        public byte MaxPlayersPerRoom = 10;

        private const string GameVersion = "2";

        private bool _isConnecting;


        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "ru";
        }

        private void Start()
        {
            ProgressLabel.SetActive(false);
            ControlPanel.SetActive(true);
        }

        public void Connect()
        {
            ProgressLabel.SetActive(true);
            ControlPanel.SetActive(false);

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                _isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = GameVersion;
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN: OnConnectedToMaster() was called by PUN");

            if (_isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                _isConnecting = false;
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            _isConnecting = false;

            ProgressLabel.SetActive(false);
            ControlPanel.SetActive(true);

            Debug.LogWarningFormat("PUN: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log(
                "PUN: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = MaxPlayersPerRoom});
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN: OnJoinedRoom() called by PUN. Now this client is in a room.");
            UpdatePlayerCountText();
            PhotonNetwork.LoadLevel("Game"); //Aby grac samemu, trzeba to odkomentowac
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            UpdatePlayerCountText();

            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
                PhotonNetwork.LoadLevel("Game");
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            UpdatePlayerCountText();
        }

        private void UpdatePlayerCountText()
        {
            ProgressLabel.GetComponent<Text>().text =
                $"Oczekiwanie na graczy {PhotonNetwork.CurrentRoom.PlayerCount}/{MaxPlayersPerRoom}";
        }
    }
}