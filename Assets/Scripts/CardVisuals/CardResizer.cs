using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardResizer : MonoBehaviour
{
    [SerializeField] List<TMP_Text> textBoxes; //textboxes can't simply be resized, their font sizes need to be updated as well.
    [SerializeField] List<ImageResizer> resizers; //ImageResizers also use special properties for their size
    //all other components use anchors to determine their size so they don't need to be included as long as the main card object is resized

    float m_currentSize;
    public float currentSize { get => m_currentSize; set => SetSize(value); }

    public void Awake()
    {
        if(m_currentSize == 0)
            m_currentSize = 1;
    }

    public void SetSize(float newSize)
    {
        RectTransform rect = (RectTransform)transform;
        float scale = newSize / m_currentSize;

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.sizeDelta.x * scale);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rect.sizeDelta.y * scale);

        foreach(TMP_Text text in textBoxes)
        {
            if(text.enableAutoSizing)
            {
                text.fontSizeMax *= scale;
                text.fontSizeMin *= scale;
            }
            else
            {
                text.fontSize *= scale;
            }    
        }

        foreach(ImageResizer resizer in resizers)
        {
            resizer.defaultHeight *= scale;
            resizer.defaultWidth *= scale;
        }

        m_currentSize = newSize;
    }
}
