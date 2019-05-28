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
    Knight.RangeTimer rangeTime;
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


    private void LateUpdate()
    {
        UITimers();
       

    }

    void UITimers()
    {
        //dashTimeTxt.text = charC.dashDelay.ToString("0");
        RangeTimeTxt.text = rangeTime.timer.ToString("Range Time: 0");
    }

    //void ResetOnDeath()
    //{
    //    Scene curScene = SceneManager.GetActiveScene();
    //    SceneManager.LoadScene(curScene.name);
    //}




}
