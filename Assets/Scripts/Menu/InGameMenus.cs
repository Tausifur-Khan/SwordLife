using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenus : MonoBehaviour
{

    #region Taus Edits for pause & death
    public bool toogleDeath;
    public bool tooglePause;
    public GameObject pause;
    public GameObject death;

    public Knight.Health playerLife;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        playerLife = Object.FindObjectOfType<Knight.Health>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Pause();

    }

    private void LateUpdate()
    {
        MenuOnDeath();

    }

    #region Pause & Death
    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.SetActive(true);
            tooglePause = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void UseLife(int curScene)
    {
        // playerLife.useLifestone = true;
        SceneManager.LoadScene(curScene);

    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void MenuOnDeath()
    {
        Knight.Health player = FindObjectOfType<Knight.Health>();
        if (player.playerDeath == true)
        {
            death.SetActive(true);
        }
    }
    #endregion
}
