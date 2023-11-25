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

    private void Awake()
    {
        
    }

    public void ReorganizeSelectedColor()
    {
        imageHolders[0].sprite = colorImages[playerMove.currentColor];
        

        int j = 1;

        // Parcourir le tableau à partir de l'indice de départ
        for (int i = playerMove.currentColor + 1; i < imageHolders.Length; i++)
        {
            print("toTransform: " + i);
            if (playerMove.enabledColor[i])
            {
                imageHolders[j].gameObject.SetActive(true);
                imageHolders[j].sprite = colorImages[i];
                j += 1;
                print("true11");
            }
            

            
        }

        // Si rien n'est trouvé, recommencer depuis le début du tableau
        for (int i = 0; i < playerMove.currentColor; i++)
        {
            print("toTransform: " + i);
            if (playerMove.enabledColor[i])
            {
                print("true00");
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
