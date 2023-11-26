using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlock_portal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            Destroy(other);
            Destroy(this);
        }
    }
}
