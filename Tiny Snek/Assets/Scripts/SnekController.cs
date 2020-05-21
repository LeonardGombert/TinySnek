using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    PlayerDirections playerDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInputs();
    }

    private void CheckPlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) playerDirection = PlayerDirections.UP;
        if (Input.GetKeyDown(KeyCode.RightArrow)) playerDirection = PlayerDirections.RIGHT;
        if (Input.GetKeyDown(KeyCode.DownArrow)) playerDirection = PlayerDirections.DOWN;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) playerDirection = PlayerDirections.LEFT;
    }
}
