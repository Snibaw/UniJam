using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    public string sceneToLoad; // Drag and drop the scene asset in the Inspector
    private Transform player;
    private GameObject particleSystem;
    private AudioManager audioManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager=FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(player);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < 50; i++)
            {
                GetComponent<ChangeCubeColors>().ChangeCubeColor();
            }
            StartCoroutine(LoadTargetScene());
        }
    }

    private IEnumerator LoadTargetScene()
    {
        yield return new WaitForSeconds(5f);

        if (sceneToLoad != null)
        {
            Physics.gravity = new Vector3(0, -10.0F, 0);
            if (audioManager!=null){audioManager.Play("splash");}
            StartCoroutine(GameObject.Find("UIPaint").GetComponent<ChangeSceneAnimation>().doAnimation("Open", sceneToLoad));
        }
        else
        {
            Debug.LogWarning("Scene to load is not assigned in the Inspector.");
        }
    }
}