using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmootheCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;
    private Vector3 velocity = Vector3.zero;
   

    
    void FixedUpdate()
    {
        Vector3 moveposition = target.position + offset;
        Vector3 movepostion1 = new Vector3(moveposition.x, moveposition.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, movepostion1, ref velocity, damping);
    }

    
}
