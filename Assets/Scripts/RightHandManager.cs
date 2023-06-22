using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RightHandManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 originPos;
    void Start()
    {
        originPos = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOLocalRotate(new Vector3(0,-50,0),.2f).OnComplete((() => transform.DOLocalRotate(new Vector3(0,-30,0),.5f)));
    }
}
