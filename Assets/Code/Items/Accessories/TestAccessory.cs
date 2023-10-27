using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAccessory : Item
{
    // get knight and samurai to change stats of
    public bool used = false;

    public override void use()
    {
        
        if (used == false)
        {
            Knight.instance.speedX = Knight.instance.speedX * 2;
            Samurai.instance.speedX = Samurai.instance.speedX * 2;
            used = true;
        }
    }
}
