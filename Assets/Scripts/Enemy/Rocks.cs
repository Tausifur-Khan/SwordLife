using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public float range;
    public GameObject rockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Spawn()
    {
        Vector3 rand = transform.position + (Random.Range(-range, range) * Vector3.right);
        Instantiate(rockPrefab, rand, Quaternion.identity, null);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position - range * Vector3.right, transform.position + range * Vector3.right);
    }
}
