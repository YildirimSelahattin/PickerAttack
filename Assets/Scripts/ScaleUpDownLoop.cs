using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpDownLoop : MonoBehaviour
{
    public float time;
    public Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale= transform.localScale;
        StartCoroutine(MoveLoop(time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator MoveLoop(float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        transform.DOScale(originalScale*1.08f, 0.2f).OnComplete(() =>
        {
            transform.DOScale(originalScale, 0.2f).OnComplete(() => {

                transform.DOScale(originalScale * 1.08f, 0.2f).OnComplete(() =>
                {
                    transform.DOScale(originalScale, 0.2f).OnComplete(() => StartCoroutine(MoveLoop(timeWait)));
                });
            });
        });
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
