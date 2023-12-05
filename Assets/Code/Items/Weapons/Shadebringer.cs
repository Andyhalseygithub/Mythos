using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadebringer : baseprojectile
{
    public static Shadebringer instance;
    public Transform attackZone;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        damage = 55;

    }
    private void Update()
    {

    }
    public void Damage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackZone.position, 7f);
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
