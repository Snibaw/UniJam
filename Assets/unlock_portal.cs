using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlock_portal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        print("1");
        if (other.CompareTag("Sphere"))
        {
            print("2");
            Destroy(other);
            Destroy(this);
        }
    }
}
