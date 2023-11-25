using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    public string sceneToLoad; // Drag and drop the scene asset in the Inspector
    public Transform player;

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(player);
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        if (sceneToLoad != null)
        {
            Physics.gravity = new Vector3(0, -10.0F, 0);
            StartCoroutine(GameObject.Find("UIPaint").GetComponent<ChangeSceneAnimation>().doAnimation("Open", sceneToLoad));
        }
        else
        {
            Debug.LogWarning("Scene to load is not assigned in the Inspector.");
        }
    }
}