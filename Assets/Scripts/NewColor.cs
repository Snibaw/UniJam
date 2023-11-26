using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewColor : MonoBehaviour
{
    [SerializeField] private int unlockedColor;
    [SerializeField] private Transform center;
    // [SerializeField] private ParticleSystem particle;

    public float rotationSpeed;
    public float floatSpeed;
    public float floatHeight;

    private AudioManager audioManager;

    private float startY;

    private void OnCollisionEnter(Collision collision)
    {
        if (audioManager!=null){audioManager.Play("pick up");}
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        player.enabledColor[unlockedColor] = true;
        player.currentColor = unlockedColor-1;
        player.ChangeColor();
        // particle.Play();
        Destroy(gameObject);
    }
    void Start()
    {
        // Stocke la position Y initiale de l'objet
        startY = transform.position.y;
        audioManager=FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        center.Rotate(Vector3.up, rotationSpeed*Time.deltaTime);

        // Faites flotter l'objet le long de l'axe Y
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
