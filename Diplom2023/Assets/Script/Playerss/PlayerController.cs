using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _movementSpeed = 4f;
 
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private float xAngle;
    private float yAngle;
    private float xAngleTemp;
    private float yAngleTemp;

    private FloatingJoystick _floatingJoystick;

    //private float _rotationX;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _floatingJoystick = FindObjectOfType<FloatingJoystick>();
        /*Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;*/

        yAngle = transform.localRotation.eulerAngles.y;

        if (!_photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(_rigidbody);
        }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
            PlayerMovement();
    }

    private void Update()
    {
        if (!_photonView.IsMine)
            return;


        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x > Screen.width / 2 & touch.phase == TouchPhase.Began)
            {
                firstPoint = touch.position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            if (touch.position.x > Screen.width / 2 & touch.phase == TouchPhase.Moved)
            {
                secondPoint = touch.position;
                //xAngle = xAngleTemp - (secondPoint.y - firstPoint.y) * 90 / Screen.height;
                yAngle = yAngleTemp - (secondPoint.x - firstPoint.x) * 180 / Screen.width;
                transform.rotation = Quaternion.Euler(0, yAngle, 0);
                //xAngle = Mathf.Clamp ()
                //_camera.transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
            }
        }
    }

    private void PlayerMovement()
    {
        float h = _floatingJoystick.Horizontal;
        float v = _floatingJoystick.Vertical;

        Vector3 movementDir = transform.forward * v + transform.right * h;
        movementDir = Vector3.ClampMagnitude(movementDir, 1f);

        _rigidbody.velocity = new Vector3(movementDir.x * _movementSpeed, _rigidbody.velocity.y,
            movementDir.z * _movementSpeed);
    }
}