using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Damage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WeaponEnemy"))
        {
            GetComponent<Stats>().AddHp(-10);
        }
    }
}
