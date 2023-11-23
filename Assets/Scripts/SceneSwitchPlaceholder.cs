using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchPlaceholder : MonoBehaviour
{
    const string COMBAT_SCENE = "CombatTest";

    public void GoToCombat()
    {
        SceneManager.LoadScene(COMBAT_SCENE);
    }
}
