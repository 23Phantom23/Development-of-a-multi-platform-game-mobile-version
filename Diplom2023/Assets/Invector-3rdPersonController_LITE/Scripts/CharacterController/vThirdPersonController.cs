using UnityEngine;
using Photon.Pun;

namespace Invector.vCharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        //public Animator animatorr;
        //private PhotonView photonView;

        public virtual void ControlAnimatorRootMotion()
        {
            if (!this.enabled) return;

            if (inputSmooth == Vector3.zero)
            {
                transform.position = animator.rootPosition;
                transform.rotation = animator.rootRotation;
            }
            
            if (useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlLocomotionType()
        {
            if (lockMovement) return;

            if (locomotionType.Equals(LocomotionType.FreeWithStrafe) && !isStrafing || locomotionType.Equals(LocomotionType.OnlyFree))
            {
                SetControllerMoveSpeed(freeSpeed);
                SetAnimatorMoveSpeed(freeSpeed);
            }
            else if (locomotionType.Equals(LocomotionType.OnlyStrafe) || locomotionType.Equals(LocomotionType.FreeWithStrafe) && isStrafing)
            {
                isStrafing = true;
                SetControllerMoveSpeed(strafeSpeed);
                SetAnimatorMoveSpeed(strafeSpeed);
            }

            if (!useRootMotion)
                MoveCharacter(moveDirection);
        }

        public virtual void ControlRotationType()
        {
            if (lockRotation) return;

            bool validInput = input != Vector3.zero || (isStrafing ? strafeSpeed.rotateWithCamera : freeSpeed.rotateWithCamera);

            if (validInput)
            {
                // calculate input smooth
                inputSmooth = Vector3.Lerp(inputSmooth, input, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);

                Vector3 dir = (isStrafing && (!isSprinting || sprintOnlyFree == false) || (freeSpeed.rotateWithCamera && input == Vector3.zero)) && rotateTarget ? rotateTarget.forward : moveDirection;
                RotateToDirection(dir);
            }
        }

        public virtual void UpdateMoveDirection(Transform referenceTransform = null)
        {
            if (input.magnitude <= 0.01)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, (isStrafing ? strafeSpeed.movementSmooth : freeSpeed.movementSmooth) * Time.deltaTime);
                return;
            }

            if (referenceTransform && !rotateByWorld)
            {
                //get the right-facing direction of the referenceTransform
                var right = referenceTransform.right;
                right.y = 0;
                //get the forward direction relative to referenceTransform Right
                var forward = Quaternion.AngleAxis(-90, Vector3.up) * right;
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                moveDirection = (inputSmooth.x * right) + (inputSmooth.z * forward);
            }
            else
            {
                moveDirection = new Vector3(inputSmooth.x, 0, inputSmooth.z);
            }
        }

        public virtual void Sprint(bool value)
        {
            var sprintConditions = (input.sqrMagnitude > 0.1f && isGrounded &&
                !(isStrafing && !strafeSpeed.walkByDefault && (horizontalSpeed >= 0.5 || horizontalSpeed <= -0.5 || verticalSpeed <= 0.1f)));

            if (value && sprintConditions)
            {
                if (input.sqrMagnitude > 0.1f)
                {
                    if (isGrounded && useContinuousSprint)
                    {
                        isSprinting = !isSprinting;
                        /*Debug.Log("ShiftTrue");
                        SetAnimBool(this.gameObject.GetPhotonView().ViewID, "isSprinting", isSprinting);
                        photonView.RPC("isSprinting", RpcTarget.All, this.gameObject.GetPhotonView().ViewID, isSprinting);*/
                    }
                    else if (!isSprinting)
                    {
                        isSprinting = true;
                        /*photonView.RPC("isSprinting", RpcTarget.All, this.gameObject.GetPhotonView().ViewID, isSprinting);
                        Debug.Log("ShiftTrue");
                        SetAnimBool(this.gameObject.GetPhotonView().ViewID, "isSprinting", isSprinting);*/
                    }
                }
                else if (!useContinuousSprint && isSprinting)
                {
                    isSprinting = false;
                    Debug.Log("ShiftFalse");
                    /*photonView.RPC("isSprinting", RpcTarget.All, this.gameObject.GetPhotonView().ViewID, isSprinting);
                    SetAnimBool(this.gameObject.GetPhotonView().ViewID, "isSprinting", isSprinting);*/
                }
            }
            else if (isSprinting)
            {
                isSprinting = false;
                Debug.Log("ShiftFalse");
                /*photonView.RPC("isSprinting", RpcTarget.All, this.gameObject.GetPhotonView().ViewID, isSprinting);
                SetAnimBool(this.gameObject.GetPhotonView().ViewID, "isSprinting", isSprinting);*/
            }
        }

       /* public void SetAnimBool(int GoViedID, string action, bool truthfulness)
        {
            Animator animator = PhotonNetwork.GetPhotonView(GoViedID).gameObject.GetComponent<vThirdPersonController>().animatorr;
            animator.SetBool(action, truthfulness);
        }*/
    }
}