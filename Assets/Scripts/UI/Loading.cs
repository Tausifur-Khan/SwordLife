using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{

    public class Loading : MonoBehaviour
    {
        public GameObject loadingUI;
        public Image loadingBar;
        public float loadSpeed = 0.0f;
        AsyncOperation async;
        public int scene;


        private void Start()
        {
            loadingUI.SetActive(false);
        }

        public void StartLoad()
        {
            StartCoroutine(LoadingScreen());
        }

        IEnumerator LoadingScreen()
        {
            //Activate UI
            loadingUI.SetActive(true);
            //Load Level in background
            async = SceneManager.LoadSceneAsync(scene);
            //dont which to new scene even when level is loaded
            async.allowSceneActivation = false;

            //while loop
            //loop bool colndition if sync is still false
            while (async.isDone == false)
            {
                loadingBar.fillAmount += Time.deltaTime * loadSpeed;
                //if async progress has reach 0.9 then....
                if (async.progress == 0.9f && loadingBar.fillAmount == 1)
                {
                    //set loading image to 1
                    //loadingBar.fillAmount = 1;
                    //activate scene
                    async.allowSceneActivation = true;
                }
                //return no value
                yield return null;
            }
        }
    }
}