using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int item_id;
    public enum ItemType
    {
        Weapon, Accessory, Potions
    }
    public virtual void use()
    {
        print("used item");
    }
}
