// Unity
using UnityEngine;

public class Vector
{
    public static readonly Vector3 Left = new Vector3(-1, 0, 0);
    public static readonly Vector3 Right = new Vector3(+1, 0, 0);
    public static readonly Vector3 Up = new Vector3(0, +1, 0);
    public static readonly Vector3 Down = new Vector3(0, -1, 0);

    public static Vector3 DirectionToNormalizedVector(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                return Left;

            case MoveDirection.Right:
                return Right;

            case MoveDirection.Up: 
                return Up;

            case MoveDirection.Down:
                return Down;

            default: 
                return Vector3.zero;
        }
    }
}