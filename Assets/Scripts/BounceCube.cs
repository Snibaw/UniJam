using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCube : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private float freezeTime;

    public bool isActive = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            PlayerMovement playerMove = collision.gameObject.GetComponent<PlayerMovement>();

            // Assurez-vous que le joueur a un Rigidbody attach�
            if (playerMove != null)
            {
                // Bounce vers le haut
                Vector3 bounceDirection = Vector3.up;

                // Appliquer une force oppos�e � la direction de la normale
                playerRb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

                playerMove.freezeTimer=freezeTime;

            }
        }
    }

    
}
