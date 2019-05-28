using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killwithnothing : MonoBehaviour
{
    public float start, end;
    // Use this for initialization
    void Awake()
    {
        Invoke("Life", start);
    }

    // Update is called once per frame
    void Life()
    {
        GetComponent<BoxCollider2D>().enabled = true;     
        Invoke("Death", start);
    }
    void Death()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
