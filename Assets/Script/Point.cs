using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Point : MonoBehaviour
{
    public enum Direction
    {
        Right,
        Down,
        Up,
    }
    public int direction = (int)Direction.Right;

    public int GetDirection()
    {
        return direction;
    }
}
