using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MusicButton : MonoBehaviour
{
    private bool playMusic = true;
    private GameObject backgroundMusic;

    private Image image;
    public Sprite mute, unmute;

    private void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic");
        image = GetComponent<Image>();
    }

    public void MuteOrUnmuteGame()
    {
        if (playMusic == false)
        {
            backgroundMusic.GetComponent<AudioSource>().Play();
            image.sprite = unmute;
            playMusic = true;
        }
        else
        {
            backgroundMusic.GetComponent<AudioSource>().Pause();
            image.sprite = mute;
            playMusic = false;
        }
    }
}
