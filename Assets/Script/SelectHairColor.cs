using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHairColor : MonoBehaviour
{
    public float redAmount;
    public float greenAmount;
    public float blueAmount;
    public float alphaAmount;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    private Color currentHairColor;

    //grab material from the skin mesh renderer and change the color properties
    public List<SkinnedMeshRenderer> renderList = new List<SkinnedMeshRenderer>();

    public void UpdateSliders(){
        redAmount = redSlider.value;
        greenAmount = greenSlider.value;
        blueAmount = blueSlider.value;
        SetHairColor();
    }

    public void SetHairColor(){
        currentHairColor =  new Color(redAmount, greenAmount, blueAmount, alphaAmount);

        for (int i = 0; i < renderList.Count; i++)
        {
            renderList[i].material.SetColor("_Color", currentHairColor);
        }
    }
}
