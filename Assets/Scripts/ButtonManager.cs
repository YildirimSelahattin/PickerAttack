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
    public float timer;
    public bool buttonPressed;
    public float rate;
    public int instantiatedNumber;
    public List<List<GameObject>>soldier5Grouped = new List<List<GameObject>>();
    public List<GameObject> prefabList;
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
        soldier5Grouped.Add(new List<GameObject>());
        soldier5Grouped.Add(new List<GameObject>());
        soldier5Grouped.Add(new List<GameObject>());
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
        GameObject temp= Instantiate(prefabList[0],GridSpawner.Instance.gridList[GridSpawner.Instance.GiveEmptyGridByRow()].transform);
        soldier5Grouped[0].Add(temp);
        if (soldier5Grouped[0].Count == 5)
        {
            MergeSoldiers(0);
        }
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

   
    public void MergeSoldiers(int levelIndex)
    {
        for (int counter = 4; counter>=0;counter--)
        {
            MoveToFirst(counter,levelIndex);
        }
    }

    public void MoveToFirst(int index,int levelIndex)
    {
        GameObject movingObject = soldier5Grouped[levelIndex][index];
        movingObject.transform.DOJump(soldier5Grouped[levelIndex][0].transform.position, 1, 1, rate/2).OnComplete(() =>
        {
            Destroy(movingObject);
            if (index == 0)
            {
                
                GameObject temp = Instantiate(prefabList[levelIndex+1], soldier5Grouped[levelIndex][0].transform.parent);
                soldier5Grouped[levelIndex+1].Add(temp);
                soldier5Grouped[levelIndex].Clear();
                ControlUpperLevels();
            }
        });

    }
    public void ControlUpperLevels()
    {
        if (soldier5Grouped[1].Count == 5)
        {
            MergeSoldiers(1);
        }
    }
}
