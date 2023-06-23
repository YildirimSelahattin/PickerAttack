using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        
        if ( other.CompareTag("In"))
        {
            other.gameObject.tag = "Pick";
            GameManager.Instance.inCount--;
            GameManager.Instance.objectList.Remove(other.gameObject);
            Debug.Log("Count --");

        }

    }
}
