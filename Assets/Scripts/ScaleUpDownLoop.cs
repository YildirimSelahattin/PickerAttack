using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpDownLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveLoop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveLoop()
    {
        transform.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            transform.DOScale(1, 0.2f).OnComplete(() => MoveLoop());
        });
    }
}
