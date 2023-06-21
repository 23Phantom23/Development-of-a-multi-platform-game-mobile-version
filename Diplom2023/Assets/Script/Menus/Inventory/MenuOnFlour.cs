using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class MenuOnFlour : MonoBehaviour
{  

    [Header("Menu Items on Flour")]
    public static GameObject menuItemsOnFlour;
    public static Transform content;
    public static GameObject slots;
    public static List<InventorySlot> listsFlour = new List<InventorySlot>();

    [Header("Keep in Inventory")]
    public static int iDSelectedOnFlourInt = 0;
    public GameObject keepButton;

    [Header("Global Info")]
    public static int countItemsOn;

    public void Update()
    {
        if (iDSelectedOnFlourInt != 0 && listsFlour[iDSelectedOnFlourInt - 1].GetComponent<InventorySlot>().item != null)
        {
            keepButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            keepButton.GetComponent<Button>().interactable = false;
        }

        if (content.childCount != countItemsOn)
        {
            listsFlour.Clear();
            DestroyChild();
        }
    }
    public static void UpdateItemsOnFlour(int countItems, List<GameObject> objectsInTrigger)
    {
        countItemsOn = countItems;

        if (countItems % 2 == 0)
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, 10 + (132 * countItems / 2));
        }
        else
        {
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, 10 + (132 * (countItems + 1) / 2));
        }
        listsFlour.Clear();
        float posY = -10;
        for (int i = 0; i < countItems; i++)
        {
            GameObject newObject = Instantiate(slots);
            newObject.transform.SetParent(content);
            newObject.transform.localScale = new Vector3(1f, 1f, 1f);
            newObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
            newObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
            newObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
            newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(122f, 122f);
            newObject.GetComponent<InventorySlot>().IDSlot = i + 1;
            newObject.GetComponent<InventorySlot>().isEmpty = true;

            if (i % 2 == 0)
            {
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-64f, posY);
            }
            else
            {
                newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(64f, posY);
                posY += -132;
            }
            listsFlour.Add(content.GetChild(i).GetComponent<InventorySlot>());
        }

        for (int j = 0; j < content.childCount; j++)
        {
            AddItemToMenuOnFlour(objectsInTrigger[j].GetComponent<Item>().item, objectsInTrigger[j].GetComponent<Item>().amount);
        }
    }

    private static void AddItemToMenuOnFlour(InventoryScripts _item, int _amount)
    {
        /*foreach (InventorySlot slot in listsFlour)
        {
            Debug.Log("CONNECT");
            if (slot.item == _item)
            {
                Debug.Log(slot.item + " == " + _item);
                Debug.Log(slot.amount + " += " + _amount);
                slot.amount += _amount;
                Debug.Log(slot.amount);
                Debug.Log("CONNECTED");
                return;
            }
            Debug.Log("NOCONNECTED");
        }*/
        foreach (InventorySlot slot in listsFlour)
        {
            Debug.Log("ADD");
            if (slot.isEmpty == true)
            {
                Debug.Log("GOADD");
                slot.isEmpty = false;
                slot.item = _item;
                slot.amount = _amount;
                Debug.Log("ADDED");
                break;
            }
            Debug.Log("NOADDED");
        }
    }
    
    public static void DestroyChild()
    {
        int counts = content.childCount;
        for (int i = 0; i < counts; i++)
        {
            Transform chil = content.GetChild(0);
            Destroy(chil.gameObject);
        }
    }


    public void KeepToInventory()
    {
        InventoryManager.AddItem(listsFlour[iDSelectedOnFlourInt - 1].GetComponent<InventorySlot>().item, listsFlour[iDSelectedOnFlourInt - 1].GetComponent<InventorySlot>().amount);

        foreach (GameObject objects in ItemsOnFlourScript.objectsInTrigger)
        {
            if (objects.GetComponent<Item>().item == listsFlour[iDSelectedOnFlourInt - 1].GetComponent<InventorySlot>().item && objects.GetComponent<Item>().amount == listsFlour[iDSelectedOnFlourInt - 1].GetComponent<InventorySlot>().amount)
            {
                if (MenuGame.OnlineOffline) //MenuGame.OnlineOffline if online and !MenuGame.OnlineOffline if offline
                {
                    Destroy(objects);
                    //PhotonNetwork.Destroy(objects);
                }
                else
                {
                    Destroy(objects);
                }
                ItemsOnFlourScript.objectsInTrigger.Remove(objects);
                listsFlour[iDSelectedOnFlourInt - 1].GetComponent<InventorySlot>().item = null;
                //listsFlour.Remove(listsFlour[iDSelectedOnFlourInt - 1]);
                break;
            }
        }
        iDSelectedOnFlourInt = 0;
        ItemsOnFlourScript.CountItemsInFlour--;
    }

    public void KeepAllToInventory()
    {
        foreach (InventorySlot slot in listsFlour)
        {
            InventoryManager.AddItem(slot.item, slot.amount);
        }
        foreach (GameObject items in ItemsOnFlourScript.objectsInTrigger)
        {
            if (MenuGame.OnlineOffline)
            {
                Destroy(items);
                //PhotonNetwork.Destroy(slot);
            }
            else
            {
                Destroy(items);
            }
        }
        ItemsOnFlourScript.objectsInTrigger.Clear();
        countItemsOn = 0;
        iDSelectedOnFlourInt = 0;
        ItemsOnFlourScript.CountItemsInFlour = 0;
        listsFlour.Clear();
        menuItemsOnFlour.SetActive(false);
    }
}
