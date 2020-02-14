using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPixels : MonoBehaviour
{
    RawImage pixelImage;
    Texture2D drawing;

    // Start is called before the first frame update
    void Start()
    {
        SetupTexture();

        AdaWeave();
    }

    // Update is called once per frame
    void Update()
    {
        // RandColor();
    }

    void RandColor()
    {
        for (int x = 0; x < drawing.width; x++)
        {
            for(int y = 0; y < drawing.height; y++)
            {
                drawing.SetPixel(x, y, new Color(Random.value, Random.value, Random.value));
            }
        }

        drawing.Apply();
    }

    void SetupTexture()
    {
        pixelImage = GetComponent<RawImage>();

        drawing = new Texture2D(100, 100, TextureFormat.ARGB32, false);
        drawing.filterMode = FilterMode.Point;

        Color defaultColor = Color.red;
        defaultColor.a = 0;

        // create a color array and set every element to red
        Color[] colorArray = new Color[drawing.height * drawing.width];
        for (int i = 0; i < colorArray.Length; i++)
        {
            colorArray[i] = defaultColor;
        }

        // set and apply pixels (make sure to apply otherwise it pixels won't appear)
        drawing.SetPixels(colorArray);

        RandColor();

        // assign our texture to rawimage
        pixelImage.texture = drawing;
    }

    void AdaWeave()
    {
        //"a", "a", "a", "b", "a", "b", "b", "b", "a", "c", "a", "c", "c", "c", "b", "c", "b", "c"

        //"a", "a", "a", "b", "a", "b", "a", "c", "a", "c", "b", "c", "b", "c", "b", "b", "c", "c" 

        string weavePattern = "aaababacacbcbcbbcc";

        Color[] colorPattern = new Color[drawing.height * drawing.width];

        for (int x = 0; x < drawing.width; x++)
        {
            for (int y = 0; y < drawing.height; y++)
            {
                if (weavePattern[x % weavePattern.Length] == weavePattern[y % weavePattern.Length])
                {
                    // match set cell black
                    colorPattern[(x*drawing.height) + y] = Color.red;
                }
                else
                {
                    // no match set cell white
                    colorPattern[(x*drawing.height) + y] = Color.green;
                }
            }
        }

        drawing.SetPixels(colorPattern);
        drawing.Apply();
    }
}
