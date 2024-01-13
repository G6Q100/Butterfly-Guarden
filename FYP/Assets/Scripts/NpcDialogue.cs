using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NpcDialogue : MonoBehaviour
{
    GameObject player;

    private Camera mainCamera;

    public Vector3 endPos;
    public Vector3 endRot;
    private DialogueSystem dialogueSystem;

    public string[] sentence, sentence2, sentence3;

    public int talkTime = 0;

    public Material[] material;

    public CutsceneManager cutsceneManager;

    public GameObject Mark;

    private void Start()
    {
        cutsceneManager = GameObject.FindObjectOfType<CutsceneManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (GameObject.Find("Dialogue System") != null)
        {
            dialogueSystem = GameObject.Find("Dialogue System").GetComponent<DialogueSystem>();
            dialogueSystem.dialogueImage.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (GameObject.Find("Dialogue System") != null && dialogueSystem == null)
        {
            dialogueSystem = GameObject.Find("Dialogue System").GetComponent<DialogueSystem>();
            dialogueSystem.dialogueImage.gameObject.SetActive(false);
        }
        if (dialogueSystem.talking)
            player.GetComponent<PlayerController>().canMove = -0.1f;

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
       
            if (Physics.Raycast(ray, out hit, 100))
            { 
                if (hit.transform == gameObject.transform && dialogueSystem.talking == false &&
                    Time.timeScale > 0)
                {

                    if (gameObject.name == "Shop NPC" && EventSettings.instance.quest == 2)
                    {
                        EventSettings.instance.questNameText.text = "Disappeared fertilizer ";
                        EventSettings.instance.questText.text = "Help trader to find the fertillizer.";
                        EventSettings.instance.haveQuest = true;
                        //Mark.SetActive(false);
                        EventSettings.instance.mission.SetInteger("QuestType", 3);
                        EventSettings.instance.quest = 3;
                        PlayerPrefs.SetInt("quest", EventSettings.instance.quest);
                    }

                    if (EventSettings.instance.haveQuest == false || EventSettings.instance.questDone == true)
                    {
                        EventSettings.instance.questDone = false;
                        cutsceneManager.Cutscene(mainCamera.transform.position, endPos,
                            mainCamera.transform.eulerAngles, endRot);
                        if (gameObject.name == "Butterfly Enthusiast" && EventSettings.instance.quest == 0)
                        {
                            EventSettings.instance.questNameText.text = "The lost scholar in the forest";
                            EventSettings.instance.questText.text = "Bring her back to the village";
                            EventSettings.instance.haveQuest = true;
                            EventSettings.instance.mission.SetInteger("QuestType", 1);
                            Mark.SetActive(false);
                            EventSettings.instance.quest = 1;
                            PlayerPrefs.SetInt("quest", EventSettings.instance.quest);
                        }
                        
                    }
                    else if (EventSettings.instance.quest > 2)
                    {
                        cutsceneManager.Cutscene(mainCamera.transform.position, endPos,
                            mainCamera.transform.eulerAngles, endRot);
                    }

                    dialogueSystem.dialogueImage.SetActive(true);
                    dialogueSystem.dialogueText.text = "";
                    if (talkTime <= 0 || (gameObject.name == "Shop NPC" && EventSettings.instance.quest != 2 &&
                        EventSettings.instance.quest != 3 && EventSettings.instance.quest != 4))
                    {
                        talkTime++;
                        dialogueSystem.sentence = sentence;
                    }
                    else if (gameObject.name == "Shop NPC" && EventSettings.instance.quest == 4)
                    {
                        talkTime++;
                        EventSettings.instance.quest = 5;
                        PlayerPrefs.SetInt("quest", EventSettings.instance.quest);
                        EventSettings.instance.mission.SetInteger("QuestType", 2);
                        EventSettings.instance.questDone = true;
                        ItemSystem.instance.fertilizer += 10;
                        ItemSystem.instance.itemText2.text = "Manure: " + ItemSystem.instance.fertilizer.ToString();
                        dialogueSystem.sentence = sentence3;
                    }
                    else
                        dialogueSystem.sentence = sentence2;

                    StartCoroutine(dialogueSystem.Type());
                }


            }
        }


        // Shop
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform == gameObject.transform && gameObject.name == "Shop NPC" &&
                    dialogueSystem.talking == false && Time.timeScale > 0 && EventSettings.instance.quest > 4)
                {
                    if (Time.timeScale != 0)
                    {
                        Time.timeScale = 0;
                        if (!ItemSystem.instance.shopUI.activeInHierarchy)
                            ItemSystem.instance.shopUI.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (GameObject.Find("Dialogue System") != null && Time.timeScale != 0)
        {
            if (dialogueSystem.talking == false)
            {
                if(GetComponent<Renderer>() != null)
                {
                    if (GetComponent<Renderer>().material != material[1])
                    {
                        GetComponent<Renderer>().material = material[1];
                    }
                    else
                    {
                        GetComponent<Renderer>().material = material[0];
                    }
                }
            }
        }
        if (GameObject.Find("InfoText") != null && dialogueSystem.talking == false)
        {
            GameObject.Find("InfoText").transform.position = Input.mousePosition + Vector3.up * 50;
            if(gameObject.name == "Shop NPC" && EventSettings.instance.quest > 4)
            {
                GameObject.Find("InfoText").GetComponent<Text>().text = "Shop: \n Talk: ";
                GameObject.Find("LeftClick").GetComponent<Image>().enabled = true;
                GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
                GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                GameObject.Find("RightClickDown").GetComponent<Image>().enabled = true;
            }
            else
            {
                GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
                GameObject.Find("RightClick").GetComponent<Image>().enabled = true;
                GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                GameObject.Find("InfoText").GetComponent<Text>().text = "Talk: ";
            }

        }

    }

    private void OnMouseExit()
    {
        if (GetComponent<Renderer>() != null)
        {
            if (GetComponent<Renderer>().material != material[0])
            {
                GetComponent<Renderer>().material = material[0];
            }
        }

        if (GameObject.Find("InfoText") != null)
        {
            GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
            GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoText").GetComponent<Text>().text = "";
        }
    }
}
