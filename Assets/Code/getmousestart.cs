using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getmousestart : MonoBehaviour
{
    //public Transform aimPivot;
    public Texture2D crosshair;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(crosshair, new UnityEngine.Vector2(100, 100), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        //aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);
    }
}
