using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cannon : Army
{
    public int firerate;
    public int bulDMG;

    public GameObject bullet;
    public SkinnedMeshRenderer skinnedMeshCannon;
    public Transform partToRotate;
    public Transform firePoint;
    int stageKeyIndex;
    float maxKeyValue = 15;
    float tweenDuration = 0.2f;
    public bool isInShootAnimation = false;
    public float[] tweeningKeyVariables = new float[7];

    protected override void Die()
    {
        Debug.Log("cannon died");
        // Additional behavior specific to MobA when it dies
        // For example, play a specific death animation or drop unique loot
        base.Die(); // Call the base implementation as well
    }
    private void OnTriggerEnter(Collider other)
    {


    }
    private void Start()
    {
        StartCoroutine(StartMoveAfterTime(0));

    }
    private void Update()
    {

    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bullet, firePoint.position, firePoint.rotation);
        GameObject sound = new GameObject("sound");
        sound.AddComponent<AudioSource>();
        sound.GetComponent<AudioSource>().volume = 1;
        //sound.GetComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.boomEffect);
        //Destroy(sound, GameDataManager.Instance.boomEffect.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
        bulletGO.transform.DOMove(BossManager.Instance.transform.position, 3f);
    }

    public void AnimateCannonExplosion(int index)
    {
        Tween keyTween = DOTween.To(() => tweeningKeyVariables[index],
        x => tweeningKeyVariables[index] = x, 100, tweenDuration).OnComplete(() =>
        {
            Tween expandTween = DOTween.To(() => tweeningKeyVariables[index],
            x => tweeningKeyVariables[index] = x, 0, tweenDuration);
            expandTween.OnUpdate(() => UpdateCannonMesh(index, tweeningKeyVariables[index])).OnComplete(() =>
            {
                if (index == 5)
                {
                    isInShootAnimation = false;
                    Shoot();
                    StartCoroutine(StartMoveAfterTime(0));

                }
            });
        });
    }

    public void UpdateCannonMesh(int index, float tweeningKeyVariable)
    {
        skinnedMeshCannon.SetBlendShapeWeight(index, tweeningKeyVariable);
    }

    public IEnumerator StartMoveAfterTime(int index)
    {
        yield return new WaitForSeconds(0.05f);
        AnimateCannonExplosion(index);
        if (index < 7)
        {
            StartCoroutine(StartMoveAfterTime(index + 1));
        }
    }
}