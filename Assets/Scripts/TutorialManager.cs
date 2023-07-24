using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 targetPos;
    public static TutorialManager Instance;
    public Transform originalPos;
    public bool startLoop = false;
    public bool goingToTarget;
    public float sensivity;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position+(targetPos-originalPos.position).normalized);
        if (startLoop == true)
        {
            if(goingToTarget == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime* sensivity);
                if (Vector3.Distance(transform.position, targetPos) <6)
                {
                    goingToTarget= false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos.position, Time.deltaTime* sensivity);
                if (Vector3.Distance( transform.position,originalPos.position)<6)
                {
                    goingToTarget = true;
                }
            }
            
        }
        if(Vector3.Distance(originalPos.position, targetPos) < 8)
        {
            sensivity = 1;
        }
        sensivity = Vector3.Distance(originalPos.position,targetPos)/3f;
    }
}
