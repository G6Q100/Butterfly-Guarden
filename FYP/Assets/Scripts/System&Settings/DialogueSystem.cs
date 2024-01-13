using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public string[] sentence;
    public int dialogueLine;
    public Text dialogueText;

    public bool talking = false, stop = false;

    public GameObject continueImage, dialogueImage;

    public CutsceneManager cutsceneManager;

    private void Start()
    {
        cutsceneManager = GameObject.FindObjectOfType<CutsceneManager>();
        sentence = new string[1];
        continueImage.SetActive(false);
    }

    private void Update()
    {
        if(dialogueText.text == sentence[dialogueLine])
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            {
                    TypeNextLine();
            }
            continueImage.SetActive(true);
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            {
                stop = true;
            }

            if (continueImage.activeInHierarchy)
                continueImage.SetActive(false);
        }
    }

    public IEnumerator Type()
    {
        if(talking == false)
            talking = true;
        foreach (char letter in sentence[dialogueLine].ToCharArray())
        {
            dialogueText.text += letter;
            if (stop == false)
            {
                yield return new WaitForSeconds(1 / 30);
            }
        }
        stop = false;
        yield return new WaitForEndOfFrame();
    }

    public void TypeNextLine()
    {
        if(dialogueLine == sentence.Length - 1)
        {
            if (talking == true)
                talking = false;
            dialogueLine = 0;
            dialogueText.text = "";
            dialogueImage.gameObject.SetActive(false);
            if(cutsceneManager.cutsceneCam.gameObject.activeInHierarchy)
                cutsceneManager.StopCutscene();
        }
        else
        {
            dialogueLine++;
            dialogueText.text = "";
            StartCoroutine(Type());
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (talking == true)
            talking = false;
        dialogueLine = 0;
        dialogueText.text = "";
        dialogueImage.gameObject.SetActive(false);
    }
}
