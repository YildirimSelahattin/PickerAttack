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
            Debug.Log("yeap");
            collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic= true;
            collision.collider.gameObject.transform.DOMove(transform.parent.position + new Vector3(0.7f,4,0), 1).OnComplete(()=>{

            collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic= false;
            collision.collider.gameObject.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;

            });


        }
    }
  
}
