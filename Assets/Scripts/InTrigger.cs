using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InTrigger : MonoBehaviour
{
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
        if (other.CompareTag("In"))
        {
            Collider.enabled = true;
            Collider2.enabled = true;
        }

        other.tag = "Pick";
    }
}
