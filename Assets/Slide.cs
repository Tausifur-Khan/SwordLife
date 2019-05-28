using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    private float offsetCount = 0;
    private bool turnOff; //
    public float offset = 3;
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void UpdateTurn()
    {
        bool flipX = !transform.GetComponentInParent<SpriteRenderer>().flipX;
        if (flipX) offsetCount += speed * Time.deltaTime;
        else offsetCount -= speed * Time.deltaTime;
        offsetCount = Mathf.Clamp(offsetCount, -offset, offset);
        transform.localPosition = offsetCount * Vector2.right;
    }
}
