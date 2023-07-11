using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHouseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.transform.CompareTag("In"))
        {
                
            collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic= true;
            collision.collider.gameObject.transform.DOMove(transform.parent.position, 1).OnComplete(()=>{
            Destroy(collision.collider.gameObject);
            });


        }
    }
  
}
