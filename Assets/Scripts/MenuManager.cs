using UnityEngine;

namespace Assets.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        private void Awake()
        {
            var menuManagers = GameObject.FindGameObjectsWithTag("MenuManager");

            if (menuManagers.Length > 1)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }
}