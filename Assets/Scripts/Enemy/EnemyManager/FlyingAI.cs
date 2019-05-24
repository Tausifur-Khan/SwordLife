using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAI : Enemy
{
    public LayerMask notplayer; //default only
    public LayerMask notme; //everything except enemy
    public FlyingType type;
    public FlyingType waitType;
    private float swoopPoint = 4; //swooper
    public bool trackPlayer; //swooper & medusa head
    public Vector2 moveSpeed; //should be all but not really
    public float foresight = 4; //where to start moving
    public float attackSpeed = 1; //above
    private float attackCounter; //above
    private Vector2 origin; //multiple use cases
    private Transform player;
    private Rigidbody2D rigid2D;
    public Animator anim;
    private ParticleSystem psOptional; //unused
    // Use this for initialization
    void Start()
    {
        HP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid2D = GetComponent<Rigidbody2D>();
        psOptional = GetComponent<ParticleSystem>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Mathf.Abs(origin.x - transform.position.x);
        switch (type)
        {
            case FlyingType.wait:
                diff = (player.position.x - transform.position.x);
                if (Mathf.Abs(diff) < foresight)
                {
                    moveSpeed.x *= Mathf.Sign(diff);
                    origin = transform.position;
                    type = waitType;
                }
                break;
            case FlyingType.medusahead:
                transform.position = origin + Mathf.Sin(origin.x) * moveSpeed.y * Vector2.up;
                origin.x += moveSpeed.x * Time.deltaTime;
                if (trackPlayer)
                {
                    origin.y += Mathf.Sign(player.position.y - transform.position.y) * Time.deltaTime;
                }

                break;
            case FlyingType.swooper:
                transform.position += (Vector3)moveSpeed * Time.deltaTime;
                if (moveSpeed.y < 0)
                {
                    if (trackPlayer)
                    {
                        if (Mathf.Abs(player.position.y - transform.position.y) < 1)
                        {
                            moveSpeed.y += .2f;
                        }
                    }
                    if (diff > swoopPoint)
                    {
                        moveSpeed.y += .2f;
                    }
                }
                break;
            case FlyingType.spelunky:
                if (!rigid2D) { Debug.LogError("Error: Rigidbody required for spelunky AI"); return; }
                Vector2 _dir = player.position + new Vector3(0, 0.5f) - transform.position;
                _dir = _dir.normalized * moveSpeed.x;
                rigid2D.velocity = _dir;
                //        rigid2D.velocity = new Vector2(Mathf.Clamp(rigid2D.velocity.x, -moveSpeed.x, moveSpeed.x), rigid2D.velocity.y);
                break;
            case FlyingType.nonborked:
                Vector3 vectorToTarget = player.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * .5f);
                transform.position += transform.right * Time.deltaTime;
                break;
            case FlyingType.above:
                if (Mathf.Abs(transform.position.x - player.position.x) < 12)
                {
                    Vector3 target = Physics2D.Raycast(player.position, Vector2.up, 100, notplayer).point + GetComponent<CircleCollider2D>().radius * Vector2.down * 0.2f;

                    if (target == new Vector3(0.0f, -0.4f, 0.0f)) target = player.position + 90 * Vector3.up;
                    target.y = Mathf.Clamp(player.position.y + 5.5f, player.position.y + 5.5f, target.y - 1);
                    target.y = Mathf.Clamp(target.y, player.position.y + 2, player.position.y + 5.5f);
                    Vector3 dir = (target - transform.position).normalized * Time.deltaTime * moveSpeed.x;
                    if (Mathf.Abs(player.position.x - transform.position.x) < .6f && transform.position.y > player.position.y)
                    {
                        if (Mathf.Abs(player.position.x - transform.position.x) < .2f) dir.x = 0;
                        attackCounter += Time.deltaTime;
                    }
                    else if (attackCounter > 0)
                    {
                        attackCounter -= Time.deltaTime * 0.75f;
                    }
                    if (attackCounter >= attackSpeed)
                    {
                        Debug.Log("ATTACK!!!");
                        gameObject.tag = "Damage";
                        attackCounter = 0;
                        origin = transform.position;
                        type = FlyingType.fall;
                        anim.SetInteger("attackState", 1);
                    }
                    transform.position += dir;
                    GetComponent<SpriteRenderer>().flipX = Mathf.Sign(player.position.x - transform.position.x) == 1;
                }
                break;
            case FlyingType.fall:
                if (moveSpeed.y == Mathf.Abs(moveSpeed.y)) transform.position += moveSpeed.y * Vector3.up * Time.deltaTime * 0.5f;
                else transform.position += moveSpeed.y * Vector3.up * Time.deltaTime;
                if (Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 0.2f).Length > 1)
                {
                    anim.SetInteger("attackState", 2);
                    moveSpeed.y = Mathf.Abs(moveSpeed.y);
                }
                if (transform.position.y > origin.y)
                {
                    gameObject.tag = "Enemy";
                    type = FlyingType.above;
                    anim.SetInteger("attackState", 0);
                    moveSpeed.y = -Mathf.Abs(moveSpeed.y);
                }
                break;
            default:
                break;
        }
        if (HP <= 0)
        {
            anim.Play("Death");
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && rigid2D)
        {
            float knock = -Mathf.Sign(collision.transform.position.x - transform.position.x) * knockback;
            rigid2D.AddForce(new Vector3(knock, knockback), ForceMode2D.Impulse);
        }
        else
        {
            //Mathf.Abs(x)/x = Mathf.Sign(x)
            //dir = -dir;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackZone")) //knockback
        {
            damaged();
        }
    }
    public void damaged()
    {
        HP--;
        gameObject.tag = "Enemy";
        anim.SetInteger("attackState", 0);
        anim.Play("Flying");
        moveSpeed.y = Mathf.Abs(moveSpeed.y);
        moveSpeed.x *= -Mathf.Sign(player.position.x - transform.position.x);
        type = FlyingType.swooper;
        Invoke("notanymore", 1.5f);
    }
    private void notanymore()
    {
        moveSpeed.y = -Mathf.Abs(moveSpeed.y);
        type = FlyingType.above;
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log(other.tag);
    //    if (other.CompareTag("Player"))
    //    {
    //        float knock = -Mathf.Sign(other.transform.position.x - transform.position.x) * knockback;
    //        rigid2D.AddForce(new Vector3(knock, knockback), ForceMode2D.Impulse);
    //    }
    //    else
    //    {
    //        //Mathf.Abs(x)/x = Mathf.Sign(x)
    //        //dir = -dir;
    //    }
    //}
    public enum FlyingType
    {
        wait,
        medusahead, //(castlevania)         reference: https://www.youtube.com/watch?v=82y2z-vdNXA
        swooper, //(SMW)                    reference: https://www.youtube.com/watch?v=nSuJzFLH90U
        spelunky, //(I kinda borked it tho) reference: https://www.youtube.com/watch?v=9eFaPgP6BPo
        nonborked, //no idea
        above, //flying thwomp
        fall //falling thwomp
    }
}
