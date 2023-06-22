using UnityEngine;
using UnityEngine.UI;
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

        public bool sprintInput = UiButtons.sprintInput;  //Mobile
        //public bool sprintInput = KeyCode.LeftShift;  //PC


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

        public Transform rHandWeaponSlot;
        public GameObject Mapa;


        private PhotonView _photonView;
        public bool isOpenFlourInv = false; //PC
        public bool isOpenInv = false; //PC

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();

            _floatingJoystick = FindObjectOfType<FloatingJoystick>();
            _photonView = GetComponent<PhotonView>();
            cam = FindObjectOfType<Camera>();

            
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (!MenuGame.OnlineOffline)
                {
                    Destroy(player.transform.Find("Quad").gameObject);
                    break;
                }
                if (player.GetComponent<PhotonView>().IsMine == false)
                {
                    Destroy(player.GetComponent<SphereCollider>());
                    Destroy(player.GetComponent<Damage>());
                    Destroy(player.transform.Find("Map").gameObject);
                }
                if (player.GetComponent<PhotonView>().IsMine == true)
                {
                    Destroy(player.transform.Find("Quad").gameObject);
                }
            }

            InventoryManager.rHandWeaponSlot = rHandWeaponSlot;
            InventoryManager.Mapa = Mapa;
        }

        protected virtual void FixedUpdate()
        {
            if (!MenuGame.OnlineOffline)
            {
                cc.UpdateMotor();               // updates the ThirdPersonMotor methods
                cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
                cc.ControlRotationType();       // handle the controller rotation type
                return;
            }
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
            if (!MenuGame.OnlineOffline)
            {
                MoveInput();
                CameraInput();
                SprintInput();
                OpenInvFlour();//PC
                OpenInv();//PC
                //Attack();//PC
                return;
            }
            if (!_photonView.IsMine)
                return;

            MoveInput();
            CameraInput();
            SprintInput();
            OpenInvFlour();//PC
            OpenInv();//PC
            //Attack();//PC
        }

        //PC
        public virtual void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                UiButtons.anim.Play("Attack_Sword");
            }
        }
        //PC
        public virtual void OpenInvFlour()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                isOpenFlourInv = !isOpenFlourInv;
                if (isOpenFlourInv == true)
                {
                    MenuOnFlour.menuItemsOnFlour.SetActive(false);
                } 
                else
                {
                    MenuOnFlour.menuItemsOnFlour.SetActive(true);
                    MenuOnFlour.UpdateItemsOnFlour(ItemsOnFlourScript.CountItemsInFlour, ItemsOnFlourScript.objectsInTrigger);
                }
            }
        }
        //PC
        public virtual void OpenInv()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                isOpenInv = !isOpenInv;
                if (isOpenInv == true)
                {
                    MenuOnFlour.DestroyChild();
                    MenuOnFlour.listsFlour.Clear();
                    MenuOnFlour.countItemsOn = 0;
                    MenuOnFlour.iDSelectedOnFlourInt = 0;
                    InventoryManager.menuInventory.SetActive(false);
                }
                else
                {
                    InventoryManager.menuInventory.SetActive(true);
                }
            }
        }


        public virtual void MoveInput()
        {
            cc.input.x = _floatingJoystick.Horizontal;
            cc.input.z = _floatingJoystick.Vertical;
            /* //PC
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
            */
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
                    tpCamera.RotateCamera(yAngle, xAngle);
                }
            }

            /* //PC
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

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y); 
            */
        }

        protected virtual void SprintInput()
        {
            sprintInput = UiButtons.sprintInput;
            if (sprintInput == true)
            {
                cc.Sprint(true);
            }
            else if (sprintInput == false)
            {
                cc.Sprint(false);
            }

            /*  //PC
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
            */
        }

        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        #endregion       
    }
}