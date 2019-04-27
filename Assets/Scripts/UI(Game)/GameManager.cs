using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Data can be converted
[System.Serializable]
public class GameData
{
    public int lives;
}

public class GameManager : MonoBehaviour
{
    //public Text dashTimeTxt;
    public Text RangeTimeTxt;

    // public Knight.CharacterController charC;
    public Knight.RangeTimer rangeTime;
    public Knight.Health playerLives;

    #region SaveData
    //Reference to Class
    public GameData data = new GameData();
    //Save Data name
    public string fileName = "GameData";
    #endregion
    //private float rounded;

    void SavaData()
    {

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
}
