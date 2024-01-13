using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject winScreen;

    void Start()
    {
        winScreen.SetActive(false);
    }

    void Update()
    {
        if(LevelSystem.instance.lv >= 10)
            winScreen.SetActive(true);
    }
}
