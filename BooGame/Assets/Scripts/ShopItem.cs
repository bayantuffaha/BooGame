using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Button_UI button = GetComponent<Button_UI>();
        if (button != null)
        {
            Debug.Log("Hello");
            button.ClickFunc = () =>
            {
                OnButtonClick();
            };
        }
        else
        {
            Debug.Log("Button_UI component not found on ShopItem GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to be called when the button is clicked
    void OnButtonClick()
    {
        // Check some variables before allowing the player to buy the item
        if (CanBuyItem())
        {
            // Perform the purchase or other actions
            Debug.Log("Item bought!");
        }
        else
        {
            Debug.Log("Cannot buy the item.");
        }
    }

    bool CanBuyItem()
    {
        return true; // Replace with your actual condition
    }
}