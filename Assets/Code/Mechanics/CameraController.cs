using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform target2;
    public Vector3 offset;
    public Vector3 offset2;
    public float smoothness;
    public bool activecam = false;
    Vector3 _velocity;
    public Vector3 activecamera1;
    public Vector3 activecamera2;
    // Start is called before the first frame update
    void Start()
    {
        if(target)
        {
            offset = transform.position - target.position;
        }

        /*if (target2)
        {
            offset = transform.position - target2.position;
        }*/
    }
    public void initializeTarget2()
    {
        if (target2)
        {
            offset = transform.position - target2.position;
        }
    }
    public void track(Transform t, Vector3 o)
    {
         transform.position = Vector3.SmoothDamp(
                    transform.position,
                    t.position + o,
                    ref _velocity,
                    smoothness
               );
        print(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (activecam == false)
        {
            track(target, offset);
        }
        else if (activecam == true)
        {
            initializeTarget2();
            track(target2, offset2);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            activecam = !activecam;
        }
        

        /*if (Input.GetKeyDown(KeyCode.G))
        {
            activecam = !activecam;
            print(activecam);
        }
        if (activecam == true)
        {
            track(target, offset);
        }
        else if (activecam == false)
        {
            initializeTarget2();
            track(target2, offset2);
        }*/

        /*if (Input.GetKeyDown(KeyCode.G))
        {
            activecam = !activecam;
        }
        if (activecam == true)
        {
            if (target)
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    target.position + offset,
                    ref _velocity,
                    smoothness
               );
            }
        }
        else if (activecam == false) {
            if (target2)
            {
                transform.position = Vector3.SmoothDamp(
                     transform.position,
                     target2.position + offset,
                     ref _velocity,
                     smoothness
                 );
            }
        }*/
    }
}
