using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class simpleMove : MonoBehaviour
{
    public bool destroyoncoll = true;
    private bool erase;
    public float moveSpeed = 3;
    public Rigidbody2D rigid;
    public SpriteRenderer rend;
    // Use this for initialization
    void Start()
    {
        rigid.velocity = transform.up * moveSpeed;
        //Debug.Log("Final speed = " + rigid.velocity);
    }

    private void Update()
    {
        if (erase)
        {
            rend.material.color = new Color(1,1,1,rend.material.color.a - 0.005f);
        }
    }
    private void SetErase()
    {
        erase = true;
        Destroy(gameObject, 1);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyoncoll)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Hit player");
                //other.GetComponent<Health>().hp--;
            }
            if (transform.parent.childCount == 1)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
        else
        {
            Invoke("SetErase", 5);
        }
    }
}
