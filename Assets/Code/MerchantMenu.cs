using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantMenu : MonoBehaviour
{
    public static MerchantMenu instance;
    //public GameObject item1, item2, item3;
    public GameObject Continue, ItemMenu, UpgradeMenu;
    private void Awake()
    {
        instance = this;
    }
    public void Show()
    {
        // make it so that we go back to the greeting upon reopening
        Audiocontroller.instance.playmenu();
        ShowMainMenu();
        gameObject.SetActive(true);
        ItemMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        // pause time
        Time.timeScale = 0;
        Samurai.instance.paused = true; // pause player
        Knight.instance.paused = true; // pause player
    }
    public void Hide()
    {
        Audiocontroller.instance.playmenu();
        gameObject.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Samurai.instance.paused = false;
        Knight.instance.paused = false;

    }
    void Start()
    {
        Hide(); // runs after awake to make sure the player controller object exists
    }
    void switchMenu(GameObject someMenu)
    {
        Audiocontroller.instance.playmenu();
        //clean up
        Continue.SetActive(false);
        ItemMenu.SetActive(false);
        UpgradeMenu.SetActive(false);

        //activate requested menu
        someMenu.SetActive(true);
    }

    public void ShowMainMenu() // calling it like this prevents erroneously calling the wrong game object
    {
        switchMenu(Continue);
    }
    public void ShowItemMenu()
    {
        switchMenu(ItemMenu);
    }
    public void ShowUpgradeMenu()
    {
        switchMenu(UpgradeMenu);
    }
}
