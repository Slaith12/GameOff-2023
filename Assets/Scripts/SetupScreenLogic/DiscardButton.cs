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
        if(ButtonEnabler.instance.GetSelectedItem() == null)
        {
            return;
        }
        if(ButtonEnabler.instance.GetSelectedItem().GetSelectableType() == 0)
        {
            if(ButtonEnabler.instance.GetSelectedItem().GetGameObject().GetComponent<Card>().currentCardState == Card.cardState.lineup)
            {
                button.enabled = true;
                return;
            }
        }
        button.enabled = false;
    }
}
