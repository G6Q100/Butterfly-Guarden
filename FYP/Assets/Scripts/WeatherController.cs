using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeatherController : MonoBehaviour
{
    GameObject rain;                            // The rain game object
    GameObject snow;                            // The snow game oject

    public int rainType = 1;                   // The rain type(1: normal/ 2: big rain/ 3: no rain)
    public int snowType = 1;                   // The rain type(1: normal/ 2: big snow/ 3: no snow)

    public static WeatherController instance = null; // original
    
    private void Awake()
    {
        if (instance == null)      // original check
        {
            instance = this;
        }
        else if (instance != this)      // original check
        {
            Destroy(gameObject);
        }       
    }

    private void Start()
    {
        // find game object
        rain = GameObject.Find("Rain");
        snow = GameObject.Find("Snow");
        //snow.SetActive(false);      // Disable snow

        rain.SetActive(true);
        snow.SetActive(true);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    if (rainType < 3)
        //        rainType++;
        //    else
        //        rainType = 1;
        //}

        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    if (snowType < 3)
        //        snowType++;
        //    else
        //        snowType = 1;
        //}

        Weather();              // Weather function
    }

    void Weather()
    {
        // Update Weather type
        rain.GetComponent<Rain>().rainType = rainType;
        snow.GetComponent<Snow>().snowType = snowType;
    }

    
    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("WinScreen").buildIndex || level == 0)
            Destroy(gameObject);
    }
}
