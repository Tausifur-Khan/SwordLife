using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnExit : MonoBehaviour
{
  void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
