using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] screens;
    public GameObject[] layouts;
    public Text layoutName;
    public Text graphicsText;
    public int layout;
    public GameObject areyousure;
    public int rustyOptionsID;
    public int graphicsCountdown;
    public GameObject commonOptions;
    public Vector2[] screenLayout;
    public Toggle fullScreen;
    public Toggle vsync;
    public Dropdown newYears;

    public int focus;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            newYears.options.Add(new Dropdown.OptionData(Screen.resolutions[i].width + " x " + Screen.resolutions[i].height));
        }
        ResetToPrefs();
    }
    private void Update()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            ScreenMove(screens[i].transform, screenLayout[i]);
        }
        ScreenMoveWorldSpace(transform, -screens[focus].transform.localPosition, true);
    }
    // Use this for initialization
    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    public void ChangeScreen(int activeScreen)
    {
        focus = activeScreen;
        if (focus == 2 || focus == 3 || focus == 4)
        {
            commonOptions.SetActive(true);
        }
        else commonOptions.SetActive(false);
        //if(activeScreen < 0)
        //{
        //    activeScreen = -activeScreen;

        //}
        //foreach (GameObject screen in screens)
        //{
        //    screen.SetActive(false);
        //}
        //screens[activeScreen].SetActive(true);
    }
    public void ImSure(bool areYouReally)
    {
        if (areYouReally) graphicsCountdown = 999;
        else graphicsCountdown = -999;
    }
    IEnumerator AreYouSure()
    {
        Screen.SetResolution(Screen.resolutions[newYears.value].width, Screen.resolutions[newYears.value].height, fullScreen.isOn);
        if (vsync.isOn) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
        while (true) // Oh no
        {
            if (graphicsCountdown > 900)
            {
                if (fullScreen.isOn) PlayerPrefs.SetInt("FullScreen", 1);
                else PlayerPrefs.SetInt("FullScreen", 0);
                if (vsync.isOn) PlayerPrefs.SetInt("VSync", 1);
                else PlayerPrefs.SetInt("VSync", 0);
                PlayerPrefs.SetInt("Resolution", newYears.value);
                areyousure.SetActive(false);
                ChangeScreen(1);
                break;
            }
            if (graphicsCountdown <= 0)
            {
                ResetToPrefs();
                Screen.SetResolution(Screen.resolutions[newYears.value].width, Screen.resolutions[newYears.value].height, fullScreen.isOn);
                areyousure.SetActive(false);
                break;
            }
            graphicsCountdown--;
            graphicsText.text = "Are you sure? (" + graphicsCountdown + ")";
            yield return new WaitForSeconds(1);
        }
    }
    public void SetOptions()
    {
        //areyousure.SetActive(false);
        switch (focus)
        {
            case 2:
                graphicsCountdown = 5;
                areyousure.SetActive(true);
                StartCoroutine(AreYouSure());
                break;
            case 3:
                PlayerPrefs.SetInt("KeyBinding", layout);
                ChangeScreen(1);
                break;
            case 4:
                break;
            default:
                break;
        }
    }
    public void ResetToDefault()
    {
        //areyousure.SetActive(false);
        switch (focus)
        {
            case 0:
                fullScreen.isOn = true;
                vsync.isOn = false;
                newYears.value = newYears.options.Count - 1;
                break;
            case 1:
                layout = 0;
                break;
            case 2:
                break;
            default:
                break;
        }
    }
    public void ChangeLayout(int incr)
    {
        layouts[layout].SetActive(false);
        layout += incr;
        if (layout >= layouts.Length) layout = 0;
        if (layout < 0) layout = layouts.Length - 1;
        layouts[layout].SetActive(true);
        layoutName.text = "Layout " + (layout + 1);
    }
    public void ResetToPrefs()
    {
        fullScreen.isOn = (PlayerPrefs.GetInt("FullScreen") == 1);
        newYears.value = PlayerPrefs.GetInt("Resolution", Screen.resolutions.Length);
        layout = PlayerPrefs.GetInt("KeyBinding", 0);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSync", 0);
        foreach (GameObject item in layouts)
        {
            item.SetActive(true);
        }
        layouts[layout].SetActive(true);
        layoutName.text = "Layout " + (layout + 1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void ScreenMove(Transform screen, Vector2 moveTo, bool lerp = false)
    {
        //Debug.LogWarning("Please use whole values when moving screen");
        Vector2 moveToWorld = new Vector2(moveTo.x * Screen.width, moveTo.y * Screen.height);
        if (lerp)
        {
            screen.localPosition = Vector2.Lerp(screen.localPosition, moveToWorld, .06f);
        }
        else screen.localPosition = moveToWorld;
    }
    private void ScreenMoveWorldSpace(Transform screen, Vector2 moveToWorld, bool lerp = false)
    {
        if (lerp)
        {
            screen.localPosition = Vector2.Lerp(screen.localPosition, moveToWorld, .1f);
        }
        else screen.localPosition = moveToWorld;
    }
}
