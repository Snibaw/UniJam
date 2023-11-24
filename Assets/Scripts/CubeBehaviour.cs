using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public string cubeType;
    [Header("Ligne de Cube")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int maxCube = 10;
    [SerializeField] private float timeBtwSpawn = 0.03f;
    private float cubeSize;
    [SerializeField] private bool isHit = false;
    private List<GameObject> cubeList = new List<GameObject>();
    BounceCube _bounceCube;
    

    private void Start()
    {
        cubeSize = transform.localScale.x;
        _bounceCube = GetComponent<BounceCube>();
        _bounceCube.enabled = false;
    }

    public void cubeHit(Vector3 hitPosition = new Vector3())
    {
        switch (cubeType)
        {
            case "Ligne":
                //Get the normal of the hit
                if (isHit)
                {
                    StartCoroutine(DeleteCubes());
                    isHit = false;
                    break;
                }
                //Is not hit
                isHit = true;
                RaycastHit hit;
                if (Physics.Raycast(hitPosition, transform.position - hitPosition, out hit))
                {
                    Vector3 direction = hit.normal;
                    
                    //Spawn the cubes
                    StartCoroutine(SpawnCubes(direction));
                }
                break;
            case "Bounce":
                _bounceCube.enabled = true;
                break;
            default:
                break;
        }
    }
    private IEnumerator DeleteCubes()
    {
        for (int i = cubeList.Count-1; i > -1; i--)
        {
            yield return new WaitForSeconds(timeBtwSpawn/5f*i);
            Destroy(cubeList[i]);
        }
        cubeList.Clear();
    }
    private IEnumerator SpawnCubes(Vector3 direction)
    {
        for (int i = 0; i < maxCube; i++)
        {
            yield return new WaitForSeconds(timeBtwSpawn*i);
            Vector3 spawnPosition = transform.position + direction.normalized * cubeSize * (i+1);
            //Raycast to check if there is an obstacle in the way
            RaycastHit hit2;
            if(Physics.Raycast(spawnPosition - (0.5f * cubeSize * direction), direction.normalized ,out hit2, cubeSize))
            {
                spawnPosition = hit2.point - direction.normalized * cubeSize/2;
                SpawnCube(spawnPosition);
                break;
            }
            SpawnCube(spawnPosition);
        }
    }
    private void SpawnCube(Vector3 spawnPosition)
    {
        GameObject obj = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        obj.transform.parent = transform.parent;
        cubeList.Add(obj);
        
    }
}
