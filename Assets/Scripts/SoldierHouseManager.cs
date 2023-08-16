using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierHouseManager : MonoBehaviour
{
    public PipeManager pipeScript;
    public GameObject targetPoint;
    public static SoldierHouseManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            TutorialManager.Instance.targetPos = new Vector3(transform.position.x,1,transform.position.z);
            TutorialManager.Instance.startLoop = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.transform.CompareTag("In") || collision.collider.gameObject.transform.CompareTag("Pick"))
        {
            pipeScript.gameObject.GetComponent<AudioSource>().PlayOneShot(pipeScript.pipe);
            if (TutorialManager.Instance.IsDestroyed() == false)
            {
                Destroy(TutorialManager.Instance.gameObject);
            }
            collision.collider.gameObject.GetComponent<Collider>().enabled = false;
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 0)
            {
                GameManager.Instance.knightCount++;
                GameManager.Instance.totalCount++;
                PlayerManager.Instance.InstantiateCoinEffect(50);
            }
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 1)
            {
                GameManager.Instance.totalCount++;
                GameManager.Instance.archerCount++;
                PlayerManager.Instance.InstantiateCoinEffect(5);
            }
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 2)
            {
                GameManager.Instance.totalCount++;
                GameManager.Instance.cannonCount++;
                PlayerManager.Instance.InstantiateCoinEffect(100);
            }
            if (collision.collider.gameObject.GetComponent<PeopleManager>().index == 3)
            {
                GameManager.Instance.totalCount++;
                GameManager.Instance.spearCount++;
                PlayerManager.Instance.InstantiateCoinEffect(25);
            }
            collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            StartCoroutine(pipeScript.StartMoveAfterTime(0, false));

            collision.collider.gameObject.transform.DOJump(targetPoint.transform.position, 5, 1, 1).OnComplete(() =>
            {
                Destroy(collision.collider.gameObject);

            });
            UIManager.Instance.ControlButtonInteractable();

        }
    }

}
