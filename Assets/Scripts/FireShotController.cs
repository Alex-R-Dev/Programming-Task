using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShotController : MonoBehaviour
{
    //distance of fly
    [SerializeField] private float distance;
    //speed of bullet
    [SerializeField] private float speed;
    
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D collider;
    //check if make shot or not
    public bool makeShot = false;
    private float direction;
    public Vector3 startPosition;

    void Start()
    {
        //deactivate object, flip if need and set start position
        this.gameObject.SetActive(false);
        if (!GetComponentInParent<SpriteRenderer>().flipX)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            direction = 1f;
            this.transform.position = GetComponentInParent<Transform>().position + Vector3.right;
        }
        else
        {
            this.transform.position = GetComponentInParent<Transform>().position + Vector3.left;
            direction = -1f;
        }

        startPosition = this.transform.localPosition;
    }
    void FixedUpdate()
    {
        //move bullet if shot and return after ending of distance
        if (makeShot)
        {
            animator.SetBool("IsShot", true);
            this.transform.localPosition += new Vector3(speed * direction * Time.fixedDeltaTime, 0, 0);
            if (Math.Abs(this.gameObject.transform.localPosition.x) >= Math.Abs(distance))
            {
                returnShot();
            }
        }
    }

    //function for RangedEnemy
    public void MakeShot()
    {
        makeShot = true;
        this.gameObject.SetActive(true);
    }

    public bool isActive()
    {
        return makeShot;
    }
    //return bullet and unactivated it
    public void returnShot()
    {
        this.gameObject.SetActive(false);
        animator.SetBool("IsShot", false);
        this.transform.localPosition = startPosition;
        makeShot = false;
    }
    //if collision - return bullet
    void OnTriggerEnter2D(Collider2D other)
    {
        returnShot();
    }
}
