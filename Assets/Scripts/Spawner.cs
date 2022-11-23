using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public GameObject BombPrefab;

        public float SpawningHeight = 100f;
        public float SpawningRate = 0.1f;

        private bool _isSpawning = false;

        public void StartSpawning()
        {
            _isSpawning = true;
        }

        public void StopSpawning()
        {
            _isSpawning = false;
        }

        private void Start()
        {
            InvokeRepeating("Spawning", 0f, SpawningRate);
        }


        //TODO Bomba może zostać utworzona ponad 2 razy nad jednym kafelkiem
        private void Spawning()
        {
            if (!_isSpawning)
                return;

            var panels = GameObject.FindGameObjectsWithTag("Panel");
            var random = Random.Range(0, panels.Length);

            var spawnPosition = new Vector3(
                panels[random].transform.position.x,
                SpawningHeight,
                panels[random].transform.position.z);

            if (PhotonNetwork.IsConnected)
                PhotonNetwork.InstantiateRoomObject(BombPrefab.name, spawnPosition, Quaternion.identity);
            else
                Instantiate(BombPrefab, spawnPosition, Quaternion.identity);
        }
    }
}