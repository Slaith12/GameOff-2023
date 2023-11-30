using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatUIManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text panelText;

    public void SetPanelText(string text)
    {
        panelText.text = text;
    }
}
