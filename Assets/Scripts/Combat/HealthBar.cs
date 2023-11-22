using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMesh text;
    [SerializeField] Transform bar;

    public void SetHealth(int remaining, int max)
    {
        if (remaining < 0)
            remaining = 0;
        text.text = $"{remaining}/{max}";
        float percentage = (float)remaining / max;
        bar.localScale = new Vector2(percentage, 1);
        bar.localPosition = new Vector2((percentage - 1) / 2, 0);
    }
}
