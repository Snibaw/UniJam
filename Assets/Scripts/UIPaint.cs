using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPaint : MonoBehaviour
{
    [SerializeField] private Image[] imageHolders;
    [SerializeField] private Sprite[] colorImages;
    [SerializeField] private PlayerMovement playerMove;
    // Start is called before the first frame update
    void Start()
    {
        ReorganizeSelectedColor();
    }

    public void ReorganizeSelectedColor()
    {
        imageHolders[0].sprite = colorImages[playerMove.currentColor];
        

        int j = 1;

        // Parcourir le tableau � partir de l'indice de d�part
        for (int i = playerMove.currentColor + 1; i < imageHolders.Length; i++)
        {
            if (playerMove.enabledColor[i])
            {
                imageHolders[j].gameObject.SetActive(true);
                imageHolders[j].sprite = colorImages[i];
                j += 1;
            }
        }

        // Si rien n'est trouv�, recommencer depuis le d�but du tableau
        for (int i = 0; i < playerMove.currentColor; i++)
        {
            if (playerMove.enabledColor[i])
            {
                imageHolders[j].gameObject.SetActive(true);
                imageHolders[j].sprite = colorImages[i];
                j += 1;
            }
        }
        while(j < colorImages.Length)
        {
            imageHolders[j].gameObject.SetActive(false);
            j++;
        }
    }
}
