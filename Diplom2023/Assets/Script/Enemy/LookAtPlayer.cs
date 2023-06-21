using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform camera;

    // Update is called once per frame
    private void Start()
    {
        camera = GameObject.Find("vThirdPersonCamera").transform;
    }
    void LateUpdate()
    {
        transform.LookAt(camera);
    }
}
