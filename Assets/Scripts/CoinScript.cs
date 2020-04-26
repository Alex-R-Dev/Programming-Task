using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private GameObject coinCounter;
    void Start()
    {
        coinCounter = GameObject.Find("CoinCounter");
    }

    void Update()
    {
        
    }
    //if player have collision with coin - increase counter (bug with two colliders - double increasing)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            coinCounter.GetComponent<CoinCounter>().UpdateCounter();
            this.gameObject.SetActive(false);
        }
    }
}
