using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekBody : MonoBehaviour
{
    public int myIndex;
    SnekController snek;

    private void Start()
    {
        snek = FindObjectOfType<SnekController>();
    }

    public void MoveHead(int direction)
    {
        transform.position = GridScript.instance.grid[myIndex + direction].position;

        if (GridScript.instance.grid[myIndex + direction].isFood)
        {
            GridScript.instance.grid[myIndex + direction].isFood = false;
            Destroy(GridScript.instance.grid[myIndex + direction].debugNodeViz);
            Grow();
        }

        myIndex += direction;

        if (myIndex == GridScript.instance.portal1Index)
        {
            transform.position = GridScript.instance.grid[GridScript.instance.portal2Index].position;
            myIndex = GridScript.instance.portal2Index + direction;
        }

        if (myIndex == GridScript.instance.portal2Index)
        {
            transform.position = GridScript.instance.grid[GridScript.instance.portal1Index].position;
            myIndex = GridScript.instance.portal1Index + direction;
        }

        if (GridScript.instance.grid[myIndex].isSnek)
        {
            foreach (Transform child in snek.gameObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void Grow()
    {
        int lastIndex = snek.snekBodyList[snek.snekBodyList.Count - 1].gameObject.GetComponent<SnekBody>().myIndex;
        GridScript.instance.SpawnBodyPiece(lastIndex, snek.sizeMultiplier);
        GridScript.instance.SpawnFirstFruit();
    }

    public void MoveBody(Vector2 position, int direction)
    {
        myIndex = snek.snekBodyList[snek.snekBodyList.IndexOf(gameObject) - 1].gameObject.GetComponent<SnekBody>().myIndex;
        GridScript.instance.grid[myIndex - 1].isSnek = false;
        GridScript.instance.grid[myIndex].isSnek = true;
        transform.position = position;
    }
}
