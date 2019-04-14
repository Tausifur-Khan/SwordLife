using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

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

    //take damage
    public float dmg;
    #endregion

    private void Start()
    {
        //set current player hp to the maximum
        curHp = Mathf.Abs(maxHp);
        liveTxt = "1";
    }

    private void LateUpdate()
    {
        PlayerDmg();
        hpSlider.value = curHp;
        liveTxtUI.text = liveTxt;
    }

    void PlayerDmg()
    {
        if (curHp <= 0)
        {
            curHp = 0;
            fill.SetActive(false);
            liveTxt = "0";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            curHp -= dmg;
        }
    }

    void AnimationSetup()
    {
        
    }
}
