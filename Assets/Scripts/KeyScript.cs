using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    //true if key taken
    private bool taken;
    void Start()
    {
        taken = false;
    }
    //if there is collision with player - open door and unactivate key
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            taken = true;
            GetComponentInParent<Animator>().SetBool("IsOpen", true);
            this.gameObject.SetActive(false);
        }
    }
    //function for door
    public bool isTaken()
    {
        return taken;
    }
}
