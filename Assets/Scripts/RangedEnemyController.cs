using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    //parameters of RangedEnemy - min and max time between shots, shotController for making shots, animator
    [SerializeField] private float randomMin;
    [SerializeField] private float randomMax;
    [SerializeField] private FireShotController shotController;
    [SerializeField] private Animator animator;
    //time for next shot
    public float randomShot;

    public void Start()
    {
        //set first time
        randomShot = Random.Range(randomMin, randomMax);
    }
    void FixedUpdate()
    {
        //if time of shot and shot wasn't made - make shot (problem with anim)
        //and set time for next shot
        if (randomShot <= 0 && !shotController.isActive())
        {
            animator.SetBool("IsShot", true);
            shotController.MakeShot();
            randomShot = Random.Range(randomMin, randomMax);
        }
        //if there is time to next shot - set IDLE animation
        if (randomShot > 0)
        {
            animator.SetBool("IsShot", false);
        }
        //decrease time to next shot
        randomShot -= 0.01f;
    }
}
