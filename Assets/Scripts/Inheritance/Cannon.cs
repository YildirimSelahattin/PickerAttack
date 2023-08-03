using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Cannon : Army
{
    public int fireDelay;
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
    private bool played = true;

    [Header("Audio")]
    public AudioClip cannon;

    protected override void Die()
    {
        Debug.Log("cannon died");
        
        base.Die(); // Call the base implementation as well
    }
    private void OnTriggerEnter(Collider other)
    {


    }
    private void Start()
    {
       

    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "BossScene")
        {
            if (GameManager.Instance.totalCount <= 0 && played)
            {
                if (BossManager.Instance == null)
                {
                    StopAllCoroutines();
                    return;
                }
                StartCoroutine(StartMoveAfterTime(0, 0));
                played = false;
            }


        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bullet, firePoint.position, firePoint.rotation);
        // GameObject sound = new GameObject("sound");
        //sound.AddComponent<AudioSource>();
        //sound.GetComponent<AudioSource>().volume = 1;
        //sound.GetComponent<AudioSource>().PlayOneShot(GameDataManager.Instance.boomEffect);
        //Destroy(sound, GameDataManager.Instance.boomEffect.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
        bulletGO.GetComponent<BulletManager>().damage = bulDMG;
        bulletGO.transform.DOMove(BossManager.Instance.arrowPoints[Random.Range(0, 11)].transform.position, 10f).SetSpeedBased(true).OnComplete(() =>
        {
            Destroy(bulletGO);
        });
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
                    StartCoroutine(StartMoveAfterTime(0,fireDelay));
                    GetComponent<AudioSource>().PlayOneShot(cannon);

                }
            });
        });
    }
    

    public void UpdateCannonMesh(int index, float tweeningKeyVariable)
    {
        skinnedMeshCannon.SetBlendShapeWeight(index, tweeningKeyVariable);
    }

    public IEnumerator StartMoveAfterTime(int index,int timer)
    {
        yield return new WaitForSeconds(timer);
        AnimateCannonExplosion(index);
        if (index < 7)
        {
            StartCoroutine(StartMoveAfterTime(index + 1,0));
        }
    }
}
