using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Characteristics of player, animator for change animation and rigidBody
    [SerializeField] private float playerSpeed;
    [SerializeField] private float powerOfJump;
    [SerializeField] private float movement;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody;
    //LayerMasks for checking colliders
    private LayerMask m_isGround;
    private LayerMask m_isEnemy;
    //Float for checking axis "Jump", start position for returning
    private float jump;
    private Vector3 startPosition;
    //radius for finding other colliders
    private const float radiusCeiling = .9f;
    //grounded - check if on ground
    //alive - check if alive
    public bool grounded;
    public bool alive;
    private float exit;

    //set start position, get layer masks and set alive and grounded
    void Start()
    {
        startPosition = this.transform.position;
        m_isGround = LayerMask.GetMask("Ground");
        m_isEnemy = LayerMask.GetMask("Enemy");
        grounded = true;
        rigidbody = this.GetComponent<Rigidbody2D>();
        alive = true;
    }

    void Update()
    {
        //check input
        exit = Input.GetAxis("Cancel");
        if (exit > 0)
        {
            Application.Quit();
        }
        if (this.transform.position.y < -10f)
        {
            //if fall - die
            animator.SetBool("IsDie", true);
            Die();
        }

        //if alive - can moving
        if (alive)
        {
            animator.SetBool("IsDie", false);
            jump = 0;
            movement = Input.GetAxisRaw("Horizontal");
            
            animator.SetFloat("Speed", Math.Abs(movement));
        }
        //check on jump
        if (Input.GetKeyDown(KeyCode.Space) && alive)
        {
            jump = 1f;
        }


    }

    void FixedUpdate()
    {
        //also check on jump, for more accurate moment of jump
        if (Input.GetKeyDown(KeyCode.Space) && alive)
        {
            jump = 1;
        }
        Move(movement, jump);
    }

    private void Move(float movement, float jump)
    {
        //check all colliders in radius "ceilingRaduis" with layer "Ground" and "Enemy"
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusCeiling, m_isGround | m_isEnemy);
        for (int i = 0; i < colliders.Length; i++)
        {
            //set grounded after jumping
            if (colliders[i].gameObject != gameObject && !grounded)
            {
                grounded = true;
                animator.SetBool("IsJumping", false);

            }
            //check on enemies
            OnTriggerEnter2D(colliders[i]);
        }
        //move with smoothing
        Vector3 velocity = Vector3.zero;
        Vector3 targetVelocity = new Vector2(playerSpeed * movement, 0);
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, 0.05f);
        //fliping of sprite
        if (movement > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (movement < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //jumping
        if (jump > 0 && grounded)
        {
            grounded = false;
            animator.SetBool("IsJumping", true);
            rigidbody.AddForce(new Vector2(0, powerOfJump));
            jump = 0;
        }
    }
    //process of die (problem with animation)
    public void Die()
    {
        animator.SetBool("IsDie", true);
        //forbid moving and returning on start position
        alive = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.transform.localPosition = startPosition;
        alive = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //die if enemy
        if (LayerMask.GetMask(LayerMask.LayerToName(other.gameObject.layer)) == LayerMask.GetMask("Enemy")) 
        {
            Die();
        }
    }
}
