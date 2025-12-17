using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject coinPrefab;

    public float laneDistance = 2f;
    public float spawnDistanceAhead = 30f;

    public float minGapZ = 6f;
    public float maxGapZ = 12f;

    float nextSpawnZ;

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistanceAhead;
    }

    void Update()
    {
        if (!player || !coinPrefab) return;

        if (player.position.z + spawnDistanceAhead >= nextSpawnZ)
        {
            int lane = Random.Range(-1, 2);
            float x = lane * laneDistance;

            Vector3 pos = new Vector3(x, 1.2f, nextSpawnZ);
            Instantiate(coinPrefab, pos, Quaternion.identity);

            nextSpawnZ += Random.Range(minGapZ, maxGapZ);
        }
    }
}
