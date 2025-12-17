using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    public Transform player;
    public Transform[] tiles;      // 3 tiles
    public float tileLength = 250f; // set from Bounds size Z

    void Update()
    {
        if (player == null || tiles == null || tiles.Length == 0) return;

        // Find the tile that is farthest behind the player
        Transform oldest = tiles[0];
        foreach (var t in tiles)
            if (t.position.z < oldest.position.z) oldest = t;

        // If that tile is far behind the player, move it to the front
        if (player.position.z - oldest.position.z > tileLength)
        {
            // Find the front-most tile
            Transform front = tiles[0];
            foreach (var t in tiles)
                if (t.position.z > front.position.z) front = t;

            oldest.position = new Vector3(oldest.position.x, oldest.position.y, front.position.z + tileLength);
        }
    }
}
