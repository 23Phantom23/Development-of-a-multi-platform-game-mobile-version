using UnityEngine;
using Photon.Pun;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        private FloatingJoystick _floatingJoystick;
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Input")]
        private Vector3 firstPoint;
        private Vector3 secondPoint;
        private float xAngle;
        private float yAngle;
        private float xAngleTemp;
        private float yAngleTemp;
        [SerializeField] private Camera cam;

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;


        private PhotonView _photonView;
        //private Rigidbody _rigidbody;

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();

            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
            _photonView = GetComponent<PhotonView>();
            cam = FindObjectOfType<Camera>();
        }

        protected virtual void FixedUpdate()
        {
            if (!_photonView.IsMine)
                return;


            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            if (!_photonView.IsMine)
                return;


            MoveInput();
            CameraInput();
            SprintInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = _floatingJoystick.Horizontal;
            cc.input.z = _floatingJoystick.Vertical;
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;


            /*var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);*/

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
                    yAngle = yAngleTemp - (secondPoint.x - firstPoint.x) * 180 / Screen.width;
                    //cam.transform.rotation = Quaternion.Euler(0, yAngle, 0);
                    tpCamera.RotateCamera(yAngle, xAngle);
                }
            }

            //tpCamera.RotateCamera(xAngle, yAngle);
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        #endregion       
    }
}