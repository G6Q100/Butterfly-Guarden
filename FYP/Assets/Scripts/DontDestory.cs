using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestory : MonoBehaviour
{
    private bool original = false;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("BackgroundMusic").Length > 1 && original == false)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if ((level == SceneManager.GetSceneByName("WinScreen").buildIndex &&
            gameObject.name != "BackgroundMusic") ||( level == 0 &&
            gameObject.name != "BackgroundMusic"))
            Destroy(gameObject);
        else if (gameObject.name == "BackgroundMusic")
        {
            original = true;
        }
    }
}
