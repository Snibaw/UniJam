using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject mainMenu;
    [HideInInspector] public static bool pause;
    private PlayerMovement player;


    [SerializeField] private Slider sensibilitySlider;
    [Range(0.5f, 8f)]
    public float sensibility;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        player = FindObjectOfType<PlayerMovement>();
        if (!PlayerPrefs.HasKey("sensibility"))
        {
            PlayerPrefs.SetFloat("sensibility", 4f);
            LoadSensibility();

        }
        else
        {
            LoadSensibility();
        }

    }

    private void OnEnable()
    {
        optionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchPause();
        }
    }

    public void ChangeSensibility()
    {
        sensibility = sensibilitySlider.value;
        SaveSensibility();
    }

    private void LoadSensibility()
    {
        sensibilitySlider.value = PlayerPrefs.GetFloat("sensibility");
    }

    private void SaveSensibility()
    {
        PlayerPrefs.SetFloat("sensibility", sensibilitySlider.value);
    }

    public void SwitchPause()
    {
        if (pause)
        {

            if (player != null)
            {
                player.sensibility = PlayerPrefs.GetFloat("sensibility"); ;
                Cursor.lockState = CursorLockMode.Locked;
            }
            pause = false;
            Time.timeScale = 1;
            optionMenu.SetActive(false);

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            optionMenu.SetActive(true);
            Time.timeScale = 0;
            pause = true; 


        }
    }

    public void CloseMenu()
    {
        optionMenu.SetActive(false);
    }
    public void GoToFirstScene()
    {
        Time.timeScale = 1;
        pause = false;
        optionMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }

}
