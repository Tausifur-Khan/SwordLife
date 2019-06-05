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
               
                //if async progress has reach 0.9 then....
                if (async.progress == 0.9f )
                {
                    yield return new WaitForSeconds(5f);
                   
                    //activate scene
                    async.allowSceneActivation = true;
                }
                //return no value
                yield return null;
            }
        }
    }
}