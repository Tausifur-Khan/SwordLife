using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTest : MonoBehaviour
{
    public LayerMask layer;
    public Transform raycasts;
    public float rayRange;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        //for each transform in raycast parent
        foreach (Transform raycast in raycasts)
        {
            //if child has tag "Raycast" then...
            if (raycast.gameObject.CompareTag("Raycast"))
            {
                //create Ray
                Ray ray = new Ray(raycast.position, Vector2.down);

                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayRange, layer);

                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("Ground Found");
                }
                else
                {
                    Debug.Log("No Ground");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Transform raycast in raycasts)
        {
            Ray ray = new Ray(raycast.position, Vector2.down);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayRange);
        }
           
    }
}
