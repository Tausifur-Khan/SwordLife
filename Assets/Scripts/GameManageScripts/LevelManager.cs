using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] vortex = new GameObject[1];

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == vortex[0])
        {
            SceneManager.LoadScene(1);
        }
    }
}
