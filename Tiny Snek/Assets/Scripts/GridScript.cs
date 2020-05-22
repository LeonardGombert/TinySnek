using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    private static GridScript _instance;
    public static GridScript instance { get { return _instance; } }

    //GRID SIZE VALUES
    public int gridSize;
    [SerializeField] float offset;
    public int startingPos;

    public int startingBodyLength;

    public GameObject nodePrefab;
    public GameObject snekPrefab;

    SnekController snekController;
    public GameObject food;
    public GameObject portal;

    [SerializeField] List<Node> nodes = new List<Node>();
    //create a new grid of Nodes
    public Node[] grid;

    public int portal1Index, portal2Index;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;

        snekController = transform.GetChild(0).GetComponent<SnekController>();

        CreateGrid();
    }

    private void CreateGrid()
    {
        //define the number of entries in the of the array based on the x, y and z size of the grid
        grid = new Node[gridSize * gridSize];

        //divide by two so player starts farther from the top right
        startingPos = UnityEngine.Random.Range(0, grid.Length / 2) + startingBodyLength;

        portal1Index = UnityEngine.Random.Range(0, grid.Length);
        portal2Index = UnityEngine.Random.Range(0, grid.Length);

        for (int index = 0, x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; index++, y++)
            {
                float offsetPosX = x * offset;
                float offsetPosY = y * offset;

                Vector2 offsetPos = new Vector2(offsetPosX, offsetPosY);

                GameObject go = Instantiate(nodePrefab, offsetPos, Quaternion.identity);
                go.transform.parent = transform.GetChild(1).transform;

                Node newNode = new Node();

                newNode.nodeIndex = index;
                newNode.position = offsetPos;
                newNode.nodePosX = x;
                newNode.nodePosY = y;

                grid[index] = newNode;
                nodes.Add(newNode);

                if (index == startingPos) SpawnSnek(index);
                if (index == portal1Index) SpawnPortal(portal1Index, newNode);
                if (index == portal2Index) SpawnPortal(portal2Index, newNode);

            }
        }

        SpawnFirstFruit();
    }

    private void SpawnPortal(int index, Node newNode)
    {
        GameObject go = Instantiate(portal, grid[--index].position, Quaternion.identity);
        go.transform.parent = transform.GetChild(2).transform;

        newNode.isPortal = true;
    }

    private void SpawnSnek(int startingPosIndex)
    {
        for (int i = 0; i < startingBodyLength; i++)
        {
            GameObject go = Instantiate(snekPrefab, grid[--startingPosIndex].position, Quaternion.identity);
            go.transform.parent = transform.GetChild(0).transform;
            snekController.snekBodyList.Add(go);

            SnekBody body = go.AddComponent<SnekBody>() as SnekBody;
            body.myIndex = startingPosIndex;
        }
    }

    public void SpawnBodyPiece(int lastSnakeIndex, int numberOfPieces)
    {
        for (int i = 0; i < numberOfPieces; i++)
        {
            GameObject go = Instantiate(snekPrefab, grid[lastSnakeIndex++].position, Quaternion.identity);
            go.transform.parent = transform.GetChild(0).transform;
            snekController.snekBodyList.Add(go);

            SnekBody body = go.AddComponent<SnekBody>() as SnekBody;
            body.myIndex = lastSnakeIndex;
        }
    }

    public void SpawnFirstFruit()
    {
        int randomInt = UnityEngine.Random.Range(0, gridSize * gridSize);
        GameObject newObj = Instantiate(food, grid[randomInt].position, Quaternion.identity);
        newObj.transform.parent = transform.GetChild(3).transform;
        grid[randomInt].isFood = true;
        grid[randomInt].debugNodeViz = newObj;
    }

    public void Restart()
    {
        StopAllCoroutines();
        snekController.firstInput = false;
        nodes.Clear();
        snekController.snekBodyList.Clear();
        foreach (Transform child in transform.GetChild(0).transform) Destroy(child.gameObject);
        foreach (Transform child in transform.GetChild(1).transform) Destroy(child.gameObject);
        foreach (Transform child in transform.GetChild(2).transform) Destroy(child.gameObject);
        foreach (Transform child in transform.GetChild(3).transform) Destroy(child.gameObject);
        CreateGrid();
    }
}
