using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoredPellets : MonoBehaviour, ISelectable
{
    public enum pelletType
    {
        red,
        yellow,
        blue,
    }
    public pelletType thisPelletType;
    [SerializeField] private int numPellets;
    [SerializeField] private TextMeshProUGUI amountText;
    private Button pelletButton;

    private void Awake()
    {
        pelletButton = GetComponent<Button>();
        pelletButton.onClick.AddListener(delegate { ButtonEnabler.instance.SetSelectedItem(this); });
        amountText.SetText(this.numPellets.ToString());
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public int GetSelectableType()
    {
        return 2;
    }

    public void ChangeNumPellets(int numPellets)
    {
        this.numPellets += numPellets;
        amountText.SetText(this.numPellets.ToString());
    }

    public int GetNumPellets()
    {
        return numPellets;
    }
}
