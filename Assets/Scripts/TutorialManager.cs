using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 targetPos;
    public static TutorialManager Instance;
    public Transform originalPos;
    public bool startLoop = false;
    public bool goingToTarget;

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
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 6);
                if (transform.position == targetPos)
                {
                    goingToTarget= false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPos.position, Time.deltaTime * 10);
                if (transform.position == originalPos.position)
                {
                    goingToTarget = true;
                }
            }
            
        }

    }
}
