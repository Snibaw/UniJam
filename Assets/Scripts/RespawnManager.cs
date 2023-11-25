using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public float respawnYThreshold = -2f;
    public Transform respawnPoint; // Set this to the position where you want the player to respawn.

    void Update()
    {
        // Check the player's Y-coordinate.
        if (transform.position.y < respawnYThreshold)
        {
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        // Set the player's position to the respawn point.
        transform.position = respawnPoint.position;
    }
}