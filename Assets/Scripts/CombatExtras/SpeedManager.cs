using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    public enum Speed { Low, Medium, High }

    Speed currentSpeed;
    Image image;
    [SerializeField] Sprite[] speedSprites;

    void Start()
    {
        image = GetComponent<Image>();
        currentSpeed = DataManager.instance.combatSpeed;
        UpdateSpeed();
    }

    //used by UI
    public void ChangeSpeed()
    {
        if (currentSpeed == Speed.High)
            currentSpeed = Speed.Low;
        else
            currentSpeed++;
        UpdateSpeed();
    }

    public void UpdateSpeed()
    {
        switch(currentSpeed)
        {
            case Speed.Low:
                Time.timeScale = 1;
                break;
            case Speed.Medium:
                Time.timeScale = 1.75f;
                break;
            case Speed.High:
                Time.timeScale = 3;
                break;
        }
        image.sprite = speedSprites[(int)currentSpeed];
        DataManager.instance.combatSpeed = currentSpeed;
    }
}
