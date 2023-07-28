using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rewardSystem : MonoBehaviour
{
    [SerializeField] private float rewardToShow;
    [SerializeField] private Transform Hand;
    [SerializeField] private Animator handAnim;
    public Button rewardBtn;
    public Button noThxBtn;
    float rewardMoney;
    void Start()
    {
        handAnim = GetComponent<Animator>();
        rewardBtn.onClick.AddListener(GetTheReward);
      
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("1.5x"))
        {
            var multiplier = other.gameObject.name;

            rewardToShow = 300;
            rewardBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardToShow.ToString();
        }
        if (other.CompareTag("2x"))
        {
            var multiplier = other.gameObject.name;

            rewardToShow = 400;
            rewardBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardToShow.ToString();
        }
        if (other.CompareTag("2.5x"))
        {
            var multiplier = other.gameObject.name;

            rewardToShow = 500;
            rewardBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardToShow.ToString();
        }
        if (other.CompareTag("3x"))
        {
            var multiplier = other.gameObject.name;

            rewardToShow = 600;
            rewardBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rewardToShow.ToString();
        }
    }

    public void GetTheReward()
    {
        //GameObject coinPrefab_ = Instantiate(GameManager.Instance.coinPrefab, UIManager.Instance.safeArea.transform);
        //coinPrefab_.GetComponent<CoinEffect>().MoveCoins(UIManager.Instance.totalMoneyText.transform.parent);
        handAnim.enabled = false;
        GameDataManager.Instance.totalMoney += (int)rewardToShow;
        rewardBtn.interactable = false;
        StartCoroutine(NextLevel());
    }

    public void NoThx()
    {
        noThxBtn.interactable = false;
        
        StartCoroutine(NextLevel());
        
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(0.1f);
        if (BossManager.Instance.gameObject == null)
        {
            GameDataManager.Instance.currentLevel++;
            GameDataManager.Instance.SaveData();
        }
        SceneManager.LoadScene(0);
    }
}
