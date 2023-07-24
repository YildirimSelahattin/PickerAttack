using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick"))
        {
            other.gameObject.tag = "In";
            
        }
    }


}
