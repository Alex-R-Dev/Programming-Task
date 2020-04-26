using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    //static variable - for all coins
    public static int coinCounter = 0;

    //output counter on screen
    void Update()
    {
        GetComponentInChildren<Text>().text = coinCounter.ToString();
    }

    public void UpdateCounter()
    {
        coinCounter++;
    }
}
