using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public GameObject PlayerPrefab;
        public Spawner SpawnerObject;

        public void Start()
        {
            if (PlayerManager.LocalPlayerInstance == null)
                PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity);

            if (!PhotonNetwork.IsConnected || PhotonNetwork.IsMasterClient)
                SpawnerObject.StartSpawning();

            var menuManager = GameObject.FindGameObjectWithTag("MenuManager");
            Destroy(menuManager);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.IsMasterClient)
                SpawnerObject.StartSpawning();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();

            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Disconnect();
        }
    }
}