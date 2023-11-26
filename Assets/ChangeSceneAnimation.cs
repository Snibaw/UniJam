using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeSceneAnimation : MonoBehaviour
{
    [SerializeField] private List<GameObject> splashs;
    [SerializeField] private Sprite[] splashSprites;
    [SerializeField] private GameObject text;
    // Start is called before the first frame update
    public bool doStartAnimation = true;
    void Start()
    {
        text.SetActive(false);
        if (doStartAnimation)
        {
            StartCoroutine(doAnimation("Close", null));
        }
        else
        {
            for(int i = 0; i < splashs.Count; i++)
            {
                splashs[i].SetActive(false);
            }
        }
        
        
    }

    public IEnumerator doAnimation(string triggerName, string sceneName)
    {
        for(int i = 0; i < splashs.Count; i++)
        {
            splashs[i].SetActive(true);
            splashs[i].GetComponentInChildren<Image>().sprite = splashSprites[i];
            splashs[i].GetComponentInChildren<Animator>().SetTrigger(triggerName);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));
        }
        
        if (triggerName == "Open" && sceneName != null)
        {
            yield return new WaitForSeconds(0.5f);
            text.SetActive(true);
            SceneManager.LoadScene(sceneName);
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < splashs.Count; i++)
        {
            splashs[i].SetActive(false);
        }
    }
}
