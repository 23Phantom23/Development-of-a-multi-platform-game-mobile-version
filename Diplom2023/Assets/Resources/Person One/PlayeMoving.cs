using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeMoving : MonoBehaviour
{
    int VelocityHashX;
    int VelocityHashZ;





    void Start()
    {
        VelocityHashX = Animator.StringToHash("Velocity X");
        VelocityHashZ = Animator.StringToHash("Velocity Z");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
