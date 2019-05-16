
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class represents fighter 1, aka player 1.
public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float startTime;
    private float time;
    private float hitForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool punch;
    public bool tennishit;
    private int extraJumps;
    private int timesHit;
    public int extraValue;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode punchkey;

    private Animator anim;

    public Transform attackPos;
    public Transform attackheadpos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float airhitrange;

    public bool damage;
    private void Start()
    {
        tennishit = false;
        left = KeyCode.A;
        right = KeyCode.D;
        jump = KeyCode.W;
        punchkey = KeyCode.S;
        timesHit = 0;
        startTime = 1.5f;
        time = 0;
        punch = false;
        speed = 4;
        jumpForce = 5.5f;
        hitForce = 1f;
        extraJumps = extraValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    //Constantly checks if the player has touched the ground. From blackthornprod tutorial.
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

    }
    //If the player turns, flips the animation (blackthornprod.
    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void Update()
    {
      
        if (time <= 0)
        {
            anim.SetBool("Hit", false);
            //If the player is moving, check to see if he needs to be flipped.
            if (facingRight == false && rb.velocity.x < 0)
            {
                flip();
            }
            else if (facingRight == true && rb.velocity.x > 0)
            {
                flip();
            }
            anim.SetBool("airhit", false);
            
            anim.SetBool("Punch", punch);
            anim.SetBool("Grounded", isGrounded);
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            punch = false;

            if (isGrounded == true)
            {
                tennishit = true;
                extraJumps = extraValue;
            }
            if (Input.GetKey(left))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else if (Input.GetKey(right))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
            //This section allows the player to jump twice, but regards to isGrounded (blackthornprod).
            if (Input.GetKeyDown(jump) && extraJumps > 0)
            {

                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }
            else if (Input.GetKeyDown(jump) && extraJumps == 0 && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            //This section allows the player to perform a tennis hit, if he has first jumped twice and is not grounded.
            else if (Input.GetKeyDown(jump) && extraJumps == 0 && !isGrounded && tennishit)
            {
                anim.SetBool("airhit", tennishit);
                //Creates a circle area that grabs any player in reach and applies damage to them (blackthornprod).
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackheadpos.position, airhitrange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].name.Equals("fighter2"))
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(true, facingRight, tennishit);
                    }
                }
                tennishit = false;
        
            }
            if (Input.GetKeyDown(punchkey))
            {
                //This boolean is used to apply the sound effect in sounds.cs. It's used during an animation to always show
                //up at the right time.
                punch = true;
            }
            if (damage == true)
            {
                
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].name.Equals("fighter2"))
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage, facingRight, false);
                    }
                }
            }
        }
        else
        {
            //This section forces the player that is being from moving right after he has been punched.
            //That is, player shouldnt be able to disregard the force that is being applied by simply touching a move button.
            time -= Time.deltaTime;
            if (startTime - time > 0.2f && isGrounded)
            {
                //Reaches out to simpleton game manager to adjust the score.
                FindObjectOfType<GameManager>().p1hurt();
                time = 0;
            }
        }

    }
    //For visuals only when finding the circle that a player can be punched in (blackthornprod).
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(attackheadpos.position, airhitrange);
    }
    //Different sections of taking damage depending on what type of hit it is.
    public void TakeDamage(bool damage, bool direction, bool isTennishit)
    {


        if (!isTennishit)
        {
            rb.AddRelativeForce(Vector2.up * hitForce);
            if (!direction)
            {
                rb.AddRelativeForce(Vector2.right * hitForce * 2);
            }
            else
            {
                rb.AddRelativeForce(Vector2.left * hitForce * 2);
            }
        }
        else
        {
           
            rb.AddRelativeForce(Vector2.up * 2200);
            if (!direction)
            {
                rb.AddRelativeForce(Vector2.right * 500);
            }
            else
            {
                rb.AddRelativeForce(Vector2.left * 500);
            }
        }
        //If the player has been hit a bunch of times (not actually 40, but closer to 4) , then the next time he is hit
        //the flying force is increased.
        if (timesHit <= 40)
        {
            hitForce = hitForce + 1;
        }
        else
        {
            hitForce = hitForce * 1.1f;
        }
        timesHit = timesHit + 1;
        time = startTime;
        anim.SetBool("Hit", true);
        
    }

    //These are used to allow the player to stay on a moving platform (Alexander Zotov).
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("floor"))
        {
            this.transform.parent = collision.transform;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("floor"))
        {
            this.transform.parent = null;
        }
    }
}