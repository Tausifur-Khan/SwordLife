using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change : MonoBehaviour
{
   
    public SpriteRenderer sRender;
    public float curHp;
    public float maxHp;

    public Slider enemyHp;

    public GameObject enemySlider;
    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        curHp = Mathf.Abs(maxHp);
        enemySlider.SetActive(false);
    }

    

    void LateUpdate()
    {
        enemyHp.value = curHp;
        EnemyDeath();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "AttackZone")
        {
            Debug.Log("Getting Attack: ");
            enemySlider.SetActive(true);
            ChangeColor();
            curHp -= 20f;
        }

        else if (col.gameObject.CompareTag("RangeZone"))
        {
            enemySlider.SetActive(true);
            ChangeColor();
            curHp -= 100f;
        }
    }

    void ChangeColor()
    {
        //change material color of gameobject
        sRender.material.color = Random.ColorHSV();
    }

    void EnemyDeath()
    {
        if (curHp <= 0)
        {
           // Destroy(gameObject);
            //enemySlider.SetActive(false);
            curHp = maxHp;
        }
    }

}
