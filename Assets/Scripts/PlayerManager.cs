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


        if (Input.GetKey(KeyCode.A) )
        {

            transform.DOMove(transform.position + Vector3.left*0.02f, .3f);
        }


        if (Input.GetKey(KeyCode.D) )
        {

            transform.DOMove(transform.position + Vector3.right*0.02f, .3f);

        }



    }


}
