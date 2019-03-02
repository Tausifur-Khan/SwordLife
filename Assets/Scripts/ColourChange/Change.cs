using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    public MeshRenderer mRender;

    // Start is called before the first frame update
    void Start()
    {
        mRender = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Knight")
        {
            Debug.Log(col.gameObject.name == "Knight");
            mRender.material.color = Random.ColorHSV();
        }
    }
}
