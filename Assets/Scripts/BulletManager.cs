using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float  damage;   
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boss"))
        {
           Destroy(this.gameObject);
           other.GetComponent<BossManager>().health -= (int)damage;
        }
    }


}
