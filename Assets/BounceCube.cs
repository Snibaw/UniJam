using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCube : MonoBehaviour
{
    public float bounceForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            // Assurez-vous que le joueur a un Rigidbody attaché
            if (playerRb != null)
            {
                // Appliquer une force vers le haut pour le rebondissement
                playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
