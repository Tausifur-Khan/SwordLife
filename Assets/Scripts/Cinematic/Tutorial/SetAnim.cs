using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationSet
{
    public class SetAnim : MonoBehaviour
    {
        [SerializeField]
        Knight.PlayerController player;

        [SerializeField]
        GameObject wizAnim;

        public void DisableAnim()
        {
            player.restrictMove = false;
            wizAnim.SetActive(false);
        }

    }
}