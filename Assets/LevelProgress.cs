using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public static int numberOfCoins = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        numberOfCoins--;
        Debug.Log("picked"+numberOfCoins);
    }
}
