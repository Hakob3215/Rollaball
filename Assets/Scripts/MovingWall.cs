using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
   

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(7.0f*Mathf.Sin(Time.time),0f,0f);
    }
}
