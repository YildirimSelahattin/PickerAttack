using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

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
        StopAllCoroutines();
        transform.DOKill();
        transform.localScale = originalScale;
    }
}
