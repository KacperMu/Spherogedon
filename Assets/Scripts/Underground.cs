using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts
{
    public class Underground : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "Bomb":
                    HandleBomb(other.gameObject);
                    break;
                case "Panel":
                    HandlePanel(other.gameObject);
                    break;
                case "Player":
                    HandlePlayer(other.gameObject);
                    break;
            }
        }

        private void HandleBomb(GameObject bomb)
        {
            PhotonNetwork.Destroy(bomb);
        }

        private void HandlePanel(GameObject panel)
        {
            Destroy(panel);
        }

        private void HandlePlayer(GameObject player)
        {
            player.GetComponent<PlayerManager>().MoveToSpectator();
        }
    }
}