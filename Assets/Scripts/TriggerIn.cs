using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
      if ( other.CompareTag("Pick"))
               {
                   other.gameObject.tag = "In";
                   GameManager.Instance.inCount++;
                   if (other.gameObject.GetComponent<PeopleManager>().index == 0)
                   {
                       GameManager.Instance.objectList.Add(other.gameObject.GetComponent<PeopleManager>().index);
                   }
                   if (other.gameObject.GetComponent<PeopleManager>().index == 1)
                   {
                       GameManager.Instance.objectList.Add(other.gameObject.GetComponent<PeopleManager>().index);
                   }
                   if (other.gameObject.GetComponent<PeopleManager>().index == 2)
                   {
                       GameManager.Instance.objectList.Add(other.gameObject.GetComponent<PeopleManager>().index);
                   }
               }
    }

    
}
