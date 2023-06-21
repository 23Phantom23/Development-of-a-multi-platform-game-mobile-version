using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Default, Food, WeaponSword, WeaponMagic, WeaponBow}

public class InventoryScripts : ScriptableObject
{
    public string ItemName;
    public ItemType itemType;
    public GameObject itemPrefab;
    public Sprite icon;
    public int MaximumAmount;
    public string ItemDescription;

    public float HealthChenge;
    public float Damage;
}
