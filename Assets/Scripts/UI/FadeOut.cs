using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{

    public CanvasGroup fadeOutImage;

    public float displayImageDur = 0f;
    public float timer;

    public bool isFading;

    private void Start()
    {
        isFading = true;

        fadeOutImage = GetComponentInChildren<CanvasGroup>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isFading)
        {
            FadeTimer();
        }

        if(displayImageDur <= 0f)
        {
            isFading = false;
            displayImageDur = 0;
        } 
    }

    void FadeTimer()
    {
            //Start timer
            displayImageDur -= Time.deltaTime;

            fadeOutImage.alpha = displayImageDur;
        
    }
}
