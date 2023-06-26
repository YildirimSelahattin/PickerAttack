using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
      // Start is called before the first frame update
    public List<GameObject> gridList = new List<GameObject>();
    public List<int> gridIsEmptyList = new List<int>();
    public GameObject gridPrefab;
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
    public int[] colCounter = new int [3];





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
        gridHeight =5;
        gridWidth = 5;
        CreateGrid();
    }

    public void CreateGrid()
    {
        // for elevator grid
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

        spawnPeople();

    }

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
}
