using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineupPellets : MonoBehaviour, ISelectable
{
    public enum pelletType
    {
        red,
        yellow,
        blue,
    }
    public pelletType thisPelletType;
    public LineupCardContainer cardContainer;

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public int GetSelectableType()
    {
        return 3;
    }
}
