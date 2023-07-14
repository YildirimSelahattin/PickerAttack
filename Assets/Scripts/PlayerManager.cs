using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("MoveBoundaries")]
    //   public  Transform rightLimit;
    //    public  Transform leftLimit;

    private bool moveRight = false;
    private bool moveLeft = false;
    public GameObject leftLimit;
    public GameObject rightLimit;
    public Rigidbody rigidBody;
    public float rotationSpeed;
    public GameObject counterObject;
    public Vector3 touchStartPos;
    public Vector3 curTouchPosition;
    public float prevXdif;
    public float prevYdif;
    public GameObject curTouchImage;
    public GameObject touchStartImage;
    public GameObject coinEffectPrefab;
    public GameObject coinEffectParent;
    [Header("AgentProperties")]

    private Rigidbody rb;

    public static PlayerManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (PlayerPrefs.HasKey(GameDataManager.Instance.speedLevelKey))
        {
        GameManager.Instance.speed = GameManager.Instance.speed * Mathf.Pow(1.25f, GameDataManager.Instance.SpeedLevel-1);
        }
        if (PlayerPrefs.HasKey(GameDataManager.Instance.sizeLevelKey))
        {
          GameManager.Instance.scale = GameManager.Instance.scale * Mathf.Pow(1.25f, GameDataManager.Instance.SizeLevel-1);

        }
        if (PlayerPrefs.HasKey(GameDataManager.Instance.timeLevelKey))
        {
       GameManager.Instance.maxTimer = GameManager.Instance.maxTimer * Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1);
       GameManager.Instance.timer = GameManager.Instance.timer * Mathf.Pow(1.25f, GameDataManager.Instance.TimeLevel-1);
        
        }


    }

    void Update()
    {

        DetectButtonPress();
        counterObject.transform.position = transform.position;
    }

    private void DetectButtonPress()
    {
        touchStartImage.transform.position = touchStartPos;
        curTouchImage.transform.position = curTouchPosition;
        if (Input.touchCount > 0)
        {
            float forwardAmount = 0f;
            Vector3 localRotationParent = transform.localEulerAngles;
            Vector3 localRotationBody = transform.localEulerAngles;

            if (Input.GetTouch(0).phase == TouchPhase.Began)                // This is actions when finger/cursor hit screen
            {
                touchStartPos = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                curTouchPosition = Input.GetTouch(0).position;
                Vector3 dir = (curTouchPosition - touchStartPos).normalized;
                Vector3 targetPos = new Vector3(transform.position.x+dir.x,0,transform.position.z + dir.y);
                transform.DOLookAt(targetPos,0.1f);
                forwardAmount = 1;
                rigidBody.velocity = transform.forward * forwardAmount * GameManager.Instance.speed;
                if (Vector3.Distance(curTouchPosition,touchStartPos)>75)
                {
                    touchStartPos += dir * (Vector3.Distance(curTouchPosition, touchStartPos)-75);
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchStartPos = Vector3.zero;
                curTouchPosition = Vector3.zero;
                rigidBody.velocity = Vector3.zero;
                prevYdif = 0;
                prevXdif = 0;
            }
           
        }
    }

    public void InstantiateCoinEffect(int coinAmount)
    {
        coinEffectPrefab.GetComponent<TextMeshPro>().text = "+ "+ coinAmount.ToString();
        GameObject text = Instantiate(coinEffectPrefab,coinEffectParent.transform);
        GameDataManager.Instance.totalMoney+= coinAmount; 
        UIManager.Instance.totalMoney.text = GameDataManager.Instance.totalMoney.ToString();
    }
}
