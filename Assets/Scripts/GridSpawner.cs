using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GridSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> gridList = new List<GameObject>();
    public List<int> gridIsEmptyList = new List<int>();
    public GameObject gridPrefab;
    public List<GameObject> soldierList;
    public GameObject leftLimit;
    public GameObject rightLimit;
    public GameObject topLimit;
    public GameObject botLimit;
    public float xSize;
    public float ySize;
    public int xIndex;
    public int yIndex;
    public int gridWidth;
    public int gridHeight;
    public GameObject gridParent;
    public static GridSpawner Instance;
    public int[] colCounter = new int[3];
    public GameObject buttonPrefab;
    public GameObject buttonPanelPrefab;
    public Sprite archerSprite;
    public Sprite knightSprite;
    public Sprite smasherSprite;
    public CinemachineVirtualCamera cam;
    public List<GameObject> archerPrefabs;
    public List<GameObject> knightPrefabs;
    public GameObject bossPrefab;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            StartGame();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        //float distanceBetweenX = Mathf.Abs(leftLimit.transform.position.x - rightLimit.transform.position.x);
        // float distanceBetweenY = Mathf.Abs(topLimit.transform.position.z - botLimit.transform.position.z);
        // xSize = (distanceBetweenX / 5);
        // ySize = (distanceBetweenY / 5);
        CreateGrid();
        CreateButtons();
    }

    public void CreateGrid()
    {
        // for elevator grid
        CalculateGridAmount();
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject currGrid = Instantiate(gridPrefab, gridParent.transform);
                currGrid.transform.localPosition = new Vector3(x * xSize, 0, -y * ySize);
                gridIsEmptyList.Add(0);
                gridList.Add(currGrid);
            }
        }

        //fix camera 
        cam.transform.position = new Vector3((gridList[gridWidth * gridHeight - 1].transform.position.x + gridList[0].transform.position.x) / 2f, cam.transform.position.y, cam.transform.position.z);
        Instantiate(bossPrefab, new Vector3((gridList[gridWidth * gridHeight - 1].transform.position.x + gridList[0].transform.position.x) / 2f, 0, gridList[gridWidth * gridHeight - 1].transform.position.z + 5), Quaternion.identity);
    }

    public void MoveToFirst(int index, int levelOfSoldier, List<SoldierList> soldierListByLevel, int soldierIndex)
    {

        GameObject movingObject = soldierListByLevel[levelOfSoldier].soldiersList[index];
        movingObject.transform.DOJump(soldierListByLevel[levelOfSoldier].soldiersList[0].transform.position, 1, 1, 0.2f).OnComplete(() =>
        {
            Destroy(movingObject);
            if (index == 0)
            {
                if(soldierIndex == 0)
                {
                    Instantiate(archerPrefabs[levelOfSoldier + 1], soldierListByLevel[levelOfSoldier].soldiersList[0].transform.parent);
                }
                else if (soldierIndex == 1)
                {
                    Instantiate(knightPrefabs[levelOfSoldier + 1], soldierListByLevel[levelOfSoldier].soldiersList[0].transform.parent);
                }
                soldierListByLevel[levelOfSoldier].soldiersList.Clear();
            }
        });
    }
    /*
    public void spawnPeople()
    {
        foreach (int index in GameManager.Instance.objectList)
        {
            if (index == 1)
            {
                GameObject temp = Instantiate(LastSceneManager.Instance.cube, gridList[((index)*30)+colCounter[index]].transform);
                
            }
            if (index == 0)
            {
                GameObject temp = Instantiate(LastSceneManager.Instance.sphere,gridList[((index)*30)+colCounter[index]].transform);
            }
            if (index == 2)
            {
                GameObject temp = Instantiate(LastSceneManager.Instance.cylinder, gridList[((index)*30)+colCounter[index]].transform);
            }
            colCounter[index]++;
        }
    }
    */
    public void CreateButtons()
    {
        if (GameManager.Instance.archerCount > 0)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, buttonPanelPrefab.transform);
            buttonObject.GetComponent<ButtonManager>().soldierImage.sprite = archerSprite;
            buttonObject.GetComponent<ButtonManager>().count = GameManager.Instance.archerCount;
            buttonObject.GetComponent<ButtonManager>().soldierIndex = 0;
            buttonObject.GetComponent<ButtonManager>().prefabList = archerPrefabs;
        }
        if (GameManager.Instance.knightCount > 0)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, buttonPanelPrefab.transform);
            buttonObject.GetComponent<ButtonManager>().soldierImage.sprite = knightSprite;
            buttonObject.GetComponent<ButtonManager>().count = GameManager.Instance.knightCount;
            buttonObject.GetComponent<ButtonManager>().soldierIndex = 1;
        }
        if (GameManager.Instance.smasherCount > 0)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, buttonPanelPrefab.transform);
            buttonObject.GetComponent<ButtonManager>().soldierImage.sprite = smasherSprite;
            buttonObject.GetComponent<ButtonManager>().count = GameManager.Instance.smasherCount;
            buttonObject.GetComponent<ButtonManager>().soldierIndex = 2;
        }

    }
    public void CalculateGridAmount()
    {
        int maxGridNumber = (GameManager.Instance.archerCount / 5) + GameManager.Instance.archerCount % 5 + (GameManager.Instance.knightCount / 5) + 5 + (GameManager.Instance.smasherCount / 5) + 5;
        gridWidth = 4;
        gridHeight = 4;

    }
    public int GiveEmptyGridByRow()
    {
        for (int counter = 0; counter < gridWidth * gridHeight; counter++)
        {
            if (gridList[counter].transform.childCount == 1)
            {
                return counter;
            }
        }
        return -1;
    }

}
