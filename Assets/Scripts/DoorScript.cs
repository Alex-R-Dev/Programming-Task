using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject key;

    void Start()
    {
        //set winText and key
        if (winText == null)
        {
            winText = GameObject.Find("WinText");
        }

        if (key == null)
        {
            key = GameObject.Find("Key");
        }
        //unatcivated winText
        winText.SetActive(false);
    }

    //if player have key and have collision with door - output Win string
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && !key.activeSelf)
        {
            winText.SetActive(true);
        }
    }
}
