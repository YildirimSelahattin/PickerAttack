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
 
    [Header("AgentProperties")]

    private Rigidbody rb;
 
    public static PlayerManager Instance;
   
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
     

        
    }
    
    void Update()
    {
        
            DetectButtonPress();
           
    }

    private void DetectButtonPress()
    {
        Vector3 addForce = Vector3.zero;

        float forwardAmount = 0f;
        Vector3 localRotationParent = transform.localEulerAngles;
        Vector3 localRotationBody = transform.localEulerAngles;
        if (Input.GetKey(KeyCode.W) )
        {

            forwardAmount = 1;
        }


        if (Input.GetKey(KeyCode.S) )
        {

            forwardAmount = -1;

        }
        if (Input.GetKey(KeyCode.D) )
        {

            transform.localEulerAngles += Vector3.up;
            localRotationBody.z -= 1;
        }


        if (Input.GetKey(KeyCode.A) )
        {
            transform.localEulerAngles += Vector3.down;
            localRotationBody.z += 1;
        }
      
       
        rigidBody.velocity = transform.forward * forwardAmount*2;


    }


}
