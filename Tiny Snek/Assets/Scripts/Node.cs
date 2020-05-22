using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2 position;
    public float nodePosX, nodePosY;
    public GameObject debugNodeViz;
    public int nodeIndex;
    public bool isFood; 
    public bool isPortal;
    public bool isSnek;
    //public Grid_Object cubeOnNodePos;
}
