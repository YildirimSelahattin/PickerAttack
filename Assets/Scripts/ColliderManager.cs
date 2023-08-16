using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update

    public void OnTriggerEnter(Collider other)
    {
        
            rigidbody.drag = 0;
        
    }
    private void OnCollisionExit(Collision collision)
    {
        
            rigidbody.drag = 7;
        
    }
  
}
