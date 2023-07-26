using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using TMPro;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [Header("MoveBoundaries")]
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
    public List<GameObject> soldierInPickerList;
    private Rigidbody rb;
    public bool stopped = false;

    public static PlayerManager Instance;
    public CinemachineVirtualCamera endCamera;
    private GameObject cameraLocation;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerManager.Instance.counterObject.transform.GetChild(0).gameObject.transform.DOLocalMoveZ(PlayerManager.Instance.counterObject.transform.GetChild(0).gameObject.transform.localPosition.z - 0.9f * GameDataManager.Instance.SizeLevel, 0.2f);
            transform.DOScale(GameDataManager.Instance.size,0.5f);
        }

    }

    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            DetectButtonPress();
        }
        counterObject.transform.position = transform.position;
    }

    private void DetectButtonPress()
    {
        touchStartImage.transform.position = touchStartPos;
        curTouchImage.transform.position = curTouchPosition;
        if (Input.touchCount > 0)
        {
            curTouchImage.gameObject.SetActive(true);
            touchStartImage.gameObject.SetActive(true);
            Vector3 localRotationParent = transform.localEulerAngles;
            Vector3 localRotationBody = transform.localEulerAngles;
            stopped = false;
            if (Input.GetTouch(0).phase == TouchPhase.Began)     // This is actions when finger/cursor hit screen
            {
                touchStartPos = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved|| Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                curTouchPosition = Input.GetTouch(0).position;
                Vector3 dir = (curTouchPosition - touchStartPos).normalized;

                if (Vector3.Distance(curTouchPosition, touchStartPos) > 60)
                {
                    touchStartPos += dir * (Vector3.Distance(curTouchPosition, touchStartPos) - 60);
                    dir = (curTouchPosition - touchStartPos).normalized;
                }
                float angle = Vector3.Angle(new Vector3(dir.x,0,dir.y), transform.forward);
                if (angle > 1.5f)
                {
                    Vector3 targetPos = new Vector3(transform.position.x + dir.x, 0, transform.position.z + dir.y);
                    transform.LookAt(targetPos);
                }
                rigidBody.velocity = transform.forward  * GameDataManager.Instance.speed;
               
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchStartPos = Vector3.zero;
                curTouchPosition = Vector3.zero;
                rigidBody.velocity = Vector3.zero;
                DecreaseDragLevel();
                prevYdif = 0;
                prevXdif = 0;
                stopped = true;
                curTouchImage.gameObject.SetActive(false);
                touchStartImage.gameObject.SetActive(false);
            }

        }
    }

    public void InstantiateCoinEffect(int coinAmount)
    {
        coinEffectPrefab.GetComponent<TextMeshPro>().text = "+ " + coinAmount.ToString();
        GameObject text = Instantiate(coinEffectPrefab, coinEffectParent.transform.position,coinEffectPrefab.transform.rotation);
        GameDataManager.Instance.totalMoney += coinAmount;
        UIManager.Instance.totalMoney.text = GameDataManager.Instance.totalMoney.ToString();
    }

    public void DecreaseDragLevel()
    {

    }
}
