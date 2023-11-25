using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CubeBehaviour : MonoBehaviour
{
    public string cubeType;
    public Vector3 bridgeCubeDirection;
    [Header("Ligne de Cube")]
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int maxCube = 10;
    [SerializeField] private float timeBtwSpawn = 0.03f;
    private float cubeSize;
    private List<GameObject> cubeList = new List<GameObject>();
    BounceCube _bounceCube;
    GravityCube _gravityCube;
    BoxCollider _boxCollider;
    [SerializeField] private float minTimeBtwStateChange = 0.2f;
    float timeSinceLastStateChange = 0f;
    [SerializeField] private Material[] cubeMaterials;
    

    private IEnumerator Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        cubeSize = _boxCollider.size.x * transform.localScale.x;
        _bounceCube = GetComponent<BounceCube>();
        _bounceCube.enabled = false;
        _gravityCube = GetComponent<GravityCube>();
        _gravityCube.enabled = false;

        timeSinceLastStateChange = minTimeBtwStateChange;
        yield return new WaitForSeconds(1f);
        cubeHit(cubeType, bridgeCubeDirection);
    }


    void Update()
    {
        timeSinceLastStateChange += Time.deltaTime;
    }

    public void cubeHit(string _cubeType = "None", Vector3 hitPosition = new Vector3())
    {
        if(timeSinceLastStateChange < minTimeBtwStateChange) return;
        timeSinceLastStateChange = 0f;
        cubeType = _cubeType;
        if (cubeType != "Ligne") StartCoroutine(DeleteCubes());
        if(cubeType != "Bounce") _bounceCube.isActive = false;
        if(cubeType != "Gravity") _gravityCube.isActive = false;
        
        switch (cubeType)
        {
            case "Bounce":
                GetComponent<MeshRenderer>().material = cubeMaterials[0];
                _bounceCube.isActive = true;
                break;
            case "Ligne":
                //Get the normal of the hit
                GetComponent<MeshRenderer>().material = cubeMaterials[1];
                if (cubeList.Count > 0)
                {
                    StartCoroutine(DeleteCubes());
                    break;
                }
                RaycastHit hit;
                if (Physics.Raycast(hitPosition, transform.position - hitPosition, out hit))
                {
                    Vector3 direction = hit.normal;
                    Debug.Log("SpawnCubes");
                    //Spawn the cubes
                    StartCoroutine(SpawnCubes(direction));
                }
                break;
            case "Gravity":
                GetComponent<MeshRenderer>().material = cubeMaterials[2];
                _gravityCube.isActive = true;
                break;
            default:
                GetComponent<MeshRenderer>().material = cubeMaterials[3];
                break;
        }
    }
    
    private IEnumerator DeleteCubes()
    {
        for (int i = cubeList.Count-1; i > -1; i--)
        {
            yield return new WaitForSeconds(timeBtwSpawn/3f*i);
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
