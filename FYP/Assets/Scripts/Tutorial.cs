using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorials, background;

    private void Start()
    {
        if (PlayerPrefs.GetInt("tutorial") != 1)
            StartCoroutine(Tutor());
        else
        {
            gameObject.SetActive(false);
            background.SetActive(false);
        }
    }

    IEnumerator Tutor()
    {
        yield return new WaitForSeconds(5);
        Time.timeScale = 0;
        tutorials.SetActive(true);
        background.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("tutorial", 1);
        gameObject.SetActive(false);
        background.SetActive(false);
    }
}
