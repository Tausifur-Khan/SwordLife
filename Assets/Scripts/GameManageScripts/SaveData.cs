using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveData : MonoBehaviour
{
    #region Variables
    [SerializeField]
    GameObject player;
    [SerializeField]
    Knight.Health playerLives;


    #region SaveData
    //Reference to Class
    public GameData data = new GameData();
    //Save Data name
    public string fileName = "GameData";
    #endregion

    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLives = (Knight.Health)player.GetComponent(typeof(Knight.Health));
    }

    private void Start()
    {
        SavaData();
       
    }

    void SavaData()
    {
        string json = JsonUtility.ToJson(data);

        string fullPath = Application.dataPath + "/Data/" + fileName + ".json";
        
        File.WriteAllText(fullPath, json);

        SaveDataType();
    }

    void SaveDataType()
    {
        data.lives = playerLives.lives;
    }


    //Data can be reconstructed
    [System.Serializable]
    public class GameData
    {
        public int lives;
    }
}
