using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOptions : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        UpdateVariables();
        //if (FindObjectsOfType<SetOptions>().Length > 1) Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void UpdateVariables()
    {
        Screen.SetResolution(Screen.resolutions[PlayerPrefs.GetInt("Resolution")].width, Screen.resolutions[PlayerPrefs.GetInt("Resolution")].height, PlayerPrefs.GetInt("FullScreen") == 1);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        switch (PlayerPrefs.GetInt("KeyBinding"))
        {
            case 0:
                player.GetComponent<Knight.PlayerController>().left = KeyCode.A;
                player.GetComponent<Knight.PlayerController>().right = KeyCode.D;
                player.GetComponent<Knight.PlayerController>().jump = KeyCode.W;
                player.GetComponent<Knight.PlayerController>().dash = KeyCode.Space;
                player.GetComponent<Knight.Attack>().attack = KeyCode.I;
                break;
            case 1:
                player.GetComponent<Knight.PlayerController>().left = KeyCode.LeftArrow;
                player.GetComponent<Knight.PlayerController>().right = KeyCode.RightArrow;
                player.GetComponent<Knight.PlayerController>().jump = KeyCode.Z;
                player.GetComponent<Knight.PlayerController>().dash = KeyCode.LeftShift;
                player.GetComponent<Knight.Attack>().attack = KeyCode.X;
                break;
        }

    }
}
