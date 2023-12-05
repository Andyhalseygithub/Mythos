using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armswinging : MonoBehaviour
{
    public static armswinging instance;
    public Transform attackZone;
    public Transform swordhitbox;
    public Transform shadebringerhitbox;
    public Transform hellbournehitbox;
    public bool swungSword;
    public bool LaunchProjectile;
    public float activeDamage;
    public float activeWeapon;
    public float hitboxsize;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        swungSword = false;
        LaunchProjectile = false;
    }

    // Update is called once per frame
    void Update()
    {
        activeWeapon = Knight.instance.weapon_equipped;
        if (activeWeapon == 1)
        {
            attackZone = swordhitbox;
            activeDamage = sword.instance.damage;
            hitboxsize = 3f;
        }
        if (activeWeapon == 2)
        {
            attackZone = shadebringerhitbox;
            activeDamage = Shadebringer.instance.damage;
            hitboxsize = 7f;
        }
        if (activeWeapon == 3)
        {
            attackZone = hellbournehitbox;
            activeDamage = hellbourne.instance.damage;
            hitboxsize = 4f;
        }
    }
    public void startSwing()
    {
        swungSword = true;
    }
    public void Swung()
    {
        swungSword = false;
    }
    public void LaunchProj()
    {
        LaunchProjectile = true;
    }
    public void LaunchedProj()
    {
        LaunchProjectile = false;
    }
    public void Damage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, hitboxsize);
        foreach (Collider2D hit in hits)
        {
            Entity enemy = hit.GetComponent<Entity>();
            if (enemy)
            {
                enemy.TakeDamage(activeDamage);
            }
        }
    }
}
