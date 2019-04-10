using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change : MonoBehaviour
{
    public MeshRenderer mRender;
    public float curHp;
    public float maxHp;

    public Slider enemyHp;

    public GameObject enemySlider;
    // Start is called before the first frame update
    void Start()
    {
        mRender = GetComponent<MeshRenderer>();
        curHp = Mathf.Abs(maxHp);
    }

    void Update()
    {


    }

    void LateUpdate()
    {
        enemyHp.value = curHp;
        EnemyDeath();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AttackZone"))
        {
            ChangeColor();
            curHp -= 20f;
        }

        else if (col.gameObject.CompareTag("RangeZone"))
        {
            ChangeColor();
            curHp -= 100f;
        }
    }

    void ChangeColor()
    {
        //change material color of gameobject
        mRender.material.color = Random.ColorHSV();
    }

    void EnemyDeath()
    {
        if (curHp <= 0)
        {
            Destroy(gameObject);
            enemySlider.SetActive(false);
        }
    }

}
