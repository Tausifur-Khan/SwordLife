﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float HP;
    public float maxHP;
    //public float Att;
    public float Acceleration;
    public float Speed;
    public float jumpSpeed;
    public float knockback;
    public bool isGrounded;
    public void Damage()
    {
        transform.GetComponentInChildren<Slider>().value = (maxHP / HP);
    }
    public void Death()
    {
        if (GetComponent<CircleCollider2D>())
            Destroy(GetComponent<CircleCollider2D>());

        if (GetComponent<BoxCollider2D>())
            Destroy(GetComponent<BoxCollider2D>());

        GetComponent<Animator>().SetTrigger("Death");
        Destroy(gameObject, 1);
    }
}
