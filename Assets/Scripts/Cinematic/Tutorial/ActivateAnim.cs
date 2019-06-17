using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AnimationSet
{
    public class ActivateAnim : MonoBehaviour
    {
        [SerializeField]
        Knight.PlayerController player;

        [SerializeField]
        GameObject wizardAnim;


        private void Start()
        {
            wizardAnim.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //activate animation with bool
                
                wizardAnim.SetActive(true);
                //disable player movement with bool
                player.restrictMove = true;

            }
        }

       
    }
}