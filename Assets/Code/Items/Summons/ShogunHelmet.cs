using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.SceneManagement;

public class ShogunHelmet : Item
{
    public GameObject Shogun;
    public override void use() // Expand upon use function in item base class
    {
        base.use(); // Calling the base item's function use
        print("used boss item");
            GameObject spawnboss = Instantiate(Shogun);
            spawnboss.transform.position = transform.position + new Vector3(0, 20f, 0);
    }
}
