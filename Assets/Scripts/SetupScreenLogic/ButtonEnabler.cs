using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public static ButtonEnabler instance;

    public List<Card> handCards;
    [SerializeField] private List<LineupCardContainer> lineupCardContainers;
    [SerializeField] private List<Card> lineupCards;

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
        if (selectedItem.GetSelectableType() == 0)
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
        if(tempSelectedItem.GetSelectableType() == 0 && selectedItem.GetSelectableType() == 0)
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
        else if(selectedItem.GetSelectableType() == 2 && tempSelectedItem.GetSelectableType() == 3)
        {

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

    public void AddlineupCardObject(Card card)
    {
        lineupCards.Add(card);
    }
}
