using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCube : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    public bool isActive = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            // Assurez-vous que le joueur a un Rigidbody attach�
            if (playerRb != null)
            {
                // Obtenez la normale de la collision (direction du rebond)
                Vector3 bounceDirection = collision.contacts[0].normal;

                // Appliquer une force oppos�e � la direction de la normale
                playerRb.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
