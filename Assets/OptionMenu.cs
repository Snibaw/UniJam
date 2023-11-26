using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
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
            PlayerPrefs.SetFloat("sensibility", 0.5f);
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
            if(player != null)
            {
                player.sensibility = 4;
            }
            pause = false;
            optionMenu.SetActive(false);

        }
        else
        {
            optionMenu.SetActive(true);
            pause = true;

        }
    }
}
