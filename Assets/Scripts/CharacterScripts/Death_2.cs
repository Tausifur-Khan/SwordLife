using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Knight
{
    public class Death_2 : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Killzone"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}