using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;


namespace Knight
{
    public class Health : MonoBehaviour
    {
        [Header("New Health")]
        #region New Health System
        public int curHp;
        public int maxHp;
        
        //player lives
        public int lives = 3;

        public bool playerDeath;
        #endregion
        [Space(3)]

        [Header("Components")]
        public Image[] hearts = new Image[5];
        public Sprite fullHp;
        public Sprite emptyHp;

        public Text liveTxtUI;

        public GameObject sliderFill;


        public GameObject deathBody;
        public BoxCollider2D body;

      

        // private Animator deathAnim;

        private void Start()
        {
            deathBody = GameObject.Find("DeathBox");
            deathBody.SetActive(false);
           
            body = GetComponent<BoxCollider2D>();

            curHp = maxHp;
        }

        private void Update()
        {
            HpSystem();
        }

        void HpSystem()
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if(i < curHp)
                {
                    hearts[i].sprite = fullHp;

                }
                else
                {
                    hearts[i].sprite = emptyHp;
                }

                if (i < maxHp)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }

        private void LateUpdate()
        {
            PlayerDmg();
            
            liveTxtUI.text = lives.ToString();
        }

        void PlayerDmg()
        {
            if (curHp <= 0 && !playerDeath)
            {

                //fill.SetActive(false);
                lives--;
                //curHp = maxHp;
                playerDeath = true;

                PlayerController player = GetComponent<PlayerController>();
                player.keyActive = false;
                
                body.isTrigger = true;

                deathBody.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Damage") && GetComponentsInChildren<BoxCollider2D>(false).Length == 1)
            {
               curHp --;
            }
            if (col.CompareTag("Killzone"))
            {
                curHp = 0;
            }
        }

    }
}