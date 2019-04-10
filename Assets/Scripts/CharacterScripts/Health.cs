using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    #region Hp Variables
    [Header("Hp Stuff")]
    //current player hp
    public float curHp;
    //maximum player hp
    public float maxHp;

    //slider array for hp
    public Slider hpSlider;

    public Text liveTxtUI;
    private string liveTxt;

    public GameObject fill;


    #endregion

    private void Start()
    {
        //set current player hp to the maximum
        curHp = maxHp;
        liveTxt = "1";
    }

    private void Update()
    {
        hpSlider.value = curHp;
        liveTxtUI.text = liveTxt;
    }

    private void LateUpdate()
    {
        PlayerDmg();
    }

    void PlayerDmg()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            curHp = 0;
            fill.SetActive(false);
            liveTxt = "0";
        }
    }
}
