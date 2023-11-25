using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewColor : MonoBehaviour
{
    [SerializeField] private int unlockedColor;
    [SerializeField] private Transform center;

    public float rotationSpeed;
    public float floatSpeed;
    public float floatHeight;

    private float startY;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        player.enabledColor[unlockedColor] = true;
        player.currentColor = unlockedColor-1;
        player.ChangeColor();
        Destroy(gameObject);
    }
    void Start()
    {
        // Stocke la position Y initiale de l'objet
        startY = transform.position.y;
    }

    private void Update()
    {
        center.Rotate(Vector3.up, rotationSpeed*Time.deltaTime);

        // Faites flotter l'objet le long de l'axe Y
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
