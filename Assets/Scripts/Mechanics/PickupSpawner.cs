using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public List<GameObject> coinPrefabs;     // List of coin prefabs to choose from
    public List<GameObject> healthPrefabs;   // List of health prefabs to choose from
    public List<GameObject> powerupPrefabs;  // List of powerup prefabs to choose from
    public List<Transform> spawnLocations;   // List of spawn locations

    private void Start()
    {
        SpawnPickups();
    }

    private void SpawnPickups()
    {
        // Shuffle the spawn locations to randomize the order
        ShuffleList(spawnLocations);

        // Iterate through the spawn locations and spawn pickups
        for (int i = 0; i < spawnLocations.Count; i++)
        {
            // Randomly select a pickup type
            int pickupType = Random.Range(0, 3);

            // Select the appropriate list of prefabs based on the pickup type
            List<GameObject> pickupPrefabs;
            switch (pickupType)
            {
                case 0:
                    pickupPrefabs = coinPrefabs;
                    break;
                case 1:
                    pickupPrefabs = healthPrefabs;
                    break;
                case 2:
                    pickupPrefabs = powerupPrefabs;
                    break;
                default:
                    pickupPrefabs = coinPrefabs;
                    break;
            }

            // Randomly select a pickup prefab from the list
            GameObject pickupPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Count)];

            // Spawn the pickup at the current spawn location
            Instantiate(pickupPrefab, spawnLocations[i].position, Quaternion.identity);
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        // Fisher-Yates shuffle algorithm
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}