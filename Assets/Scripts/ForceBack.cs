using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("In"))
        {

            other.transform.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.SetParent(transform.parent);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
