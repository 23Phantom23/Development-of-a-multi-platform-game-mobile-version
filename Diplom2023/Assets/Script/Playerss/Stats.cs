using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [Header("Stats Player")]
    public float Hp = 100f;
    public float HpMax = 100f;
    public float RegenHp = 0;   //Значення потрібного в секунду ділити на 50 (1 хп реген в 1 сек = 1/50 = 0.02)
    public float Mp = 100;
    public float MpMax = 100;
    public float RegenMp = 0;
    public float Sp = 100;
    public float SpMax = 100;
    public float RegenSp = 0;
    public float Damage = 10;

    [Header("LVL")]
    public int LVL = 1;
    public float XP = 0;

    [Header("Skills")]
    public bool skillsOne = false;
    public bool skillsTwo = false;
    public bool skillsThree = false;
    public bool skillsFour = false;
    public bool skillsFive = false;

    public void AddDamage(float AddDamage)
    {
        Debug.Log("Add Damage before = " + Damage + " + " + AddDamage);
        Damage += AddDamage;
        Debug.Log("Add Damage after = " + Damage);
    }
    public void SetDamage(float AddDamage)
    {
        Debug.Log("Set Damage before = " + Damage + " + " + AddDamage);
        Damage = AddDamage;
        Debug.Log("Set Damage after = " + Damage);
    }
    public float GetDamage()
    {
        return Damage;
    }

    public void AddMaxHp(float AddMaxHp)
    {
        Debug.Log("Add MaxHP before = " + HpMax + " + " + AddMaxHp);
        HpMax += AddMaxHp;
        SliderHP.UpdateHP(Hp, HpMax);
        Debug.Log("Add MaxHP after = " + HpMax);
    }
    public void SetMaxHp(float AddMaxHp)
    {
        Debug.Log("Set MaxHP before = " + HpMax + " = " + AddMaxHp);
        HpMax = AddMaxHp;
        SliderHP.UpdateHP(Hp, HpMax);
        Debug.Log("Set MaxHP after = " + HpMax);
    }
    public float GetMaxHp()
    {
        return HpMax;
    }

    public void AddHp(float AddHp)
    {
        Debug.Log("Add HP before = " + Hp + " + " + AddHp);
        Hp += AddHp;
        if (Hp > HpMax)
        {
            Hp = HpMax;
        }
        SliderHP.UpdateHP(Hp, HpMax);
        Debug.Log("Add HP after = " + Hp);
    }
    public void SetHp(float AddHp)
    {
        Debug.Log("Set HP before = " + Hp + " + " + AddHp);
        Hp = AddHp;
        SliderHP.UpdateHP(Hp, HpMax);
        Debug.Log("Set HP after = " + Hp);
    }
    public float GetHp()
    {
        return Hp;
    }

    public void AddRegenHp(float AddRegenHp)
    {
        Debug.Log("Add RegenHp before = " + RegenHp + " + " + AddRegenHp);
        RegenHp += AddRegenHp;
        SliderHP.UpdateHP(Hp, HpMax);
        Debug.Log("Add RegenHp after = " + RegenHp);
    }
    public void SetRegenHp(float AddRegenHp)
    {
        Debug.Log("Set RegenHp before = " + RegenHp + " + " + AddRegenHp);
        RegenHp = AddRegenHp;
        SliderHP.UpdateHP(Hp, HpMax);
        Debug.Log("Set RegenHp after = " + RegenHp);
    }
    public float GetRegenHp()
    {
        return RegenHp;
    }

    void Start()
    {
        //vThirdPersonCamera.target
        SliderHP.UpdateHP(Hp, HpMax);
    }
    private void FixedUpdate()
    {
        if (RegenHp > 0 && Hp < HpMax)
        {
            AddHp(RegenHp);
        }
    }
}
