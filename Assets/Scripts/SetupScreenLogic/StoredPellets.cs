using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoredPellets : MonoBehaviour
{
    [SerializeField] private int numPellets;
    [SerializeField] private TextMeshProUGUI amountText;
    private Button pelletButton;

    private void Awake()
    {
        pelletButton = GetComponent<Button>();
        amountText.SetText(numPellets.ToString());
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ChangeNumPellets(int value)
    {
        this.numPellets += value;
        amountText.SetText(this.numPellets.ToString());
    }

    public int GetNumPellets()
    {
        return numPellets;
    }

    public void SetNumPellets(int value)
    {
        this.numPellets = value;
    }

    public bool TryTake()
    {
        if (numPellets <= 0) return false;
        ChangeNumPellets(-1);
        return true;
    }

    public void Return()
    {
        ChangeNumPellets(1);
    }
}