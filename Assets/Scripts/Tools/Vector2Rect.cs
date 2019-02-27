using UnityEngine;
[System.Serializable]
public class Vector2Rect
{
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public Vector2 Size {
        get {
            return bottomRight - topLeft;
        }
    }
    public Vector2 Center {
        get {
            return topLeft + (Size / 2);
        }
    }
}