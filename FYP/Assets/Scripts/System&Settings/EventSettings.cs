using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventSettings : MonoBehaviour
{
    public Text questText, questNameText;
    public Button infoButton;
    public Image infoImage;

    public Animator mission;
    public Sprite catalogNum;

    public int quest = 0;

    public bool haveQuest = false, questDone = false;

    public static EventSettings instance = null; // original
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
        if(PlayerPrefs.GetInt("quest") > 0)
        {
            quest = PlayerPrefs.GetInt("quest");
            if (PlayerPrefs.GetInt("haveQuest") == 1)
                haveQuest = true;
            else
                haveQuest = false;

            if (PlayerPrefs.GetInt("questDone") == 1)
                questDone = true;
            else
                questDone = false;

            if (quest == 1)
            {
                quest = 2;
                PlayerPrefs.SetInt("quest", quest);
                questNameText.text = "The lost scholar in the forest";
                questText.text = "Bring her back to the village";
                mission.SetInteger("QuestType", 1);
                infoImage.sprite = catalogNum;
                infoImage.gameObject.GetComponent<Animator>().SetBool("Info", true);
                questDone = true;
            }
            else if (quest == 3)
            {
                questNameText.text = "Disappeared fertilizer ";
                questText.text = "Help trader to find the fertillizer.";
                mission.SetInteger("QuestType", 3);
                infoImage.sprite = catalogNum;
                infoImage.gameObject.GetComponent<Animator>().SetBool("Info", true);
                questDone = true;
            }
            else if (quest == 4)
            {
                instance.questNameText.text = "Disappeared fertilizer ";
                instance.questText.text = "Give trader the fertillizer.";
                mission.SetInteger("QuestType", 3);
                infoImage.sprite = catalogNum;
                infoImage.gameObject.GetComponent<Animator>().SetBool("Info", true);
                questDone = true;
            }
        }

        if (quest > 1)
        {
            infoImage.sprite = catalogNum; 
            infoImage.gameObject.GetComponent<Animator>().SetBool("save", true);
            GameObject HiddentNPC = GameObject.Find("Butterfly Enthusiast_");

            SkinnedMeshRenderer[] HiddenRend = HiddentNPC.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer h in HiddenRend)
                h.enabled = true;

            //HiddentNPC.GetComponent<MeshRenderer>().enabled = true;
            HiddentNPC.GetComponent<CharacterController>().enabled = true;
            HiddentNPC.GetComponent<NpcDialogue>().enabled = true;

            HiddentNPC = GameObject.Find("Shop NPC");

            HiddenRend = HiddentNPC.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer h in HiddenRend)
                h.enabled = true;

            //HiddentNPC.GetComponent<MeshRenderer>().enabled = true;
            HiddentNPC.GetComponent<BoxCollider>().enabled = true;
            HiddentNPC.GetComponent<NpcDialogue>().enabled = true;
        }
    }

    private void Update()
    {
        if (haveQuest)
            PlayerPrefs.SetInt("haveQuest", 1);
        else
            PlayerPrefs.SetInt("haveQuest", 0);

        if (questDone)
            PlayerPrefs.SetInt("questDone", 1);
        else
            PlayerPrefs.SetInt("questDone", 0);
    }

    public void FinishQuest()
    {
        haveQuest = false;
        questDone = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("Scene1").buildIndex && quest == 1)
        {
            quest = 2;
            PlayerPrefs.SetInt("quest", quest);
            mission.SetInteger("QuestType", 2);
            infoImage.sprite = catalogNum;
            infoImage.gameObject.GetComponent<Animator>().SetBool("Info", true);
            questDone = true;
        }
        if (level == SceneManager.GetSceneByName("Scene2").buildIndex && quest > 1)
        {
            Destroy(GameObject.Find("Butterfly Enthusiast"));
        }
        if (level == SceneManager.GetSceneByName("Scene1").buildIndex && quest > 1)
        {
            GameObject HiddentNPC = GameObject.Find("Butterfly Enthusiast_");
            
            SkinnedMeshRenderer[] HiddenRend = HiddentNPC.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer h in HiddenRend)
                h.enabled = true;

            //HiddentNPC.GetComponent<MeshRenderer>().enabled = true;
            HiddentNPC.GetComponent<CharacterController>().enabled = true;
            HiddentNPC.GetComponent<NpcDialogue>().enabled = true;
            
            HiddentNPC = GameObject.Find("Shop NPC");

            HiddenRend = HiddentNPC.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer h in HiddenRend)
                h.enabled = true;

            //HiddentNPC.GetComponent<MeshRenderer>().enabled = true;
            HiddentNPC.GetComponent<BoxCollider>().enabled = true;
            HiddentNPC.GetComponent<NpcDialogue>().enabled = true;

        }
    }
}
