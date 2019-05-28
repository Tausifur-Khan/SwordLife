using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suprise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>(true))
        {
            t.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
