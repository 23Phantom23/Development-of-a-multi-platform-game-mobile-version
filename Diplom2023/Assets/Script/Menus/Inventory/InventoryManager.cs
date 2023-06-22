using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public static GameObject menuInventory; //PC ne vse

    public static int idSelectedInv = 0;
    public static int idSelectedQuickBarInv = 0;
    public static int idSelectedArmor = 0;
    public Transform inventoryPanel;
    public Transform armorPanel;
    public static List<InventorySlot> slots = new List<InventorySlot>();
    public static List<InventorySlot> slotsArmor = new List<InventorySlot>();
    public Button DropItemButton;
    public Button DeleteItemButton;
    public static GameObject InteractiveButton;
    public Transform playerYou;
    public static Transform rHandWeaponSlot;
    public static GameObject Mapa;
    public GameObject weapon;

    void Start()
    {
        idSelectedInv = 0;
        idSelectedQuickBarInv = 0;
        idSelectedArmor = 0;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
                inventoryPanel.GetChild(i).GetComponent<InventorySlot>().IDSlot = i + 1;
            }
        }
        for (int i = 0; i < armorPanel.childCount; i++)
        {
            if (armorPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slotsArmor.Add(armorPanel.GetChild(i).GetComponent<InventorySlot>());
                armorPanel.GetChild(i).GetComponent<InventorySlot>().IDSlot = i + 1;
            }
        }

        playerYou = GameObject.Find(PlayerManager.YouPerson + "(Clone)").transform;
        InteractiveButton.GetComponentInChildren<TMP_Text>().text = "";
    }

    private void Update()
    {
        if (idSelectedInv != 0 && slots[idSelectedInv - 1].item != null)
        {
            DropItemButton.interactable = true;
            DeleteItemButton.interactable = true;
        }
        else
        {
            DropItemButton.interactable = false;
            DeleteItemButton.interactable = false;
        }

        if ((idSelectedInv != 0 && slots[idSelectedInv - 1].item != null) || (idSelectedQuickBarInv != 0 && QuickBar.slotsQuickBarInv[idSelectedQuickBarInv - 1].item != null) || (idSelectedArmor != 0 && slotsArmor[idSelectedArmor - 1] != null))
        {

        }
        else
        {
            InteractiveButton.GetComponentInChildren<TMP_Text>().text = "";
            InteractiveButton.GetComponent<Button>().interactable = false;
        }

        foreach (InventorySlot slot in QuickBar.slotsQuickBar)
        {
            if (slot.item != null)
            {
                QuickBar.slotsQuickBarInv[slot.IDSlot - 1].item = QuickBar.slotsQuickBar[slot.IDSlot - 1].item;
                QuickBar.slotsQuickBarInv[slot.IDSlot - 1].amount = QuickBar.slotsQuickBar[slot.IDSlot - 1].amount;
            }
            else
            {
                QuickBar.slotsQuickBarInv[slot.IDSlot - 1].item = null;
            }
        }
    }

    public static void AddItem(InventoryScripts _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            Debug.Log("CONNECT To INV");
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.MaximumAmount)
                {
                    slot.amount += _amount;
                    Debug.Log("CONNECTED To INV");
                    return;
                }
                Debug.Log("NOCONNECTED To INV");
            }
            Debug.Log("NOCONNECTED To INV");
        }
        foreach (InventorySlot slot in slots)
        {
            Debug.Log("ADD To INV");
            if (slot.isEmpty == true)
            {
                slot.isEmpty = false;
                slot.item = _item;
                slot.amount = _amount;
                Debug.Log("ADDED To INV");
                return;
            }
            Debug.Log("NOADDED To INV");
        }
        return;
    }
    public static void AddItemToArmor(InventoryScripts _item, int _amount)
    {
        foreach (InventorySlot slot in slotsArmor)
        {
            Debug.Log("CONNECT To SlotsArmor");
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.MaximumAmount)
                {
                    slot.amount += _amount;
                    Debug.Log("CONNECTED To SlotsArmor");
                    return;
                }
                Debug.Log("NOCONNECTED To SlotsArmor");
            }
            Debug.Log("NOCONNECTED To SlotsArmor");
        }
        foreach (InventorySlot slot in slotsArmor)
        {
            Debug.Log("ADD To SlotsArmor");
            if (slot.isEmpty == true)
            {
                slot.isEmpty = false;
                slot.item = _item;
                slot.amount = _amount;
                Debug.Log("ADDED To SlotsArmor");
                return;
            }
            Debug.Log("NOADDED To SlotsArmor");
        }
        return;
    }

    public void DropItem()
    {
        Transform faceTransform = playerYou.transform; // Припустимо, що у вашого гравця є компонент Transform, який представляє обличчя
        Vector3 facePosition = faceTransform.position;
        Quaternion faceRotation = faceTransform.rotation;
        Vector3 spawnOffset = new Vector3(0f, 1f, 1f); // Встановіть зміщення, яке позиціонує новий об'єкт перед обличчям гравця
        Vector3 spawnPosition = facePosition + faceRotation * spawnOffset;
        Quaternion spawnRotation = faceRotation; // Збережіть ту ж орієнтацію, як у гравця, але ви можете також змінити її за необхідності
        GameObject newObject;

        if (MenuGame.OnlineOffline) //MenuGame.OnlineOffline if online and !MenuGame.OnlineOffline if offline
        {
            newObject = Instantiate(slots[idSelectedInv - 1].item.itemPrefab, spawnPosition, spawnRotation);
            //newObject = PhotonNetwork.Instantiate(slots[idSelectedInv - 1].item.itemPrefab.name, spawnPosition, spawnRotation);
        }
        else
        {
            newObject = Instantiate(slots[idSelectedInv - 1].item.itemPrefab, spawnPosition, spawnRotation);
        }

        newObject.tag = "Items";
        newObject.GetComponent<Item>().amount = slots[idSelectedInv - 1].amount;
        slots[idSelectedInv - 1].item = null;
    }

    public void DeleteItem()
    {
        slots[idSelectedInv - 1].item = null;
    }

    public void InteractiveItemButton()
    {
        if (idSelectedInv != 0) //Одіти зброю або в хот бор
        {
            if (slots[idSelectedInv - 1].item.itemType == ItemType.Food)
            {
                bool tf = QuickBar.AddItem(slots[idSelectedInv - 1].item, slots[idSelectedInv - 1].amount);
                if (tf)
                {
                    slots[idSelectedInv - 1].item = null;
                }
            }
            if (slots[idSelectedInv - 1].item.itemType == ItemType.WeaponSword)
            {
                if (slotsArmor[0].item == null)
                {
                    AddItemToArmor(slots[idSelectedInv - 1].item, slots[idSelectedInv - 1].amount);
                    weapon = SwordInArmor(slots[idSelectedInv - 1].item.itemPrefab);
                    slots[idSelectedInv - 1].item = null;
                }
            }
        }
        if (idSelectedQuickBarInv != 0) //Зняти хот бар
        {
            AddItem(QuickBar.slotsQuickBar[idSelectedQuickBarInv - 1].item, QuickBar.slotsQuickBar[idSelectedQuickBarInv - 1].amount);
            QuickBar.slotsQuickBar[idSelectedQuickBarInv - 1].item = null;
        }
        if (idSelectedArmor != 0) //Зняти зброю
        {
            AddItem(slotsArmor[idSelectedArmor - 1].item, slotsArmor[idSelectedArmor - 1].amount);
            slotsArmor[idSelectedArmor - 1].item = null;

            Destroy(weapon);
        }
    }

    public GameObject SwordInArmor(GameObject obj)
    {
        GameObject newObject;

        if (MenuGame.OnlineOffline) //MenuGame.OnlineOffline if online and !MenuGame.OnlineOffline if offline
        {
            newObject = Instantiate(obj);
            //newObject = PhotonNetwork.Instantiate(obj);
        }
        else
        {
            newObject = Instantiate(obj);
        }

        newObject.transform.parent = rHandWeaponSlot;
        newObject.tag = "Weapon";
        newObject.layer = LayerMask.NameToLayer("weapon");
        Destroy(newObject.GetComponent<Rigidbody>());
        Destroy(newObject.GetComponent<PhotonView>());
        Destroy(newObject.GetComponent<PhotonTransformView>());
        Collider[] colliders = gameObject.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            Destroy(collider);
        }

        var colider = gameObject.AddComponent<BoxCollider>();
        colider.isTrigger = true;

        if (newObject.GetComponent<Item>().item.ItemName == "Коса мерців")
        {
            newObject.transform.localPosition = new Vector3(0.264f, -0.432f, 0.003f);
            newObject.transform.localRotation = new Quaternion(0, 0, 180, 0);

            colider.size = new Vector3(7.88f, 17.32f, 1f);
            colider.center = new Vector3(4.42f, -0.9f, 0f);
        }
        if (newObject.GetComponent<Item>().item.ItemName == "Посох Патріка")
        {
            newObject.transform.localPosition = new Vector3(-0.007f, -0.723f, 0.025f);
            newObject.transform.localRotation = new Quaternion(0, 0, 0, 0);

            colider.size = new Vector3(0.1f, 1.9f, 0.1f);
            colider.center = new Vector3(0f, 0.4f, 0f);
        }
        return newObject;
    }

    public static void UpdatesInfo()
    {
        if (idSelectedInv != 0)
        {
            if (slots[idSelectedInv - 1].item != null)
            {
                if (slots[idSelectedInv - 1].item.itemType == ItemType.Food)
                {
                    InteractiveButton.GetComponent<Button>().interactable = true;
                    InteractiveButton.GetComponentInChildren<TMP_Text>().text = "Взяти";
                }
                if (slots[idSelectedInv - 1].item.itemType == ItemType.WeaponSword || slots[idSelectedInv - 1].item.itemType == ItemType.WeaponMagic || slots[idSelectedInv - 1].item.itemType == ItemType.WeaponBow)
                {
                    InteractiveButton.GetComponent<Button>().interactable = true;
                    InteractiveButton.GetComponentInChildren<TMP_Text>().text = "Одягнути";
                }
            }
        }
        if (idSelectedQuickBarInv != 0)
        {
            if (QuickBar.slotsQuickBarInv[idSelectedQuickBarInv - 1].item != null)
            {
                InteractiveButton.GetComponent<Button>().interactable = true;
                InteractiveButton.GetComponentInChildren<TMP_Text>().text = "Покласти";
            }
        }
        if (idSelectedArmor != 0)
        {
            if (slotsArmor[idSelectedArmor - 1].item != null)
            {
                InteractiveButton.GetComponent<Button>().interactable = true;
                InteractiveButton.GetComponentInChildren<TMP_Text>().text = "Зняти";
            }
        }
    }

    public void DestroyColliders(GameObject gameObject)
    {
        gameObject.transform.SetParent(rHandWeaponSlot);

        rHandWeaponSlot.GetChild(0).tag = "Weapon";
        rHandWeaponSlot.GetChild(0).gameObject.layer = LayerMask.NameToLayer("weapon");
        Destroy(rHandWeaponSlot.GetChild(0).GetComponent<Rigidbody>());
        Destroy(rHandWeaponSlot.GetChild(0).GetComponent<PhotonView>());
        Destroy(rHandWeaponSlot.GetChild(0).GetComponent<PhotonTransformView>());

        Collider[] colliders = gameObject.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            Destroy(collider);
        }

        var colider = gameObject.AddComponent<BoxCollider>();
        colider.isTrigger = true;
        colider.size = new Vector3(7.88f, 17.32f, 1f);
        colider.center = new Vector3(4.42f, -0.9f, 0f);
    }
}
