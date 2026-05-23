using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleScreen : MonoBehaviour
{
    private float scaleValue;
    CanvasScaler ss;
    void Start()
    {
        ss = this.GetComponent<CanvasScaler>();
        ScaleScr();
    }
    void Update()
    {
        ScaleScr();
    }
    public void ScaleScr()
    {
        float x = Screen.width;
        float y = Screen.height;
        float referenceWidth = ss.referenceResolution.x;
        float referenceHeight = ss.referenceResolution.y;
        scaleValue=(int)(((float)x/y)/((float)referenceWidth/referenceHeight)*10);

        ss.matchWidthOrHeight = scaleValue/10;
    }
}
