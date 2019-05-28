using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private Camera camLocal;
    public GameObject target;
    public Vector2 boxSize;
    private Vector2 halfSize = new Vector2();
    public Vector2Rect levelSize;
    public float lerp = 1;
    private float tempLerp = 1;
    //public float level3lerp = .1f;
    //private Vector2 focusSwitch;
    public bool moveX = true, moveY = true;
    private Vector2 offset;
    private Slide slide;
    
    void Start()
    {
        camLocal = transform.GetComponentInChildren<Camera>();
        halfSize = camLocal.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        target = GameObject.FindGameObjectWithTag("PlayerOffSet");
        slide = target.GetComponent<Slide>();
    }

    public void cameraFocus(Vector2 focus, float lerpMagnitude = 1)
    {
        tempLerp = Mathf.Lerp(tempLerp, lerpMagnitude, .1f);
        Vector2 camPos = transform.position;
        offset = focus - camPos;
        if (offset.x != Mathf.Clamp(offset.x, -boxSize.x, boxSize.x) && moveX)
        {
            slide.UpdateTurn();
            camPos.x = focus.x - (boxSize.x * Sign(offset.x));
        }
        if (offset.y != Mathf.Clamp(offset.y, -boxSize.y, boxSize.y) && moveY)
        {
            camPos.y = focus.y - (boxSize.y * Sign(offset.y));
        }
        transform.position = Vector3.Lerp(transform.position, (Vector3)camPos + 10 * Vector3.back, tempLerp);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, levelSize.topLeft.x + halfSize.x, levelSize.bottomRight.x - halfSize.x), Mathf.Clamp(transform.position.y, levelSize.topLeft.y + halfSize.y, levelSize.bottomRight.y - halfSize.y), -10);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            cameraFocus(camLocal.ScreenToWorldPoint(Input.mousePosition), .1f);            
        }
        else if (Input.GetKey(KeyCode.U))
        {
            cameraFocus(new Vector2(4, 4), .1f);
        }
        else
        {
            cameraFocus(target.transform.position, lerp);
        }
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