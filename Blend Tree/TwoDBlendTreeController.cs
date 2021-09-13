using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlendTree
{
    public class TwoDBlendTreeController : MonoBehaviour
    {      
        public Animator anim; // assign animator

        float acceleration = 2.0f; //this can change
        float deceleration = 2.0f;//this can change
        float maxWalkVelocity = 0.5f; // this can change
        float maxRunVelocity = 2.0f; // this can change

        float velocityX, velocityZ; // blendtree values
        bool forwardPressed, leftPressed, rightPressed, runPressed; // these gets control key values in "GetKeyValues()" method

        public void GetKeyValues()
        {
            forwardPressed = Input.GetKey(KeyCode.W);
            leftPressed = Input.GetKey(KeyCode.A);
            rightPressed = Input.GetKey(KeyCode.D);
            runPressed = Input.GetKey(KeyCode.LeftShift);
        }//check if keys pressed or not
        public void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
        {
            {
                if (forwardPressed && velocityZ < currentMaxVelocity) // animtate forward
                {
                    velocityZ += Time.deltaTime * acceleration;
                }
                else if (!forwardPressed && velocityZ > 0f) // slow down 
                {
                    velocityZ -= Time.deltaTime * deceleration;
                }
                else if (!forwardPressed && velocityZ < 0f) // if velocityZ is negative make it zero
                {
                    velocityZ = 0.0f;
                }
            }//forward move or stop

            {

                if (leftPressed && velocityX > -currentMaxVelocity) // if left pressed
                {
                    velocityX -= Time.deltaTime * acceleration;
                }
                else if (!leftPressed && velocityX < 0.0f) // if left isnt pressed
                {
                    velocityX += Time.deltaTime * deceleration;
                }
                else if (rightPressed && velocityX < currentMaxVelocity) // if right pressed
                {
                    velocityX += Time.deltaTime * acceleration;
                }
                else if (!rightPressed && velocityX > 0.0f) // if right isnt pressed
                {
                    velocityX -= Time.deltaTime * deceleration;
                }
                else if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))//if both
                {                                                                                                         //arent pressed 
                    velocityX = 0.0f;
                }
            }//left/right move or stop

        }
        public void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
        {
            {
                /*----------------------------------locking forward-------------------------------------*/
                if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
                {
                    velocityZ = currentMaxVelocity;
                }
                else if (forwardPressed && velocityZ > currentMaxVelocity)
                {
                    velocityZ -= Time.deltaTime * deceleration;
                    if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
                    {
                        velocityZ = currentMaxVelocity;
                    }
                }
                else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
                {
                    velocityZ = currentMaxVelocity;
                }
                /*--------------------------------------------------------------------------------------*/
            }// lock forward

            {
                /*----------------------------------locking left-------------------------------------*/
                if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
                {
                    velocityX = -currentMaxVelocity;
                }
                else if (leftPressed && velocityX < -currentMaxVelocity)
                {
                    velocityX += Time.deltaTime * deceleration;
                    if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
                    {
                        velocityX = -currentMaxVelocity;
                    }
                }
                else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
                {
                    velocityX = -currentMaxVelocity;
                }
                /*--------------------------------------------------------------------------------------*/
            }//lock left

            {
                /*----------------------------------locking right-------------------------------------*/
                if (rightPressed && runPressed && velocityX > currentMaxVelocity)
                {
                    velocityX = currentMaxVelocity;
                }
                else if (rightPressed && velocityX > currentMaxVelocity)
                {
                    velocityX -= Time.deltaTime * deceleration;
                    if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
                    {
                        velocityX = currentMaxVelocity;
                    }
                }
                else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
                {
                    velocityX = currentMaxVelocity;
                }
                /*--------------------------------------------------------------------------------------*/
            }//lock right
        }
       
        
        public void AnimatorController() // must call in Update - controls blend tree
        {
            GetKeyValues();

            float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

            ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
            LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

            anim.SetFloat("Velocity Z", velocityZ);
            anim.SetFloat("Velocity X", velocityX);
        }


    }
}

