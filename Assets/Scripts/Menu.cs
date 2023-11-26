using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject optionsPanel; // R�f�rence au panneau d'options
    public GameObject mainMenu;
    private AudioManager audioManager;

    void Start()
    {
        // Assurez-vous que le panneau d'options est d�sactiv� au d�marrage
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager!=null){audioManager.Play("intro");}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenOptions();
        }
    }

    public void StartGame()
    {
        // Charge la sc�ne du jeu
        SceneManager.LoadScene("3D Menu 1");
    }

    public void OpenOptions()
    {
        // Active le panneau d'options
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void Back()
    {
        optionsPanel.SetActive(false);
    }

    
    public void ExitGame()
    {
        // Quitte l'application (seulement disponible dans un build ex�cutable, pas dans l'�diteur Unity)
        Application.Quit();
    }
}
