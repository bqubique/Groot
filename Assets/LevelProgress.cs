using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    static int totalCoinNumber = 100;
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        totalCoinNumber--;
        Debug.Log("total c numL ;" + totalCoinNumber);
    }
}
