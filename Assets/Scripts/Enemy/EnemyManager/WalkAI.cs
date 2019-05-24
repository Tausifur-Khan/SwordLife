using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAI : Enemy
{
    #region variables
    #region difficulty
    [Header("AI Movement Modes")]
    public Difficulty difficulty; //Which AI to use
    private Difficulty tempDifficulty; //Which AI to go back to after pausing to attack
    #endregion
    #region turning
    [Header("Turning")]    
    public bool dontLook = false; //look & looksign are always 1
    public float look = 1; //x distance from player (can be negative)
    public int looksign = 1; //x direction to look at the player
    public LayerMask notme;
    [Space]
    public float wanderDelay; //Ranged enemy idle delay
    private float wanderCounter; //Increment above
    public bool wander; //Ranged enemy idle or idly wandering
    public bool angery; //Stopping to attack
    public bool redkoopa; //Turning on ledge
    #endregion
    #region attack and jump
    [Header("AI Attack Modes")]
    public bool shoot;            //can you switch from moving to idle attack 
    public bool shootMelee;      //Activate attack when player is nearby
    public bool patrol = false;
    public bool rush, rush2;           //Alternate melee
    public float shootDelay;   //switch to idle attack delay & counter
    private float counterv2;  //Increment above
    public float shotDelay;  // Idle attack delay & counter
    private float counter;  //Increment above
    [Header("Jump Options")]
    public bool autoJump; //Jump whenever near a ledge
    public float foreSight; //Distance to ledge
    public float jumpPoint; //something to do with autojump
    public float jumpHeight; //Not too accurate but there about
    public bool alwaysJump; //self explanatory
    #endregion
    #region Local Components
    [Header("Local Components")]
    private Rigidbody2D rigid2D;
    public Animator anim;
    private SpriteRenderer rend;
    #endregion
    #region World Components
    [Header("World Components")]
    private Transform player; //using the default "Player" tag
    public GameObject bulletPrefab; //Optional ranged attack
    //public GameObject corpsePrefab; //No longer used; ignore this

    //Empty gameobjects attached to this.gameObject
    public Transform shootPoint; //not used... yet
    public Transform meleePoint; //maybe used idk
    #endregion
    #endregion
    void Start()
    {
        HP = maxHP;
        counter = shotDelay; //Reset counters
        counterv2 = shootDelay;
        rigid2D = GetComponent<Rigidbody2D>(); //Reference
        rend = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dontLook) //Always turn to the player
        {
            look = player.transform.position.x - transform.position.x;
            looksign = Mathf.RoundToInt(Mathf.Sign(look));
        }
        if (redkoopa && isGrounded && (patrol || wander))
        {
            if (!Physics2D.Raycast((Vector2)transform.position + Vector2.right * 0.5f * looksign + (transform.localScale.y / 2 * Vector2.down), Vector2.down, .42f, notme))
            {
                looksign = -looksign;
            }
        }
        if (Physics2D.Raycast((Vector2)transform.position + (transform.localScale.y / 2 * Vector2.down), Vector2.down, .42f, notme)) //if you are grounded
        {
            if (alwaysJump) rigid2D.velocity = new Vector3(rigid2D.velocity.x + looksign * Acceleration, jumpSpeed); //If the enemy is spamming the jump button
            if (autoJump && isGrounded) //If autojump and ???
            {
                RaycastHit2D contact1 = Physics2D.Raycast((Vector2)transform.position + (transform.localScale.y / 2 * Vector2.down), Vector2.right * looksign, foreSight, notme); //raycast forward and down

                if (contact1)
                {
                    //Debug.Log("Laggy as fuck");
                    RaycastHit2D[] contact2 = Physics2D.RaycastAll(contact1.point + new Vector2(0.02f, 0), Vector2.up, jumpHeight + 0.42f, notme); //Go up
                    for (int i = 1; i < contact2.Length; i++) //For each object hit up
                    {
                        //Debug.Log(contact2[i].collider.name + (contact2[i].point.y - contact2[i - 1].point.y));
                        if (AutoJumpRaycast(contact2[i].point, contact2[i - 1].point)) //If the space between objects is larger than this object
                        {
                            Debug.Log("Can jump");
                            rigid2D.velocity = new Vector3(rigid2D.velocity.x + looksign * Acceleration, jumpSpeed);
                            break;
                        }
                        if (i == contact2.Length - 1) //If you run out of objects, test the highest possible point
                        {
                            if (AutoJumpRaycast(new Vector2(contact2[i].point.x, transform.position.y + jumpHeight), contact2[i].point))
                            {
                                Debug.Log("Can jump");
                                rigid2D.velocity = new Vector3(rigid2D.velocity.x + looksign * Acceleration, jumpSpeed);
                                break;
                            }
                        }
                    }
                }
            }
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
        else
        {
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
        if (difficulty != Difficulty.None) //If you are not stationary
        {
            rend.flipX = (looksign == 1);
            transform.GetChild(0).localScale = new Vector2(-looksign, transform.GetChild(0).localScale.y);
            if (shoot && counterv2 <= 0) //timer shoot
            {
                dontLook = true; //Switch to settings needed for difficulty.none
                anim.Play("Attack");
                tempDifficulty = difficulty;
                difficulty = Difficulty.None;
                counter = shotDelay;
            }
        }
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < 6)
        {
            if (shootMelee)
            {
                if (counterv2 > 1) counterv2 -= Time.deltaTime;
                else
                {
                    dontLook = false;
                    if (Vector2.Distance(player.transform.position, transform.position) < 1.4f) //If player is 2 units nearby
                    {
                        counterv2 = -1; //attack
                    }
                }
            }
            else
            {
                dontLook = false;
                counterv2 -= Time.deltaTime; //wait until attack
            }
            patrol = false;
        }
        else
        {
            patrol = true;
        }
        if(HP <= 0)
        {
            anim.Play("Death");
            Destroy(gameObject, 0.5f);
        }
        switch (difficulty)
        {
            case Difficulty.None: //Stationary
                counter -= Time.deltaTime;
                if (counter <= 0)
                {
                    counter = shotDelay;
                    if (bulletPrefab) //If there is a bullet etc to instantiate
                    {
                        if (wander) //This will be expanded upon in robot area
                        {
                            StartCoroutine(AnimationDelay(1.2f)); //delay for animation
                            counter += 1;
                        }
                        else
                        {
                            StartCoroutine(AnimationDelay(0.1f)); //delay for animation
                        }
                    }
                    //bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * looksign, 1, 1);
                    if (rush)
                    {
                        counter = shotDelay;
                        rush2 = true;
                    }
                    else if (shoot)
                    {
                        counterv2 = shootDelay;
                        difficulty = tempDifficulty;
                    }
                    else //if the animation hasnt already played and you want to stay in this form
                    {
                        anim.Play("Attack");
                    }
                }
                if (rush2)
                {
                    if (Mathf.Abs(transform.position.x - player.transform.position.x) > 6)
                        rigid2D.velocity += (new Vector2(looksign * Acceleration, 0));
                    anim.SetBool("isMoving", true);
                }
                break;
            case Difficulty.Easy: //Patrol
                dontLook = true;
                if (isGrounded) rigid2D.velocity += (new Vector2(looksign * Acceleration, 0));
                //Debug.DrawRay((Vector2)transform.position + (transform.localScale.x / 2 * Vector2.left + (Vector2.down * 0.3f)), Vector2.left);
                if (Physics2D.Raycast((Vector2)transform.position + (transform.localScale.x / 2 * Vector2.left) + (Vector2.down * 0.3f), Vector2.left, .1f, notme))
                {
                    //Debug.Log(Physics2D.Raycast((Vector2)transform.position + (transform.localScale.x / 2 * Vector2.left), Vector2.left, .1f, notme).collider);
                    looksign = 1;
                }
                if (Physics2D.Raycast((Vector2)transform.position + (transform.localScale.x / 2 * Vector2.right + (Vector2.down * 0.3f)), Vector2.right, .1f, notme))
                {
                    looksign = -1;
                }
                if (!patrol) difficulty = Difficulty.Medium;
                break;
            case Difficulty.Medium: //Follow
                if (isGrounded && Mathf.Abs(look) > .3f)
                {
                    rigid2D.velocity += (new Vector2(looksign * Acceleration, 0));
                    anim.SetBool("isMoving", true);
                }
                else anim.SetBool("isMoving", false);
                if (patrol) difficulty = Difficulty.Easy;
                break;
            case Difficulty.Hard: //Follow and jump
                if (isGrounded)
                {
                    if (Mathf.Abs(look) > .3f) { rigid2D.velocity += (new Vector2(looksign * Acceleration, 0)); anim.SetBool("isMoving", true); }
                    else anim.SetBool("isMoving", false);
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        rigid2D.AddForce(new Vector3(0, jumpSpeed), ForceMode2D.Impulse);
                    }
                }
                break;
            case Difficulty.Wander: //Ranged
                if (isGrounded)
                {
                    RaycastHit2D item = Physics2D.Raycast(transform.position, Vector2.right * looksign, foreSight, notme);
                    Debug.Log(item.collider);
                    if (item)
                    {
                        if (item.transform == player)
                        {
                            difficulty = Difficulty.None;
                            dontLook = false;
                            //shoot = true;
                        }
                    }
                    wanderCounter -= Time.deltaTime;
                    if (wanderCounter <= 0)
                    {
                        wanderCounter = wanderDelay + Random.Range(-wanderDelay * 0.2f, wanderDelay * 0.2f);
                        wander = !wander;
                        if (wander) looksign = -looksign;
                    }
                    if (wander) rigid2D.velocity += (new Vector2(looksign * Acceleration, 0));
                }
                break;
            default:
                break;
        }
    }
    IEnumerator AnimationDelay(float t)
    {
        yield return new WaitForSeconds(t);
        GameObject bullet = Instantiate(bulletPrefab, null, false);
        bullet.transform.position = shootPoint.position;
        foreach (simpleMove simple in bullet.GetComponentsInChildren<simpleMove>())
        {
            simple.moveSpeed *= -looksign;
        }
    }
    private void LateUpdate()
    {
        rigid2D.velocity = new Vector2(Mathf.Clamp(rigid2D.velocity.x, -Speed, Speed), rigid2D.velocity.y); //Limit velocity
        //transform.localScale = new Vector2(-look, transform.localScale.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackZone")) //knockback
        {
            HP--;

            float knock = -Mathf.Sign(collision.transform.position.x - transform.position.x) * knockback;
            rigid2D.AddForce(new Vector3(knock, knockback), ForceMode2D.Impulse);
        }
        else
        {
            //dir = -dir;
        }
    }
    bool AutoJumpRaycast(Vector2 P1, Vector2 P2)
    {
        if ((P1.y - P2.y) > 2)
        {
            RaycastHit2D contact3 = Physics2D.Raycast(P1 + new Vector2(0, -0.02f), Vector2.down, 40 + .42f, notme);
            if ((P1.y - contact3.point.y) > 2)
            {
                Debug.DrawLine(P1, contact3.point, Color.magenta);
                return true;
            }
        }
        return false;
    }
}
public enum Difficulty
{
    None,
    Easy,
    Medium,
    Hard,
    Wander
}
//if (Input.GetKey(KeyCode.O))
//{
//    GameObject corpse = Instantiate(corpsePrefab, transform.position, transform.rotation);
//    foreach (Rigidbody2D rb in corpse.GetComponentsInChildren<Rigidbody2D>())
//    {
//        rb.AddForce(new Vector2(Random.Range(-1, 1) * 1.5f, Random.Range(1, 3) * 1.5f), ForceMode2D.Impulse);
//    }
//    Destroy(gameObject);
//}
//
//Used in contact1:
//Vector2Rect transfer = new Vector2Rect();
//transfer.topLeft = (Vector2)transform.position + (transform.localScale.y / 2 * Vector2.down);
//transfer.bottomRight = transfer.topLeft + new Vector2(foreSight, -.42f);
//if (Physics2D.OverlapBox(transfer.Center, transfer.Size,0, notme))
//{
//    rigid2D.velocity = new Vector3(rigid2D.velocity.x + looksign * Acceleration, jumpSpeed);
//}
//IEnumerator TriggerOnGround()
//{
//    while (!isGrounded)
//    {
//        yield return new WaitForEndOfFrame();
//    }
//    if (difficulty != Difficulty.None) anim.SetTrigger("isAttacking");
//    yield return new WaitForEndOfFrame();
//}
//void OnDrawGizmos()
//{
//    Gizmos.color = Color.magenta;
//    Vector2 pos = (Vector2)transform.position + (transform.localScale.y / 2 * Vector2.down);
//    Gizmos.DrawLine(pos, pos + Vector2.down * .02f);
//}