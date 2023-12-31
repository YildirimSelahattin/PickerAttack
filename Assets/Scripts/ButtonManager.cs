using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Image soldierImage;
    public TextMeshProUGUI countText;
    private int _count;
    public int soldierIndex;
    public List<GameObject> soldierPrefabs;
    public float timer;
    public bool buttonPressed;
    public float rate;
    public int instantiatedNumber;
    public List<GameObject> soldier5Grouped;
    public int count
    {
        get {
           return _count;
         }
        set
        { _count = value; 
           countText.text = _count.ToString();
        }
    }
    void Start()
    {
        timer = rate;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                InstantiateInLoop();
                timer = rate;
            }
        }
    }

    public void OnPushStarted()
    {
        if (count > 0)
        {
            buttonPressed = true;
            InstantiateInLoop();
        }
    }

    public void OnPushStop()
    {
        buttonPressed = false;
        timer = rate;
    }
    public void InstantiateInLoop()
    {
        GameObject temp= Instantiate(soldierPrefabs[soldierIndex],GridSpawner.Instance.gridList[GridSpawner.Instance.GiveEmptyGridByRow()].transform);
        GridSpawner.Instance.soldierList.Add(temp);
        GridSpawner.Instance.ControlMerge();
        count--;
        switch (soldierIndex)
{
    case 1:
        GameManager.Instance.knightCount--;
        break;

    case 0:
        GameManager.Instance.archerCount--;
        break;

  
}

        if (count == 0)
        {
            buttonPressed = false;
            GetComponent<Button>().interactable = false;
        }
    }

   
    public void MergeSoldiers()
    {
        for (int counter = 4; counter>=0;counter--)
        {
            MoveToFirst(counter);
        }
    }

    public void MoveToFirst(int index)
    {

        GameObject movingObject = soldier5Grouped[index];
        movingObject.transform.DOJump(soldier5Grouped[0].transform.position, 1, 1, 0.2f).OnComplete(() =>
        {
            Destroy(movingObject);
            if (index == 0)
            {
                Instantiate(soldierPrefabs[soldierIndex], soldier5Grouped[0].transform.parent);
                soldier5Grouped.Clear();
            }
            });
    }
}
