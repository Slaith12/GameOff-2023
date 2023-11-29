using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public static ButtonEnabler instance;

    public List<Card> handCards;
    [SerializeField] private List<LineupCardContainer> lineupCardContainers = new List<LineupCardContainer>(5);

    private ISelectable selectedItem;

    private void Awake()
    {
        instance = this;
    }

    public void SetSelectedItem(GameObject lineupCardContainer)
    {
        if(selectedItem == null)
        {
            return;
        }
        if (selectedItem.GetSelectableType() == ISelectable.CARD)
        {
            Alien tempAlien = selectedItem.GetGameObject().GetComponent<Card>().alien;
            Card placeholderCard = null;
            foreach (Card card in handCards)
            {
                if (card == selectedItem.GetGameObject().GetComponent<Card>())
                {
                    placeholderCard = card;
                }
            }
            if(placeholderCard != null)
            {
                handCards.Remove(placeholderCard);
            }

            if(selectedItem.GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.lineup)
            {
                selectedItem.GetGameObject().GetComponent<Card>().GetParentTransform().GetComponent<LineupCardContainer>().SetCard(null);
            }

            Destroy(selectedItem.GetGameObject());
            lineupCardContainer.GetComponent<LineupCardContainer>().SetCard(tempAlien);
            selectedItem = null;
        }
        else
        {
            selectedItem = null;
            return;
        }
    }

    public void SetSelectedItem(ISelectable tempSelectedItem)
    {
        //when no item is selected
        if(selectedItem == null)
        {
            this.selectedItem = tempSelectedItem;
            return;
        }

        //if both items are cards
        if(tempSelectedItem.GetSelectableType() == ISelectable.CARD && selectedItem.GetSelectableType() == ISelectable.CARD)
        {
            if(selectedItem.GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.lineup && tempSelectedItem.GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.lineup)
            {
                Alien tempAlien = tempSelectedItem.GetGameObject().GetComponent<Card>().alien;
                Transform tempParent = tempSelectedItem.GetGameObject().GetComponent<Card>().GetParentTransform();
                Destroy(tempSelectedItem.GetGameObject());

                tempParent.GetComponent<LineupCardContainer>().SetCard(selectedItem.GetGameObject().GetComponent<Card>().alien);
                tempParent = selectedItem.GetGameObject().GetComponent<Card>().GetParentTransform();
                tempParent.GetComponent<LineupCardContainer>().SetCard(tempAlien);
                Destroy(selectedItem.GetGameObject());

                selectedItem = null;
                return;
            }
            else
            {
                this.selectedItem = tempSelectedItem;
                return;
            }
        }
        //if selectedItem is a stored pellet and the tempSelectedItem is an alien pellet container
        else if(selectedItem.GetSelectableType() == ISelectable.PELLET && tempSelectedItem.GetSelectableType() == ISelectable.PELLET_CONTAINER)
        {
            if (selectedItem.GetGameObject().GetComponent<StoredPellets>().GetNumPellets() == 0)
            {
                this.selectedItem = tempSelectedItem;
                return;
            }
            else if (selectedItem.GetGameObject().GetComponent<StoredPellets>().thisPelletType == StoredPellets.pelletType.red && tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.red)
            {
                selectedItem.GetGameObject().GetComponent<StoredPellets>().ChangeNumPellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeAttackPellets(1);

                selectedItem = null;
                return;
            }
            else if(selectedItem.GetGameObject().GetComponent<StoredPellets>().thisPelletType == StoredPellets.pelletType.yellow && tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.yellow)
            {
                selectedItem.GetGameObject().GetComponent<StoredPellets>().ChangeNumPellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeSpeedPellets(1);

                selectedItem = null;
                return;
            }
            else if (selectedItem.GetGameObject().GetComponent<StoredPellets>().thisPelletType == StoredPellets.pelletType.blue && tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.blue)
            {
                selectedItem.GetGameObject().GetComponent<StoredPellets>().ChangeNumPellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeDefensePellets(1);

                selectedItem = null;
                return;
            }
            else
            {
                this.selectedItem = tempSelectedItem;
                return;
            }
        }
        //if selectedItem is an alien pellet container and the tempSelectedItem is a stored pellet
        else if (selectedItem.GetSelectableType() == ISelectable.PELLET_CONTAINER && tempSelectedItem.GetSelectableType() == ISelectable.PELLET)
        {
            if (selectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.red && tempSelectedItem.GetGameObject().GetComponent<StoredPellets>().thisPelletType == StoredPellets.pelletType.red)
            {
                if (selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.attackPellets == 0)
                {
                    this.selectedItem = tempSelectedItem;
                    return;
                }
                selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeAttackPellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<StoredPellets>().ChangeNumPellets(1);

                selectedItem = null;
                return;
            }
            else if (selectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.yellow && tempSelectedItem.GetGameObject().GetComponent<StoredPellets>().thisPelletType == StoredPellets.pelletType.yellow)
            {
                if (selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.speedPellets == 0)
                {
                    this.selectedItem = tempSelectedItem;
                    return;
                }
                selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeSpeedPellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<StoredPellets>().ChangeNumPellets(1);

                selectedItem = null;
                return;
            }
            else if (selectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.blue && tempSelectedItem.GetGameObject().GetComponent<StoredPellets>().thisPelletType == StoredPellets.pelletType.blue)
            {
                if (selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.defensePellets == 0)
                {
                    this.selectedItem = tempSelectedItem;
                    return;
                }
                selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeDefensePellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<StoredPellets>().ChangeNumPellets(1);

                selectedItem = null;
                return;
            }
            else
            {
                this.selectedItem = tempSelectedItem;
                return;
            }
        }
        //if both selectedItem and tempSelectedItem are alien pellet containers
        else if (selectedItem.GetSelectableType() == ISelectable.PELLET_CONTAINER && tempSelectedItem.GetSelectableType() == ISelectable.PELLET_CONTAINER)
        {
            if (selectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.red && tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.red)
            {
                if (selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.attackPellets == 0)
                {
                    this.selectedItem = tempSelectedItem;
                    return;
                }
                selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeAttackPellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeAttackPellets(1);

                selectedItem = null;
                return;
            }
            else if (selectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.yellow && tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.yellow)
            {
                if (selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.speedPellets == 0)
                {
                    this.selectedItem = tempSelectedItem;
                    return;
                }
                selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeSpeedPellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeSpeedPellets(1);

                selectedItem = null;
                return;
            }
            else if (selectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.blue && tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().thisPelletType == LineupPellets.pelletType.blue)
            {
                if (selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.defensePellets == 0)
                {
                    this.selectedItem = tempSelectedItem;
                    return;
                }
                selectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeDefensePellets(-1);
                tempSelectedItem.GetGameObject().GetComponent<LineupPellets>().cardContainer.alien.ChangeDefensePellets(1);

                selectedItem = null;
                return;
            }
            else
            {
                this.selectedItem = tempSelectedItem;
                return;
            }
        }
        else
        {
            this.selectedItem = tempSelectedItem;
        }

    }

    public void DiscardSelectedItem()
    {
        Debug.Log("runs");
        Destroy(selectedItem.GetGameObject());
        selectedItem = null;
    }

    public ISelectable GetSelectedItem()
    {
        if(selectedItem != null)
        {
            return selectedItem;
        }
        return null;
    }

    public void DeleteSelectedItem()
    {
        selectedItem = null;
    }

    public void AddHandCardObject(Card card)
    {
        handCards.Add(card);
    }
}
