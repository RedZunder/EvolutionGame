using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMenu : MonoBehaviour
{
    [SerializeField] string sceneToLoad;

    public void swapMenu()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
