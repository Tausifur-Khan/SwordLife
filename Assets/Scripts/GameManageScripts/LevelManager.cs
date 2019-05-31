using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public UI.Loading load;

   

    private void Start()
    {
        load = GetComponent<UI.Loading>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            load.StartLoad();
            
            //DontDestroyOnLoad(player);
        } 
        
    }

 
}
