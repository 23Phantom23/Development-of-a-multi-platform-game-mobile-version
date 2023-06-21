using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{
    public static Slider sliderHp;

    private void Start()
    {
        sliderHp = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
    }

    public static void UpdateHP(float HP, float MaxHP)
    {
        sliderHp.maxValue = MaxHP;
        sliderHp.value = HP;//vThirdPersonCamera.target.GetComponent<Stats>().GetHp();
    }
}
