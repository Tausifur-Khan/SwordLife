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
        public int lives;
        public bool useLifestone;
        public bool playerDeath;
        #endregion
        [Space(3)]

        [Header("Components")]
        public Image[] hearts;
        public Sprite fullHp;
        public Sprite emptyHp;

        public Text liveTxtUI;
        
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
            //for loop of all heart sprites
            for (int i = 0; i < hearts.Length; i++)
            {
                #region health loss
                //if curent hp is less then heart length then..
                //heart sprite will be full
                if (i < curHp) hearts[i].sprite = fullHp;
                //otherwise empty full heart
                else hearts[i].sprite = emptyHp;
                #endregion

                #region Amount of hearts
                //if heart length is less than max amount then...
                //allow hearts to be shown
                if (i < maxHp) hearts[i].enabled = true;
                //othwerwise disable any hearts above maximum
                else hearts[i].enabled = false;
                #endregion
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
                //curHp = maxHp;
                playerDeath = true;

                PlayerController player = GetComponent<PlayerController>();
                player.keyActive = false;

                body.isTrigger = true;

                deathBody.SetActive(true);
                if (useLifestone)
                {
                    //fill.SetActive(false);
                    lives--;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Damage") && GetComponentsInChildren<BoxCollider2D>(false).Length == 1)
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