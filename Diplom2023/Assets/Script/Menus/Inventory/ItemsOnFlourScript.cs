using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsOnFlourScript : MonoBehaviour
{
    public static int CountItemsInFlour;
    public float radius = 3.5f;
    public static List<GameObject> objectsInTrigger = new List<GameObject>();
    public static GameObject menuItemsOnFlourButton;

    private void Update()
    {
        if (CountItemsInFlour > 0)
        {
            menuItemsOnFlourButton.SetActive(true);
        }
        else
        {
            menuItemsOnFlourButton.SetActive(false);
            MenuOnFlour.menuItemsOnFlour.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Items") && !objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Add(other.gameObject);
            CountItemsInFlour++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Items") && objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject);
            CountItemsInFlour--;
        }
    }
}
