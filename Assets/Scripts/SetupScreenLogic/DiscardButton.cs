using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { ButtonEnabler.instance.DiscardSelectedItem(); });
    }

    private void Update()
    {
        button.enabled = false;
        ISelectable selectedItem = ButtonEnabler.instance.GetSelectedItem();
        if(selectedItem != null)
        {
            if(selectedItem.GetSelectableType() == 0)
            {
                if(selectedItem.GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.lineup)
                {
                    button.enabled = true;
                }
            }
        }
        if (button.enabled && Input.GetKeyDown(KeyCode.X))
        {
            ButtonEnabler.instance.DiscardSelectedItem();
        }
    }
}
