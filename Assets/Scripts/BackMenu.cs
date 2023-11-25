using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Référence au panneau d'options

    public void OpenOptions()
    {
        // Active le panneau d'options
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
            gameObject.SetActive(false);

        }
    }
}
