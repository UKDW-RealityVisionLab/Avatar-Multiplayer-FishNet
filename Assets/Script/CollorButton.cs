using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollorButton : MonoBehaviour
{
    public SelectHairColor selectHairColor;

    [Header("Color Values")]
    public float redAmount;
    public float greenAmount;
    public float blueAmount;
    public float alphaAmount;

    [Header("Color Sliders")]    
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Image colorImage;

    private void Awake(){
        colorImage = GetComponent<Image>();
        redAmount = colorImage.color.r;
        greenAmount = colorImage.color.g;
        blueAmount = colorImage.color.b;
    }

    public void SetSliderValuesToImageColor(){
        redSlider.value = redAmount;
        greenSlider.value = greenAmount;
        blueSlider.value = blueAmount;

        selectHairColor.redAmount = redAmount;
        selectHairColor.greenAmount = greenAmount;
        selectHairColor.blueAmount = blueAmount;
        selectHairColor.SetHairColor();
    }
}
