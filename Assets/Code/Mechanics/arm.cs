using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arm : MonoBehaviour
{
    public static arm instance;
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

        
        if (Input.GetMouseButtonDown(0) && armswinging.instance.swungSword == false) {
            if (animator.GetBool("attack") != true)
            {
                animator.SetBool("attack", true);
                Audiocontroller.instance.playswordswing();
                //StartCoroutine(resetattack());
            }
        }
        if (Input.GetMouseButton(0) && armswinging.instance.swungSword == false)
        {
            if (animator.GetBool("attack") != true)
            {
                animator.SetBool("attack", true);
                Audiocontroller.instance.playswordswing();
                //StartCoroutine(resetattack());
            }
        }
    }

   IEnumerator Resetarm1()
    {
        print("Right");
        called1 = true;
        called2 = false;
        transform.localScale = new Vector3(1, 1, 1);
        //aimPivot.position = new Vector3((float)-0.088, (float)0.107, 0);
        aimPivot.localScale = new Vector3(1, 1, 1);
        aimPivot.position = (aimPivot.position + new Vector3(0, 1, 0));
        transform.localPosition = new Vector3(0, 0, 0);
        yield return aimPivot.position = (aimPivot.position + new Vector3(0, -1, 0));
    }
    IEnumerator Resetarm2()
    {
        print("Left");
        called2 = true;
        called1 = false;
        transform.localScale = new Vector3(-1, -1, 1);
        transform.localPosition = new Vector3(-0.204f, 0.241f, 0);
        aimPivot.localScale = new Vector3(1, 1, 1);
        aimPivot.position = (aimPivot.position + new Vector3(0, -1, 0));
        yield return aimPivot.position = (aimPivot.position + new Vector3(0, 1, 0));
    }
}


/*{
    public static arm instance;
Animator animator;
public Transform aimPivot;
public Transform bimPivot;
public bool called1;
public bool called2;
public bool swungSword1;
public bool LaunchProjectile1;
public bool start;
//public GameObject armswinging;
private void Start()
{
    animator = GetComponentInChildren<Animator>();
    animator.SetBool("attack", false);
    bimPivot = aimPivot;
    called1 = false;
    called2 = false;
    start = false;
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


    if (Input.GetMouseButtonDown(0) && armswinging.instance.swungSword == false)
    {
        if (animator.GetBool("attack") != true)
        {
            animator.SetBool("attack", true);
            //StartCoroutine(resetattack());
        }
    }
    if (Input.GetMouseButton(0) && armswinging.instance.swungSword == false)
    {
        if (animator.GetBool("attack") != true)
        {
            animator.SetBool("attack", true);
            //StartCoroutine(resetattack());
        }
    }
}

IEnumerator Resetarm1()
{
    print("Right");
    called1 = true;
    called2 = false;
    start = true;
    transform.localScale = new Vector3(1, 1, 1);
    //aimPivot.position = new Vector3((float)-0.088, (float)0.107, 0);
    aimPivot.localScale = new Vector3(1, 1, 1);
    *//*if (start == true)
    {
        aimPivot.position = (aimPivot.position - new Vector3((float)0.097, (float)-0.111, (float)0));
        aimPivot.position = (aimPivot.position + new Vector3((float)-0.102, (float)0.13, (float)0));
    }*//*
    //(aimPivot.position + new Vector3((float)-0.102, (float)0.13, (float)0));
    yield return aimPivot.position = (aimPivot.position + new Vector3((float)0, (float)-1, (float)0));
    //yield return start = true;
}
IEnumerator Resetarm2()
{
    print("Left");
    called2 = true;
    called1 = false;
    transform.localScale = new Vector3(-1, -1, 1);
    aimPivot.localScale = new Vector3(1, 1, 1);
    *//*if (start == true)
    {
        aimPivot.position = (aimPivot.position - new Vector3((float)-0.102, (float)0.13, (float)0));
        aimPivot.position = (aimPivot.position + new Vector3((float)0.097, (float)-0.111, (float)0));
    }*//*
    //(aimPivot.position + new Vector3((float)0.097, (float)-0.111, (float)0));
    yield return aimPivot.position = (aimPivot.position + new Vector3((float)0, (float)1, (float)0));
    //yield return start = true;
}
}*/