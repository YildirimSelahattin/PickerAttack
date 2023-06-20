using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("Small"))
        {
            Debug.Log("small hit");
        }
        if ( collision.gameObject.CompareTag("Big"))
        {
            Debug.Log("big hit");
        }

    }
    
}
