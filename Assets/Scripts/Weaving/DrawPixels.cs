using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPixels : MonoBehaviour
{
    RawImage pixelImage;
    Texture2D drawing;

    /* Nice Patterns
     * "a", "a", "a", "b", "a", "b", "b", "b", "a", "c", "a", "c", "c", "c", "b", "c", "b", "c"
     * "a", "a", "a", "b", "a", "b", "a", "c", "a", "c", "b", "c", "b", "c", "b", "b", "c", "c"  
     * aabbccaaabbbccc
     * accbbbcca (SwapIndex = 3)
     * aabbccddeeffaabbccddeeff (SwapIndex = 8)
     * aabbccddeeffaabbccddeeff (SwapIndex = 10) (Speed = 0.05)
     */

    public string weavePattern = "aabbccaaabbbccc";

    public float ShiftSpeed = 0.02f;

    // index that ShiftString function will use to swap the final character
    [Range(0, 14)]
    public int SwapIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetupTexture();

        AdaWeave();

        InvokeRepeating("ShiftString", 0.0f, ShiftSpeed);
        InvokeRepeating("AdaWeave", 0.0f, ShiftSpeed);
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
                    colorPattern[(x * drawing.height) + y] = Color.yellow;
                }
            }
        }

        drawing.SetPixels(colorPattern);
        drawing.Apply();
    }

    void ShiftString()
    {
        // NewString array will have the shifted pattern chars added into it
        char[] NewString = new char[weavePattern.Length];

        for (int i = 0; i < weavePattern.Length; i++)
        {
            if(i == weavePattern.Length - 1)
            {
                // make the last element of NewString the SwapIndex from the weave pattern
                NewString[i] = weavePattern[SwapIndex];
            }
            else
            {
                // Shift letters down by one space
                NewString[i] = weavePattern[i + 1];
            }
        }

        weavePattern = new string(NewString);
    }
}
