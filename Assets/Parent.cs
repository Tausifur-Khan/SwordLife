using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    private Transform player;
    public float deactivateDistance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
        {
            if(child != transform)
            {
                child.gameObject.SetActive(Vector2.Distance(player.position, child.position) < deactivateDistance);
            }
        }
    }
}
