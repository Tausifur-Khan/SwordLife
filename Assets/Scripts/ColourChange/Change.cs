
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knight
{
    public class Change : MonoBehaviour
    {
        public SpriteRenderer sRender;
       
       public Attack playerAttack;

        // Start is called before the first frame update
        void Start()
        {
            sRender = GetComponent<SpriteRenderer>();
          
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name == "AttackZone")
            {
                Debug.Log("Getting Attack: ");
               
                ChangeColor();
              
                
            }
        }
    
        void ChangeColor()
        {
            //change material color of gameobject
            sRender.material.color = Random.ColorHSV();
        }

       

    }
}