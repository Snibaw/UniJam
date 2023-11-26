using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public float respawnYMinThreshold = -5f;
    public float respawnYMaxThreshold = 22f;
    public Transform respawnPoint; // Set this to the position where you want the player to respawn.
    public GameObject player;

    void Update()
    {
        // Check the player's Y-coordinate.
        if (player.transform.position.y < respawnYMinThreshold || player.transform.position.y > respawnYMaxThreshold)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    public IEnumerator RespawnPlayer()
    {
        StartCoroutine(GameObject.Find("UIPaint").GetComponent<ChangeSceneAnimation>().doAnimation("Open", null));
        yield return new WaitForSeconds(.5f);
        // Set the player's position to the respawn point.
        player.transform.position = respawnPoint.position;
        // reset gravity
        Physics.gravity = new Vector3(0, -10.0F, 0);
        Camera.main.transform.parent.rotation = Quaternion.Euler(0f, 0f, 0f);
        StartCoroutine(GameObject.Find("UIPaint").GetComponent<ChangeSceneAnimation>().doAnimation("Close", null));

        
    }
}