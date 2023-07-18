using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    int stageKeyIndex;
    float maxKeyValue = 120;
    float tweenDuration = 1f;
    public float[] tweeningKeyVariables = new float[8];
    public SkinnedMeshRenderer skinnedMesh;
    public bool isInAnimation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Animate(int index)
    {
        Tween keyTween = DOTween.To(() => tweeningKeyVariables[index],
        x => tweeningKeyVariables[index] = x, 100, tweenDuration*3).OnComplete(() =>
        {
            Tween expandTween = DOTween.To(() => tweeningKeyVariables[index],
            x => tweeningKeyVariables[index] = x, 0, tweenDuration/2);
            expandTween.OnUpdate(() => UpdateCannonMesh(index, tweeningKeyVariables[index])).OnComplete(() =>
            {
                if (index == 8)
                {
                    isInAnimation = false;
                }
            });
        });
    }

    public void UpdateCannonMesh(int index, float tweeningKeyVariable)
    {
        skinnedMesh.SetBlendShapeWeight(index, tweeningKeyVariable);
    }

    public IEnumerator StartMoveAfterTime(int index,float timer)
    {
        yield return new WaitForSeconds(timer);
        isInAnimation = true;
        Animate(index);
        if (index < 8)
        {
            StartCoroutine(StartMoveAfterTime(index + 1, 0.07f));
        }

    }
}
