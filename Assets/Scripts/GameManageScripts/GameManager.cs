using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : MonoBehaviour
{
    #region Variables
    //public Text dashTimeTxt;
    public Text RangeTimeTxt;

    // public Knight.CharacterController charC;
 
    [SerializeField]
    Knight.Health playerSurv;
    [SerializeField]
    GameObject player;


   
    #endregion
    //private float rounded;

    private void Start()
    {
        //refernce player game object
        player = GameObject.FindGameObjectWithTag("Player");
        //Refernce health script
        playerSurv = (Knight.Health)player.GetComponent(typeof(Knight.Health));
    }
}
