using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nextScene;
    // Start is called before the first frame update

    public void StartGame()
    {
        DataManager.instance.GoToSetup();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
