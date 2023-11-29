using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public const int CARD = 0;
    public const int CARD_CONTAINER = 1;
    public const int PELLET = 2;
    public const int PELLET_CONTAINER = 3;

    public int GetSelectableType();
    public GameObject GetGameObject();
}
