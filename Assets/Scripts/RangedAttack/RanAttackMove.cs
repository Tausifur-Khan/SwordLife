using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Knight
{
    public class RanAttackMove : MonoBehaviour
    {
        public float rSpeed = 2;
        

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector2.right * rSpeed * Time.deltaTime);
        }
    }
}