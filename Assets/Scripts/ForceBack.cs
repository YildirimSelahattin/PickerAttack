using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBack : MonoBehaviour
{
    public float forceAmount;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("In"))
        {
            other.collider.gameObject.GetComponent<Rigidbody>().drag = 7;
        }
        
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("In"))
        {
            other.collider.gameObject.GetComponent<Rigidbody>().drag = 2;
        }
    }
}
