using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitScreen : MonoBehaviour
{
    private bool canSkip = false;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        canSkip = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSkip)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
