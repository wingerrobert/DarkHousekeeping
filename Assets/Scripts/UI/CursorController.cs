using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    private Color activeColor = Color.green;
    private Color invalidColor = Color.red;
    private Color idleColor = Color.white;
    private Color currentColor;
    
    private Image cursorImage;
    private GameObject cursorObject;

    // Start is called before the first frame update
    void Start()
    {
        cursorObject = GameObject.FindGameObjectWithTag(GlobalValues.TagValues[GlobalValues.Tags.Cursor]);
        cursorImage = cursorObject.GetComponent<Image>();
        currentColor = idleColor;
    }

    public void setCursorValue(GlobalValues.CursorValue cursorValue)
    {
        switch (cursorValue)
        {
            case GlobalValues.CursorValue.Idle:
                cursorImage.color = idleColor;
                break;
            case GlobalValues.CursorValue.Invalid:
                cursorImage.color = invalidColor;
                break;
            case GlobalValues.CursorValue.Active:
                cursorImage.color = activeColor;
                break;
            default:
                cursorImage.color = currentColor;
                break;
        }
    }
}
