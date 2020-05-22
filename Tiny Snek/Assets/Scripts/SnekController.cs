using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    public List<GameObject> snekBodyList = new List<GameObject>();
    public PlayerDirections playerMovement;
    public float moveSpeed;
    public bool firstInput;

    //used by snekBody
    public int sizeMultiplier;


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && firstInput == false)
        {
            firstInput = true;
            StartCoroutine(MoveSnake());
        }

        CheckPlayerInputs();
    }

    private void CheckPlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerMovement = PlayerDirections.UP;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerMovement = PlayerDirections.RIGHT;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerMovement = PlayerDirections.DOWN;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerMovement = PlayerDirections.LEFT;
        }
    }

    IEnumerator MoveSnake()
    {
        for (int i = snekBodyList.Count - 1; i >= 0; i--)
        {
            if (i == 0) snekBodyList[0].GetComponent<SnekBody>().MoveHead(FindPosition(playerMovement));

            else
            {
                snekBodyList[i].GetComponent<SnekBody>().MoveBody(snekBodyList[i - 1].transform.position, FindPosition(playerMovement));
            }
        }

        yield return new WaitForSeconds(moveSpeed);
        StartCoroutine(MoveSnake());
    }

    public int FindPosition(PlayerDirections direction)
    {
        int moveWeight = 0;

        switch (direction)
        {
            case PlayerDirections.UP:
                moveWeight = 1;
                break;

            case PlayerDirections.RIGHT:
                moveWeight = GridScript.instance.gridSize;
                break;

            case PlayerDirections.DOWN:
                moveWeight = -1;
                break;

            case PlayerDirections.LEFT:
                moveWeight = - GridScript.instance.gridSize;
                break;

            default:
                break;
        }

        Debug.Log(moveWeight);
        return moveWeight;
    }
}
