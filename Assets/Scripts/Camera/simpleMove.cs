using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class simpleMove : MonoBehaviour
{
    public float moveSpeed = 3;
    public Rigidbody2D rigid;
    // Use this for initialization
    void Start()
    {
        rigid.velocity = transform.up * moveSpeed;
        //Debug.Log("Final speed = " + rigid.velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            //other.GetComponent<Health>().hp--;
        }
        if(transform.parent.childCount == 1)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }
}
