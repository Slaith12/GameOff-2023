using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public static ButtonEnabler instance;

    [SerializeField] private List<Card> handCards;
    [SerializeField] private List<LineupCardContainer> lineupCardContainers;
    [SerializeField] private List<Card> lineupCards;

    private ISelectable selectedItem;

    private void Awake()
    {
        instance = this;
    }

    public void SetSelectedItem(GameObject lineupCardContainer, int garbage)
    {
        Debug.Log("Button Press Detected");
        if(selectedItem.GetSelectableType() == 0)
        {
            Alien tempAlien = selectedItem.GetGameObject().GetComponent<Card>().alien;

            Debug.Log(tempAlien.cardDataSO.unchangingName);

            Destroy(selectedItem.GetGameObject());
            for(int i = 0; i < handCards.Count; i++)
            {
                if(handCards[i] == null)
                {
                    handCards.RemoveAt(i);
                }
            }
            lineupCardContainer.GetComponent<LineupCardContainer>().SetCard(tempAlien);
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
