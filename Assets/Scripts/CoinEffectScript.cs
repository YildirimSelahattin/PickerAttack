using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y + 3, 0.4f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
