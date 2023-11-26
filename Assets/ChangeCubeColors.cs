using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCubeColors : MonoBehaviour
{
    List<GameObject> cubes = new List<GameObject>();
    CubeBehaviour cubeBehaviour;
    //list of indexes of cubes that have been changed
    List<int> changedCubes = new List<int>();
    public ParticleSystem particleSystem;
    
    private IEnumerator Start()
    {
        //find all cubes
        GameObject[] cubeTemps = GameObject.FindGameObjectsWithTag("Cube");
        foreach (GameObject cube in cubeTemps)
        {
            cubes.Add(cube);
        }
        
        cubeBehaviour = FindObjectOfType<CubeBehaviour>();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ChangeCubeColor());
        StartCoroutine(LoadTargetScene());
    }

    
    public IEnumerator ChangeCubeColor()
    {
        int randomIndex;
        int randomMaterial;
        for(int i=0; i<cubes.Count; i++)
        {
            randomIndex = UnityEngine.Random.Range(0, cubes.Count);
            randomMaterial = UnityEngine.Random.Range(0, cubeBehaviour.cubeMaterials.Length-1);
            cubes[randomIndex].GetComponent<MeshRenderer>().material = cubeBehaviour.cubeMaterials[randomMaterial];
            //instantiate particle system
            ParticleSystem particleSystemTemp = Instantiate(particleSystem, cubes[randomIndex].transform.position, Quaternion.identity);
            particleSystemTemp.Play();
            cubes.RemoveAt(randomIndex);
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(10f);
        
        
    }


    private IEnumerator LoadTargetScene()
    {
        yield return new WaitForSeconds(5f);
        Physics.gravity = new Vector3(0, -10.0F, 0);
        StartCoroutine(GameObject.Find("UIPaint").GetComponent<ChangeSceneAnimation>().doAnimation("Open", "Credits"));
    }
    
}
