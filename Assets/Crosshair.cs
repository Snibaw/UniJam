using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Sprite[] crosshairSprites;
    private Image crosshairImage;

    private void Start()
    {
        crosshairImage = GetComponent<Image>();
    }
    public void ChangeCrosshair(int colorIndex)
    {
        crosshairImage.sprite = crosshairSprites[colorIndex];
    }
}
