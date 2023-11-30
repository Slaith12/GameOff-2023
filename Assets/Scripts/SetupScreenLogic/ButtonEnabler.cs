using System.Collections;
using System.Collections.Generic;
using Structures;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public static ButtonEnabler instance;

    public List<Card> handCards;
    public List<LineupCardContainer> lineupCardContainers = new List<LineupCardContainer>(5);

    [SerializeField] private StoredPellets redPellets;
    [SerializeField] private StoredPellets yellowPellets;
    [SerializeField] private StoredPellets bluePellets;

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
            if (selectedItem.GetGameObject() == tempSelectedItem.GetGameObject())
            {
                this.selectedItem = tempSelectedItem;
                return;
            }
            else if (selectedItem.GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.lineup && tempSelectedItem.GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.lineup)
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
    }

    public void DiscardSelectedItem()
    {
        var alien = selectedItem.GetGameObject().GetComponent<Card>().GetParentTransform().GetComponent<LineupCardContainer>().alien;
        int attackPel = alien.attackPellets;
        redPellets.ChangeNumPellets(attackPel);

        int speedPel = alien.speedPellets;
        yellowPellets.ChangeNumPellets(speedPel);

        int defensePel = alien.defensePellets;
        bluePellets.ChangeNumPellets(defensePel);

        selectedItem.GetGameObject().GetComponent<Card>().GetParentTransform().GetComponent<LineupCardContainer>().SetCard(null);
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
