using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDataManager : MonoBehaviour
{
    public static AlienDataManager alienDataManager { get; private set; }

    [SerializeField] private List<Alien> alienData;

    private void Awake()
    {
        alienDataManager = this;
        DontDestroyOnLoad(gameObject);
    }
}
