using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class BounceCube : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private float freezeTime;
    private AudioManager audioManager;

    public bool isActive = false;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

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
                CameraShaker.Instance.ShakeOnce(1.2f, 0.5f, 0.1f, 1f);
                // Bounce vers le haut
                Vector3 bounceDirection = -Physics.gravity;

                // Appliquer une force oppos�e � la gravit�
                playerRb.AddForce(bounceDirection.normalized * bounceForce, ForceMode.Impulse);

                playerMove.freezeTimer=freezeTime;
                if (audioManager!=null){audioManager.Play("bounce");}
            }
        }
    }

    
}
