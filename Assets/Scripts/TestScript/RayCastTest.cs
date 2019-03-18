//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RayCastTest : MonoBehaviour
//{
//    void Grounded()
//    {
//        #region For Each in Raycasting
//        //for each raycast within parent transform
//        foreach (Transform raycast in raycasts)
//        {
//            int i = 0;
//            // set new ray pos and direction
//            Ray ray = new Ray(raycast.position, Vector2.down);

//            //set raycast hit 2d objects
//            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDist, groundLayer);

//            //check if it hits something
//            if (hit.collider != null)
//            {
//                if (hit.collider.CompareTag("Ground"))
//                {
//                    Debug.Log("Is Grounded");
//                    //set grounded bool true 
//                    groundedChecks[i] = true;
//                    //set doubleJump bool false

//                    //set jump animtion to false upon contact
//                    // anim.SetBool("isJumping", false);
//                }
//            }
//            //otherwise if hit nothing then..
//            else
//            {
//                Debug.Log("Is NOT Grounded");
//                //set jump animtion to false upon contact
//                // anim.SetBool("isJumping", true);
//                groundedChecks[i] = false;

//            }
//            i++;

//        }
//        #endregion
//        //if 1 or 2

//        // else if ! 1&& 2
//    }
//}
