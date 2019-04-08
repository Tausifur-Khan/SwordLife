using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    #region Variables
    public float magnitude = 0;
    private Vector2 origin;
    public Animator anim;
    #endregion
    // Use this for initialization
    private void Awake()
    {
        origin = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("Screen");
        }
        if (magnitude > 0)
        {
            transform.localPosition = origin + (Random.insideUnitCircle * transform.localScale * magnitude);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, magnitude);
    }
}
