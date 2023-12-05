using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : baseprojectile
{
    public static Katana instance;
    public Transform attackZone;
    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        damage = 20;

    }
    private void Update()
    {

    }

    public void Damage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, 2f);
        foreach (Collider2D hit in hits)
        {
            Entity enemy = hit.GetComponent<Entity>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
