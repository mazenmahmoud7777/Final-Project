using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject[] obstaclePrefabs; // 0 = Tall, 1 = Low

    [Header("Lanes")]
    public float laneDistance = 2f; // must match your lane spacing

    [Header("Spawn Settings")]
    public float spawnDistanceAhead = 40f; // how far in front of player
    public float minGapZ = 12f;            // minimum distance between obstacles
    public float obstacleY = 0f;           // we’ll set Y per prefab below

    private float nextSpawnZ;

    void Start()
    {
        if (player != null)
            nextSpawnZ = player.position.z + spawnDistanceAhead;
    }

    void Update()
    {
        if (player == null || obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        // Keep spawning as the player moves forward
        if (player.position.z + spawnDistanceAhead >= nextSpawnZ)
        {
            SpawnOne();
            nextSpawnZ += Random.Range(minGapZ, minGapZ + 10f); // random spacing
        }
    }

    void SpawnOne()
    {
        // pick lane: -1, 0, 1
        int lane = Random.Range(-1, 2);
        float x = lane * laneDistance;

        // pick obstacle type
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // choose Y based on obstacle type (low sits lower, tall sits higher)
        float y = 0f;
        if (prefab.name.ToLower().Contains("tall")) y = 1.5f; // tall center
        else y = 0.4f; // low center

        Vector3 pos = new Vector3(x, y, nextSpawnZ);

        Instantiate(prefab, pos, prefab.transform.rotation);
    }
}
