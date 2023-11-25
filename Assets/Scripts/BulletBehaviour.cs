using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.CompareTag("Cube"))
        {
            other.gameObject.GetComponent<CubeBehaviour>().cubeHit(transform.position);
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        
    }
}