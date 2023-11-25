using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    /*
    0 = card
    1 = card container
    2 = pellet
    3 = pellet container
    */
    public int GetSelectableType();
    public GameObject GetGameObject();
}
