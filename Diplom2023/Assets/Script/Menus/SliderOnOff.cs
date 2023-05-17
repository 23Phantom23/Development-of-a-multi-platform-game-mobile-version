using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOnOff : MonoBehaviour
{
    [SerializeField] private Slider sliders;
    [SerializeField] private Button buttons;

    public void SliderButton()
    {
        if (sliders.value == 0)
        {
            sliders.value = 1;
        }
        else
        {
            sliders.value = 0;
        }
    }
}
