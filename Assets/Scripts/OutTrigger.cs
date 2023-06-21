using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider Collider;
    public BoxCollider Collider2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Collider.enabled = false;
            Collider2.enabled = false;
        }
        other.tag = "In";

    }
}
