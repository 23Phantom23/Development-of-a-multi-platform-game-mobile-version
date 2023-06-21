using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickBar : MonoBehaviour
{
    public Transform QuickBarPanel;
    public Transform QuickBarPanelInv;
    public static List<InventorySlot> slotsQuickBar = new List<InventorySlot>();
    public static List<InventorySlot> slotsQuickBarInv = new List<InventorySlot>();

    void Start()
    {
        for (int i = 0; i < QuickBarPanel.childCount; i++)
        {
            if (QuickBarPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slotsQuickBar.Add(QuickBarPanel.GetChild(i).GetComponent<InventorySlot>());
                QuickBarPanel.GetChild(i).GetComponent<InventorySlot>().IDSlot = i + 1;
            }
        }
        for (int i = 0; i < QuickBarPanelInv.childCount; i++)
        {
            if (QuickBarPanelInv.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slotsQuickBarInv.Add(QuickBarPanelInv.GetChild(i).GetComponent<InventorySlot>());
                QuickBarPanelInv.GetChild(i).GetComponent<InventorySlot>().IDSlot = i + 1;
            }
        }
    }

    public static bool AddItem(InventoryScripts _item, int _amount)
    {
        foreach (InventorySlot slot in slotsQuickBar)
        {
            Debug.Log("CONNECT To QuickBar");
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.MaximumAmount)
                {
                    slot.amount += _amount;
                    Debug.Log("CONNECTED To QuickBar");
                    return true;
                }
                Debug.Log("NOCONNECTED To QuickBar");
            }
            Debug.Log("NOCONNECTED To QuickBar");
        }
        foreach (InventorySlot slot in slotsQuickBar)
        {
            Debug.Log("ADD To QuickBar");
            if (slot.isEmpty == true)
            {
                slot.isEmpty = false;
                slot.item = _item;
                slot.amount = _amount;
                Debug.Log("ADDED To QuickBar");
                return true;
            }
            Debug.Log("NOADDED To QuickBar");
        }
        return false;
    }
}
