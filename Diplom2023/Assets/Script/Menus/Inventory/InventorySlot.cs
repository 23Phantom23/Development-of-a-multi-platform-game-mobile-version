using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public int IDSlot;
    public InventoryScripts item;
    public int amount;
    public Image iconGO;
    public TMP_Text amountText;
    public TMP_Text ItemNameText;

    public bool isEmpty = true;

    private void Start()
    {
        isEmpty = true;
        iconGO = transform.GetChild(0).GetComponent<Image>();
        amountText = transform.GetChild(1).GetComponent<TMP_Text>();
        ItemNameText = transform.GetChild(2).GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (item != null)
        {
            isEmpty = false;
            iconGO.color = new Color(1, 1, 1, 1);
            iconGO.sprite = item.icon;

            if (item.MaximumAmount > 1)
            {
                amountText.text = amount.ToString();
            }
            if (item.MaximumAmount == 1)
            {
                amountText.text = "";
            }

            ItemNameText.text = item.ItemName;
        }
        else
        {
            isEmpty = true;
            amount = 0;
            iconGO.color = new Color(0, 0, 0, 0);
            iconGO.sprite = null;
            amountText.text = "";
            ItemNameText.text = "";
        }
    }

    public void SetIcon(Sprite icon)
    {
        iconGO.color = new Color(1, 1, 1, 1);
        iconGO.sprite = icon;
    }

    public void GetIDOnFlourInventory()
    {
        MenuOnFlour.iDSelectedOnFlourInt = IDSlot;
    }

    public void GetIDOnQuickBarInv()
    {
        InventoryManager.idSelectedInv = 0;
        InventoryManager.idSelectedArmor = 0;
        InventoryManager.idSelectedQuickBarInv = IDSlot;
        InventoryManager.UpdatesInfo();
    }

    public void GetIdOnArmorMenu()
    {
        InventoryManager.idSelectedInv = 0;
        InventoryManager.idSelectedQuickBarInv = 0;
        InventoryManager.idSelectedArmor = IDSlot;
        InventoryManager.UpdatesInfo();
    }

    public void GetIDOnInventory()
    {
        InventoryManager.idSelectedQuickBarInv = 0;
        InventoryManager.idSelectedArmor = 0;
        InventoryManager.idSelectedInv = IDSlot;
        InventoryManager.UpdatesInfo();
    }

    public void QuickBarButton()
    {
        if (item != null)
        {
            vThirdPersonCamera.target.GetComponent<Stats>().AddHp(item.HealthChenge);

            amount -= 1;
            if (amount == 0)
            {
                item = null;
            }
        }
    }
}
