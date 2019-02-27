using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private Camera camLocal;
    public Transform target;
    public Vector2 boxSize;
    public Vector2Rect levelSize;
    public float lerp = 1;
    public bool moveX = true, moveY = true;
    private Vector2 offset;
    void Start()
    {
        camLocal = GetComponent<Camera>();
    }

    void Update()
    {
        Vector2 camPos = transform.position;
        offset = target.position - (Vector3)camPos;
        if (offset.x != Mathf.Clamp(offset.x, -boxSize.x, boxSize.x) && moveX)
        {
            camPos.x = target.position.x - (boxSize.x * Sign(offset.x));
        }
        if (offset.y != Mathf.Clamp(offset.y, -boxSize.y, boxSize.y) && moveY)
        {
            camPos.y = target.position.y - (boxSize.y * Sign(offset.y));
        }
        transform.position = Vector3.Lerp(transform.position, (Vector3)camPos + 10 * Vector3.back, lerp);
        Vector2 halfSize = camLocal.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) / 2;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, levelSize.topLeft.x + halfSize.x, levelSize.bottomRight.x - halfSize.x), Mathf.Clamp(transform.position.y, levelSize.topLeft.y + halfSize.y, levelSize.bottomRight.y - halfSize.y), -10);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, boxSize * 2);
        Gizmos.DrawWireCube(levelSize.Center, levelSize.Size);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)offset);
    }

    bool PointInBox(Vector2 point, Vector2 boxSize)
    {
        if (point.x > -boxSize.x && point.y > -boxSize.y && point.x < boxSize.x && point.y < boxSize.y)
        {
            return true;
        }
        return false;
    }
    int Sign(float input)
    {
        if (input != 0) input = Mathf.Abs(input) / input;
        return Mathf.FloorToInt(input);
    }
}