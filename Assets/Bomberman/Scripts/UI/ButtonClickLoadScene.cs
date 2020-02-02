using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickLoadScene : MonoBehaviour, IButton
{
    public string sceneToLoad = "Gameplay";
    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
