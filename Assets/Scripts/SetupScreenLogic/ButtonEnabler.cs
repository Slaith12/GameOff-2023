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
        if(selectedItem == null)
        {
            this.selectedItem = tempSelectedItem;
            return;
        }

        if ((selectedItem.GetSelectableType() == 0) && (tempSelectedItem.GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.hand))
        {
            this.selectedItem = tempSelectedItem;
            return;
        }

        if (tempSelectedItem.GetSelectableType() == 0 && selectedItem.GetSelectableType() == 0)
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
