using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            Debug.Log("Portal has been opened!");
        }
    }
}
