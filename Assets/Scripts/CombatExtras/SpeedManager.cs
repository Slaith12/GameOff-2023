using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    public enum Speed
    {
        Low,
        Medium,
        High,
        Warp
    }

    private Speed visualSpeed;
    Image image;
    [SerializeField] Sprite[] speedSprites;

    void Start()
    {
        image = GetComponent<Image>();
        visualSpeed = DataManager.instance.combatSpeed;
        UpdateSpeed();
    }

    //used by UI
    public void ChangeSpeed()
    {
        if (visualSpeed == Speed.High)
            visualSpeed = Speed.Low;
        else
            visualSpeed++;
        UpdateSpeed();
    }

    private void KeyChangeSpeed(bool reverse)
    {
        if (reverse)
        {
            if (visualSpeed > Speed.Low) visualSpeed--;
        }
        else
        {
            if (visualSpeed < Speed.High) visualSpeed++;
        }
        UpdateSpeed();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) KeyChangeSpeed(false);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) KeyChangeSpeed(true);
    }

    public void UpdateSpeed()
    {
        Time.timeScale = visualSpeed switch
        {
            Speed.Low => 1,
            Speed.Medium => 1.75f,
            Speed.High => 3,
            Speed.Warp => 4,
            _ => Time.timeScale
        };
        image.sprite = speedSprites[(int) visualSpeed];
        DataManager.instance.combatSpeed = visualSpeed;
    }
}