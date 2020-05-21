using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    //GRID SIZE VALUES
    public int gridSize;
    [SerializeField] float offset;
    int startingPos;
    
    public int starterLength;

    GameObject nodePrefab;
    [SerializeField] List<Node> nodes = new List<Node>();
    //create a new grid of Nodes
    public Node[] grid;

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        //define the number of entries in the of the array based on the x, y and z size of the grid
        grid = new Node[gridSize];

        //divide by two so player starts farther from the top right
        startingPos = UnityEngine.Random.Range(0, grid.Length/2);

        //index starts at one because it is used for array length
        for (int index = 1, x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; index++, y++)
            {
                //declare the final X, Y and Z positions with the offset
                float offsetPosX = x * offset;
                float offsetPosY = y * offset;

                //Place the grid's cell at the current node position
                Vector3 offsetPos = new Vector3(offsetPosX, offsetPosY);

                if (index == startingPos) SpawnSnek(index);
                
                //Spawn a new cube at the corresponding "world" position of the node (WITH OFFSET)
                GameObject go = Instantiate(nodePrefab, offsetPos, Quaternion.identity);
                go.transform.parent = transform.GetChild(0).GetChild(0).transform;

                /*
                nodeObject.posX = x;
                nodeObject.posY = y;
                nodeObject.posZ = z;
                */

                Node currNode = new Node();         //generate a new node to store the current cell's information

                currNode.position = offsetPos;              //assign nodePrefab as the node viz
                currNode.nodePosX = x;              //assign current Node's info with x, y, z (NO OFFSET, CORRESPONDS TO GRID POSITION)
                currNode.nodePosY = y;

                grid[index] = currNode;           //store the node's information in the gridInfo array
                nodes.Add(currNode);                //add the node to a List that can be tracked in the inspector
                //onPlayNodePosList.Add(go);
            }
        }
    }

    private void SpawnSnek(int startingPosIndex)
    {
        for (int i = 0; i < starterLength; i++)
        {
            //Spawn a new cube at the corresponding "world" position of the node (WITH OFFSET)
            GameObject go = Instantiate(nodePrefab, grid[startingPosIndex].position, Quaternion.identity);
            go.transform.parent = transform.GetChild(0).GetChild(0).transform;
        }
    }

    int FindPosition(PlayerDirections direction)
    {
        int moveWeight = 0;

        switch (direction)
        {
            case PlayerDirections.UP:
                moveWeight = 1;
                break;

            case PlayerDirections.RIGHT:
                moveWeight = gridSize;
                break;

            case PlayerDirections.DOWN:
                moveWeight = -1;
                break;

            case PlayerDirections.LEFT:
                moveWeight = -gridSize;
                break;

            default:
                break;
        }

        return moveWeight;
    }
}
