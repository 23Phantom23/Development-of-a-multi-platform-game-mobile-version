using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountsMenu : MonoBehaviour
{
    [SerializeField] private GameObject Standartmenu;
    [SerializeField] private GameObject Registermenu;
    [SerializeField] private GameObject Loginmenu;

    void Start()
    {
        /*Screen.fullScreen = true;*/
        Standartmenu.SetActive(true);
        Registermenu.SetActive(false);
        Loginmenu.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void RegisterButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoginButton()
    {
        SceneManager.LoadScene("MainMenu");
    }







    public void RegisterMenu()
    {
        Standartmenu.SetActive(false);
        Registermenu.SetActive(true);
        Loginmenu.SetActive(false);
    }

    public void LoginMenu()
    {
        Standartmenu.SetActive(false);
        Registermenu.SetActive(false);
        Loginmenu.SetActive(true);
    }

    public void BackButton()
    {
        Standartmenu.SetActive(true);
        Registermenu.SetActive(false);
        Loginmenu.SetActive(false);
    }
}
