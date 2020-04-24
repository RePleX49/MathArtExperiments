using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVPicker : MonoBehaviour
{
    Vector2 ScreenCenter;
    Vector2 MousePos;


    // Start is called before the first frame update
    void Start()
    {
        ScreenCenter.x = Screen.width / 2;
        ScreenCenter.y = Screen.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Input.mousePosition;
        float deltaX = MousePos.x - ScreenCenter.x;
        float deltaY = MousePos.y - ScreenCenter.y;
        float hypon = Mathf.Sqrt(deltaY * deltaY + deltaX * deltaX);

        float theta = Mathf.Atan2(deltaY, deltaX);

        Color newColor = Color.HSVToRGB(Mathf.Sin(theta), 1, Mathf.Clamp(hypon, 0.0f, 1.0f));

        Camera.main.backgroundColor = newColor;
    }
}
