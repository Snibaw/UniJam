using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigneDeCube : MonoBehaviour
{
    public CubeBehaviour cubeParent;
    [SerializeField] private Material cubeMaterial;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = cubeMaterial;
    }

    public void DeleteCubeFromChild()
    {
        StartCoroutine(cubeParent.DeleteCubes());
    }
    
}
