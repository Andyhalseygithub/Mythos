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
    }

    public void track(Transform t, Vector3 o)
    {
         transform.position = Vector3.SmoothDamp(
                    transform.position,
                    t.position + o,
                    ref _velocity,
                    smoothness
               );
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameController.instance.currentcam == true)
        {
            track(target, offset);
        }
        else if (GameController.instance.currentcam == false)
        {

            track(target2, offset);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            activecam = !activecam;
        }
    }
}
