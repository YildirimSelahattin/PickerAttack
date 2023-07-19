using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBack : MonoBehaviour
{
    public float forceAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("In"))
        {
            other.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * forceAmount * GameDataManager.Instance.speed / 2f) ;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
