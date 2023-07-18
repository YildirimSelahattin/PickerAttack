using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    int stageKeyIndex;
    float maxKeyValue = 70;
    float tweenDuration = 0.4f;
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
        x => tweeningKeyVariables[index] = x, maxKeyValue, tweenDuration).OnComplete(() =>
        {
            Tween expandTween = DOTween.To(() => tweeningKeyVariables[index],
            x => tweeningKeyVariables[index] = x, 0, tweenDuration);
            expandTween.OnUpdate(() => UpdateCannonMesh(index, tweeningKeyVariables[index])).OnComplete(() =>
            {
                if (index == 7)
                {
                    isInAnimation = false;
                }
            });
        });
        keyTween.OnUpdate(() => UpdateCannonMesh(index, tweeningKeyVariables[index]));
    }

    public void UpdateCannonMesh(int index, float tweeningKeyVariable)
    {
        skinnedMesh.SetBlendShapeWeight(index, tweeningKeyVariable);
    }

    public IEnumerator StartMoveAfterTime(int index,bool timer)
    {
        if (timer == false)
        {
            yield return new WaitForSeconds(1);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);

        }
        isInAnimation = true;
        Animate(index);
        if (index < 8)
        {
            StartCoroutine(StartMoveAfterTime(index + 1, true));
        }

    }
}
