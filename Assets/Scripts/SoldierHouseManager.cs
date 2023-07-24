using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHouseManager : MonoBehaviour
{
    public PipeManager pipeScript;
    public GameObject targetPoint;

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
        if (collision.collider.gameObject.transform.CompareTag("In") || collision.collider.gameObject.transform.CompareTag("Pick"))
        {

            collision.collider.gameObject.GetComponent<Collider>().enabled = false;
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 0)
            {
                GameManager.Instance.knightCount++;
                GameManager.Instance.totalCount++;
                PlayerManager.Instance.InstantiateCoinEffect(100);
            }
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 1)
            {
                GameManager.Instance.totalCount++;
                GameManager.Instance.archerCount++;
                PlayerManager.Instance.InstantiateCoinEffect(10);
            }
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 2)
            {
                GameManager.Instance.totalCount++;
                GameManager.Instance.cannonCount++;
                PlayerManager.Instance.InstantiateCoinEffect(200);
            }
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 3)
            {
                GameManager.Instance.totalCount++;
                GameManager.Instance.spearCount++;
                PlayerManager.Instance.InstantiateCoinEffect(50);
            }
            collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            StartCoroutine(pipeScript.StartMoveAfterTime(0, false));

            collision.collider.gameObject.transform.DOJump(targetPoint.transform.position,5,1,1).OnComplete(() =>
            {
                pipeScript.gameObject.GetComponent<AudioSource>().PlayOneShot(pipeScript.pipe);
                Destroy(collision.collider.gameObject);

            });
            UIManager.Instance.ControlButtonInteractable();

        }
    }

}
