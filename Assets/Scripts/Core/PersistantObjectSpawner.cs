using UnityEngine;

namespace RPG.Core
{
    public class PersistantObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistantObjectPrefab;

        static bool hasSpawned = false;

        private void Awake()
        {
            if(hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistantObject = Instantiate(persistantObjectPrefab);
            DontDestroyOnLoad(persistantObject);
        }
    }
}
