using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Audio;

public class ShopMenu2 : MonoBehaviour
{
    public static bool ShopisOpen = false;
    public GameObject shopMenuUI;
    public GameObject buttonOpenShop;

    public GameObject item1BuyButton;
    public GameObject item2BuyButton;
    public GameObject item3BuyButton;

    public int item1Cost = 10;
    public int item2Cost = 15;
    public int item3Cost = 20;
    //public AudioSource KaChingSFX;

    void Start()
    {
        shopMenuUI.SetActive(false);
        // gameController = GameObject.FindWithTag("GameHandler").GetComponent<GameController>();
    }

    void Update()
    {
        if (GameController.control.candyCount >= item1Cost)
        {
            item1BuyButton.SetActive(true);
        }
        else { item1BuyButton.SetActive(false); }

        if (GameController.control.candyCount >= item2Cost)
        {
            item2BuyButton.SetActive(true);
        }
        else { item2BuyButton.SetActive(false); }

        if (GameController.control.candyCount >= item3Cost)
        {
            item3BuyButton.SetActive(true);
        }
        else { item3BuyButton.SetActive(false); }
    }

    //Button Functions:
    public void Button_OpenShop()
    {
        Debug.Log("Hello");
        shopMenuUI.SetActive(true);
        buttonOpenShop.SetActive(false);
        ShopisOpen = true;
        Time.timeScale = 0f;
    }

    public void Button_CloseShop()
    {
        shopMenuUI.SetActive(false);
        buttonOpenShop.SetActive(true);
        ShopisOpen = false;
        Time.timeScale = 1f;
    }

    public void Button_BuyItem1()
    {
        GameController.control.candyCount = GameController.control.candyCount - item1Cost;
        GameController.control.bombCount++;
        //KaChingSFX.Play();
    }

    public void Button_BuyItem2()
    {
        GameController.control.candyCount = GameController.control.candyCount - item2Cost;
        GameController.control.bottleCount++;
        //KaChingSFX.Play();
    }

    public void Button_BuyItem3()
    {
        GameController.control.candyCount = GameController.control.candyCount - item3Cost;
        GameController.control.speedCount++;
        //KaChingSFX.Play();
    }
}