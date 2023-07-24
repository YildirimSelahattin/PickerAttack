using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 originalPos;
    Vector3 targetPos;
    public static TutorialManager Instance;
    void Start()
    {
        if(Instance == null){
            Instance= this;
        }
        originalPos = transform.localPosition;
        TutorialLoop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPos);
    }
    public void TutorialLoop()
    {
        if(SoldierHouseManager.Instance != null)
        {
            targetPos = SoldierHouseManager.Instance.gameObject.transform.position;
            transform.DOMove(targetPos, 10).SetSpeedBased(true).OnComplete(() =>
            {
                transform.DOLocalMove(originalPos, 10).SetSpeedBased(true).OnComplete(() => TutorialLoop());
            });
        }
        else
        {
            TutorialLoop();
        }
        
    }
}
