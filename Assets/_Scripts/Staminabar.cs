using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    public void SetStamina(float stamina, float maxStamina) {
        slider.gameObject.SetActive(stamina < maxStamina);
        slider.value = stamina;
        slider.maxValue = maxStamina;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }

    //void Update()
    //{
    //    slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    //}
}
