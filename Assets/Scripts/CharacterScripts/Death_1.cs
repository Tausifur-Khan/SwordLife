using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Knight
{
    public class Death_1 : MonoBehaviour
    {
        public bool death;
        private Vector2 origin;

        // Start is called before the first frame update
        void Start()
        {
            origin = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Killzone"))
            {
                death = true;
                gameObject.transform.position = origin;
            }
        }

    }
}