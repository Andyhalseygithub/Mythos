using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothness;
    Vector3 _velocity;
    // Start is called before the first frame update
    void Start()
    {
        if(target){
            offset = transform.position - target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(target){
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + offset,
            ref _velocity,
            smoothness
        );
       } 
    }
}
