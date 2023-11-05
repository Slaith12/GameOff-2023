using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//NOTE: if you want to offset a sprite so that it isn't 

[ExecuteAlways] //this will make it so card images are resized while in edit mode as well.
public class ImageSize : MonoBehaviour
{
    //Shrink = shrink image so none of it is cut off
    //Expand = expand image to take up entire space, allowing some of it to be cut off
    enum ResizeMode { Shrink, Expand }

    [SerializeField] float defaultWidth;
    [SerializeField] float defaultHeight;
    [SerializeField] ResizeMode resizeMode;
    [SerializeField] Image image;

    void Update() //during edit mode, this is only called when something changes in the scene, not every frame
    {
        float defaultRatio = defaultWidth / defaultHeight;
        Vector2 imageSize = image.sprite.bounds.size;
        float imageRatio = imageSize.x / imageSize.y;

        float ratioDiff = imageRatio / defaultRatio; //higher = image is wider, lower = image is taller

        //remember: shrink mode = one side will be smaller, so multiply by <1 or divide by >1. opposite for expand

        if(imageRatio >= 1) //image is wider than default bounds
        {
            if(resizeMode == ResizeMode.Shrink) 
            {
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultWidth);
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultHeight / imageRatio);
            }
            else
            {
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultWidth * imageRatio);
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultHeight);
            }
        }
        else //image is taller than default bounds, imageRatio < 1
        {
            if (resizeMode == ResizeMode.Shrink)
            {
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultWidth * imageRatio);
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultHeight);
            }
            else
            {
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultWidth);
                image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, defaultHeight / imageRatio);
            }
        }
    }
}
