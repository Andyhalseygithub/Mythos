using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarmswinging : MonoBehaviour
{
    public static Sarmswinging instance;
    public Transform attackZone;
    public Transform katanahitbox;
    public Transform penumbrahitbox;
    public Transform blizzardhitbox;
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
        activeWeapon = Samurai.instance.weapon_equipped;
        if (activeWeapon == 1)
        {
            attackZone = katanahitbox;
            activeDamage = Katana.instance.damage;
            hitboxsize = 3f;
        }
        if (activeWeapon == 2)
        {
            attackZone = penumbrahitbox;
            activeDamage = penumbra.instance.damage;
            hitboxsize = 7f;
        }
        if (activeWeapon == 3)
        {
            attackZone = blizzardhitbox;
            activeDamage = blizzard.instance.damage;
            hitboxsize = 7f;
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