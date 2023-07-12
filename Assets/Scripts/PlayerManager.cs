using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class PlayerManager : MonoBehaviour
{
    [Header("MoveBoundaries")]
    //   public  Transform rightLimit;
    //    public  Transform leftLimit;

    private bool moveRight = false;
    private bool moveLeft = false;
    public GameObject leftLimit;
    public GameObject rightLimit;
    public Rigidbody rigidBody;
    public float rotationSpeed;
    public GameObject counterObject;
    public Vector3 touchStartPos;
    public Vector3 curTouchPosition;
    public float prevXdif;
    public float prevYdif;
    [Header("AgentProperties")]

    private Rigidbody rb;

    public static PlayerManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }



    }

    void Update()
    {

        DetectButtonPress();
        counterObject.transform.position = transform.position;
    }

    private void DetectButtonPress()
    {
        if (Input.touchCount > 0)
        {
            float forwardAmount = 0f;
            Vector3 localRotationParent = transform.localEulerAngles;
            Vector3 localRotationBody = transform.localEulerAngles;

            if (Input.GetTouch(0).phase == TouchPhase.Began)                // This is actions when finger/cursor hit screen
            {
                touchStartPos = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                curTouchPosition = Input.GetTouch(0).position;
                Vector3 dir = (curTouchPosition - touchStartPos).normalized;
                Vector3 targetPos = new Vector3(transform.position.x+dir.x,0,transform.position.z + dir.y);
                transform.DOLookAt(targetPos,0.1f);
                forwardAmount = 1;
                rigidBody.velocity = transform.forward * forwardAmount * GameManager.Instance.speed;
            }
          
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchStartPos = Vector3.zero;
                curTouchPosition = Vector3.zero;
                rigidBody.velocity = Vector3.zero;
                prevYdif = 0;
                prevXdif = 0;
            }
           
        }
    }
}
