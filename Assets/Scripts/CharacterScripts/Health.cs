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

        public bool playerDeath;
        #endregion
        [Space(3)]

        [Header("Components")]
        [SerializeField]
        private Image[] hearts;
        [SerializeField]
        private Sprite fullHp;
        [SerializeField]
        private Sprite emptyHp;
        
        public GameObject deathBody;
        public BoxCollider2D body;

       
        // private Animator deathAnim;

        private void Start()
        {
            deathBody = GameObject.Find("DeathBox");
            deathBody.SetActive(false);

            body = GetComponent<BoxCollider2D>();

            curHp = maxHp;

            fullHp = Resources.Load<Sprite>("PlayerHp/Hp");
            emptyHp = Resources.Load<Sprite>("PlayerHp/EmptyHp");
            
            

        }

        private void Update()
        {
            HpSystem();

        }

        void HpSystem()
        {



            //for loop of all heart sprites
            for (int i = 0; i < hearts.Length; i++)
            {
                #region health loss
                //if curent hp is less then heart length then..
                //heart sprite will be full
                if (i < curHp)
                    hearts[i].sprite = fullHp;
                //otherwise empty full heart
                else
                    hearts[i].sprite = emptyHp;
                #endregion

                #region Amount of hearts
                //if heart length is less than max amount then...
                //allow hearts to be shown
                if (i < maxHp)
                {
                    hearts[i].enabled = true;
                    
                }
                //othwerwise disable any hearts above maximum
                else
                    hearts[i].enabled = false;


                #endregion
            }
        }

        private void LateUpdate()
        {
            PlayerDmg();


        }

        void PlayerDmg()
        {
            if (curHp <= 0 && !playerDeath)
            {
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
            if (col.CompareTag("Damage") && GetComponentsInChildren<BoxCollider2D>(false).Length == 1 && col.gameObject.layer != 15)
            {
                curHp--;
            }
            if (col.CompareTag("Killzone"))
            {
                curHp = 0;
            }
        }

    }
}