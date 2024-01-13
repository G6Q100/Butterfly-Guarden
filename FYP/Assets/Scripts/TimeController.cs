using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public GameObject lights;                   // The light game object

    private int lightType = 1;                  // The current time period(1: morning / 2: afternoon/ 3: night)


    public float time = 96;                    // Time var for counting how many second had passed
    public int timeMode = 1;                   // The current time period(for the clock function)
    public float timeMove = 1;                    // The time move when this var is possitive

    public int day = 1, season = 1, year = 1;
    public string seasonText = ", Spring";

    public Text timeText, dateText, yearText, weatherText;

    public bool original = false;               // Bool to check is this object only have one existed
    public static TimeController instance = null; // original

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
        timeText = gameObject.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lights = GameObject.Find("Directional Light");

        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            lightType = PlayerPrefs.GetInt("lightType");
            time = PlayerPrefs.GetFloat("time");
            timeMode = PlayerPrefs.GetInt("timeMode");
            timeMove = PlayerPrefs.GetFloat("timeMove");
            day = PlayerPrefs.GetInt("day");
            season = PlayerPrefs.GetInt("season");
            year = PlayerPrefs.GetInt("year");
        }

        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        Clock();                // Time function

        PlayerPrefs.SetInt("lightType", lightType);
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.SetInt("timeMode", timeMode);
        PlayerPrefs.SetFloat("timeMove", timeMove);
        PlayerPrefs.SetInt("day", day);
        PlayerPrefs.SetInt("season", season);
        PlayerPrefs.SetInt("year", year);
    }
    void Clock()
    {
        timeMove += Time.deltaTime;         // time is moving
        if (timeMove > 0)
        {
            time += Time.deltaTime;
        }
        if (time <= 60 && time > 2 && timeMode == 4)
        {
            timeMode = 1;
            StartCoroutine(TimeChange2());
        }

        if (time >= 84 && timeMode == 4)
        {
            timeMode = 1;
            StartCoroutine(TimeChange());
        }
        else if (time >= 192 && timeMode == 1)
        {
            timeMode = 2;
            StartCoroutine(TimeChange());
        }
        else if (time >= 228 && timeMode == 2)
        {
            timeMode = 3;
            StartCoroutine(TimeChange());
        }
        else if (time >= 288)
        {
            timeMode = 4;
            // move to next day
            time -= 288;
            if(day < 7)
                day++;
            else
            {
                day = 1;
                if (season < 4)
                    season++;
                else
                {
                    season = 1;
                    year++;
                }
            }

            switch (season)
            {
                case 1:
                    seasonText = ", Spring";
                    break;
                case 2:
                    seasonText = ", Summer";
                    break;
                case 3:
                    seasonText = ", Fall";
                    break;
                case 4:
                    seasonText = ", Winter";
                    break;
                default:
                    break;
            }
        }

        if (lights != null)
        {
            lights.GetComponent<LightTime>().lightType = lightType;
        }
        else
        {
            lights = GameObject.Find("Directional Light");
        }

        // time ui
        string hourText = ((int)(time / 12)).ToString();
        string minuteText = ((int)((time % 12)) * 5).ToString();
        
        // timer set up
        if(time / 12 < 10)
            hourText = "0" + ((int)(time / 12)).ToString();
        if ((time % 12) * 5< 10)
            minuteText = "0" + ((int)((time % 12)) * 5).ToString();

        // time text
        yearText.text = "Year " + year.ToString();
        dateText.text = "Day " + day.ToString() + seasonText;
        timeText.text = hourText + ":" + minuteText;

        string currentTime = "Morning", currentWeather = "Average";
        switch (lightType)
        {
            case 1:
                currentTime = "Morning";
                break;
            case 2:
                currentTime = "Afternoon";
                break;
            case 3:
                currentTime = "Evening";
                break;
            default:
                break;
        }
        switch (WeatherController.instance.rainType)
        {
            case 1:
                currentWeather = ", Average";
                break;
            case 2:
                currentWeather = ", Raining";
                break;
            case 3:
                currentWeather = ", Storm";
                break;
            default:
                break;
        }

        weatherText.text = currentTime + currentWeather;
    }

    IEnumerator TimeChange()
    {
        int rand = Random.Range(0, 10);
        
        if(rand < 1)
        {
            if (season != 4)
            {
                WeatherController.instance.rainType = 3;
                WeatherController.instance.snowType = 1;
            }
            else
            {
                WeatherController.instance.rainType = 1;
                WeatherController.instance.snowType = 3;
            }
        }
        else if (rand < 3)
        {
            if (season != 4)
            {
                WeatherController.instance.rainType = 2;
                WeatherController.instance.snowType = 1;
            }
            else
            {
                WeatherController.instance.rainType = 1;
                WeatherController.instance.snowType = 2;
            }
        }
        else
        {
            WeatherController.instance.rainType = 1;
            WeatherController.instance.snowType = 1;
        }

        GameObject.Find("TimeChangePanel").GetComponent<Animator>().Play("TimeChange");
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = -2;
        timeMove = -2;
        yield return new WaitForSeconds(1);
        if (lightType < 3)
            lightType++;
        else
            lightType = 1;

        yield return new WaitForSeconds(1);
    }

    IEnumerator TimeChange2()
    {
        for (int i = 0; i < DirtManager.instance.dirtGroup.Length; i++)
        {
            if (timeMove > 0 &&
                DirtManager.instance.dirtGroup[i].state != "normal" &&
                DirtManager.instance.dirtGroup[i].state != "hovered" &&
                DirtManager.instance.dirtGroup[i].state != "dead")
            {
                DirtManager.instance.dirtGroup[i].growthTime -= 90;
                DirtManager.instance.dirtGroup[i].dryTime -= 90;
            }
        }
        ItemSystem.instance.fertilizerNum = 10;
        ItemSystem.instance.fertilizerNumText.text = "We still have " + ItemSystem.instance.fertilizerNum + " of them to sell today!";

        GameObject.Find("TimeChangePanel").GetComponent<Animator>().Play("TimeChange");
        GameObject.Find("Player").GetComponent<PlayerController>().canMove = -2;
        timeMove = -2;
        yield return new WaitForSeconds(1);
        if (lightType < 3)
            lightType++;
        else
            lightType = 1;

        time = 90;
        timeMode = 1;
        if (day < 7)
            day++;
        else
        {
            day = 1;
            if (season < 4)
                season++;
            else
            {
                season = 1;
                year++;
            }
        }

        switch (season)
        {
            case 1:
                seasonText = ", Spring";
                break;
            case 2:
                seasonText = ", Summer";
                break;
            case 3:
                seasonText = ", Fall";
                break;
            case 4:
                seasonText = ", Winter";
                break;
            default:
                break;
        }


        LevelManager.instance.spawnPoint = new Vector3(-5.9f, -3.907f, -22);
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1);
    }

    private void OnLevelWasLoaded(int level)
    {
        original = true;
    }
}
