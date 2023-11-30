using System;
using Structures;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineupPellets : MonoBehaviour, ISelectable, IPointerClickHandler
{
    public PelletType thisPelletType;
    public LineupCardContainer cardContainer;
    public StoredPellets storage;


    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public int GetSelectableType()
    {
        return 3;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var alien = cardContainer.alien;
        switch (eventData.button)
        {
            // try to take a pellet from storage.
            case PointerEventData.InputButton.Left when !storage.TryTake():
                return;
            case PointerEventData.InputButton.Left:
                switch (thisPelletType)
                {
                    case PelletType.Red:
                        alien.ChangeAttackPellets(1);
                        break;
                    case PelletType.Blue:
                        alien.ChangeDefensePellets(1);
                        break;
                    case PelletType.Yellow:
                        alien.ChangeSpeedPellets(1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            case PointerEventData.InputButton.Right:
            {
                var v = thisPelletType switch
                {
                    PelletType.Red => alien.attackPellets,
                    PelletType.Blue => alien.defensePellets,
                    PelletType.Yellow => alien.speedPellets,
                    _ => throw new ArgumentOutOfRangeException()
                };
                if (v <= 0) return;
                storage.Return();
                switch (thisPelletType)
                {
                    case PelletType.Red:
                        alien.ChangeAttackPellets(-1);
                        break;
                    case PelletType.Blue:
                        alien.ChangeDefensePellets(-1);
                        break;
                    case PelletType.Yellow:
                        alien.ChangeSpeedPellets(-1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            }
        }
    }
}