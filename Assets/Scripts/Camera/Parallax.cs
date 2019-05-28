using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform player;
    public float scale = 0.6f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position * scale;
    }
}
