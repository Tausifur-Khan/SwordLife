using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOptions : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        if (FindObjectsOfType<SetOptions>().Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
