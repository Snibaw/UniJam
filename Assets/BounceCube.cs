using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCube : MonoBehaviour
{
    public float bounceForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            // Assurez-vous que le joueur a un Rigidbody attaché
            if (playerRb != null)
            {
                // Obtenez la normale de la collision (direction du rebond)
                Vector3 bounceDirection = collision.contacts[0].normal;

                // Appliquer une force opposée à la direction de la normale
                playerRb.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
