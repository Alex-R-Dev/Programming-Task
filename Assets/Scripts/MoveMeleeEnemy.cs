using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MoveMeleeEnemy : MonoBehaviour
{   
    //Speed and points of way of enemy
    [Range(0.1f, 1f)][SerializeField] private float speed;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    //check if point A less B
    private bool isALessB = false;
    //bool for flipping of sprite
    private bool facingRight = false;
    //direction of move - left or right
    private float direction = -1f;
    //x-coordinates of points
    public Vector3 xCoordinateA;
    public Vector3 xCoordinateB;
    void Start()
    {   
        //set coordinates, check on direction
        xCoordinateA = pointA.transform.position;
        xCoordinateB = pointB.transform.position;
        if (xCoordinateB.x > xCoordinateA.x)
        {
            Flip();
            isALessB = true;
        }
    }
    void Update()
    {
        if (isALessB)
        {
            Move(xCoordinateA, xCoordinateB);
        }
        else
        {
            Move(xCoordinateB,xCoordinateA);
        }
    }

    void Move(Vector3 pointA, Vector3 pointB)
    {
        //Move and flip sprite and direction, if go through point
        this.transform.position += new Vector3(speed * direction, 0, 0);
        if (this.transform.position.x >= pointB.x)
        {
            Flip();
        }

        if (this.transform.position.x <= pointA.x)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        this.transform.localScale = scale;
        direction *= -1;
    }
}
