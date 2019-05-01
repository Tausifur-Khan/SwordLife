using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatThrough : MonoBehaviour
{

    public Transform raycastPos;
    public float rayDist;
    [SerializeField]
    Collider2D player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RaycastSet();
    }

    void RaycastSet()
    {
        Ray ray = new Ray(raycastPos.position, Vector2.up);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayDist);

        if (hit)
        {
            if (hit.collider.CompareTag("Ground"))
            {
               
            }
               

        }
       
    }

    void OnDrawGizmos()
    {

        // set new ray pos and direction
        Ray ray = new Ray(raycastPos.position, Vector2.up);
        //Set colour of ray red
        Gizmos.color = Color.blue;
        //Draw ray
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayDist);
    }
}
