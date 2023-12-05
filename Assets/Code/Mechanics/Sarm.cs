using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarm : MonoBehaviour
{
    public static Sarm instance;
    Animator animator;
    public Transform aimPivot;
    public Transform bimPivot;
    public bool called1;
    public bool called2;
    public bool swungSword1;
    public bool LaunchProjectile1;
    //public GameObject armswinging;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("attack", false);
        bimPivot = aimPivot;
        called1 = false;
        called2 = false;
    }
    public void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;
        if (directionFromPlayerToMouse.x > 0)
        {
            //aimPivot.position = (aimPivot.position + new Vector3((float)0.092, (float)-0.103, (float)0.004049368));
            if (called1 == false)
            {
                StartCoroutine(Resetarm1());
            }
        }
        if (directionFromPlayerToMouse.x < 0)
        {
            //aimPivot.position = bimPivot;//(aimPivot.position + new Vector3((float)-0.1077844, (float)-0.1233257, (float)0.004049368));
            if (called2 == false)
            {
                StartCoroutine(Resetarm2());
            }

        }
        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);


        if (Input.GetMouseButtonDown(0) && Sarmswinging.instance.swungSword == false)
        {
            if (animator.GetBool("attack") != true)
            {
                animator.SetBool("attack", true);
                Audiocontroller.instance.playkatanaswing();
                //StartCoroutine(resetattack());
            }
        }
        if (Input.GetMouseButton(0) && Sarmswinging.instance.swungSword == false)
        {
            if (animator.GetBool("attack") != true)
            {
                animator.SetBool("attack", true);
                Audiocontroller.instance.playkatanaswing();
                //StartCoroutine(resetattack());
            }
        }
    }

    IEnumerator Resetarm1()
    {
        print("Right");
        called1 = true;
        called2 = false;
        //transform.localScale = new Vector3(1, 1, 1);
        //aimPivot.position = new Vector3((float)-0.088, (float)0.107, 0);
        aimPivot.localScale = new Vector3(1, 1, 1);
        //aimPivot.position = (aimPivot.position + new Vector3(0, 1, 0));
        //transform.localPosition = new Vector3(0, 0, 0);
        //yield return aimPivot.localPosition = (aimPivot.localPosition + new Vector3(-1, 0, 0));
        yield return transform.localPosition = new Vector3(-1f, 0, 0);
    }
    IEnumerator Resetarm2()
    {
        print("Left");
        called2 = true;
        called1 = false;
        //transform.localScale = new Vector3(-1, -1, 1);
        //transform.localPosition = new Vector3(-0.987f, -0.017f, 0);
        aimPivot.localScale = new Vector3(-1, -1, 1);
        transform.localPosition = new Vector3(-1f, -0.034f, 0);
        //yield return aimPivot.localPosition = (aimPivot.localPosition + new Vector3(1, 0, 0));
        //yield return transform.localPosition = new Vector3(-0.987f, -0.017f, 0);
        yield return aimPivot.localPosition = new Vector3(0.026f, 0.236f, 0);
    }
}
