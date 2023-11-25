using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject optionsPanel; // Référence au panneau d'options

    void Start()
    {
        // Assurez-vous que le panneau d'options est désactivé au démarrage
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        // Charge la scène du jeu
        SceneManager.LoadScene("3DMenu 1");
    }

    public void OpenOptions()
    {
        // Active le panneau d'options
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void CloseOptions()
    {
        // Désactive le panneau d'options
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
    }

    public void ExitGame()
    {
        // Quitte l'application (seulement disponible dans un build exécutable, pas dans l'éditeur Unity)
        Application.Quit();
    }
}
