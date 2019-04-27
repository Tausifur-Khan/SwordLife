using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

namespace Knight
{
    public class Death : MonoBehaviour
    {
        public bool death;
        private Vector2 origin;

        private Animation anim;

        // Start is called before the first frame update
        void Start()
        {
            origin = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Killzone"))
            {
               
                gameObject.transform.position = origin;
            }
        }

    }
}