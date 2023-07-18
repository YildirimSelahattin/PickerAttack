using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class MovingArrowManager : MonoBehaviour
{

    float originalYPos;
    // Start is called before the first frame update
    void Start()
    {
        originalYPos = transform.localPosition.y;
        MoveLoop();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveLoop()
    {
        transform.DOPunchScale(new Vector3(-0.2f,0,0),0.5f,1);
        transform.DOLocalMoveY(250, 1f).OnComplete(() =>
        {
            transform.DOLocalMoveY(200 , 1.2f).OnComplete(() => {
                transform.DOPunchScale(new Vector3(-0.2f, 0, 0), 0.5f, 1);
                MoveLoop();
                });
        });
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
