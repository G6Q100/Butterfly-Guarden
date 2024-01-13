using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleepsystem : MonoBehaviour
{
    public GameObject SleepPanel;

    bool sleeping = false;

    //void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        SleepPanel.SetActive(false);
    //        //yield return new WaitForSeconds(1);
    //        //SleepPanel.SetActive(true);
    //    }
    //}

    

    //public void Start()
    //{
    //    //OliveOil.SetActive(false);
    //    SleepPanel.SetActive(false);

    //    //ShowCharacters();

    //    StartCoroutine(WaitBeforeShow());
    //}

    private void OnMouseOver()
    {
        if (GameObject.Find("InfoText") != null)
        {
            GameObject.Find("InfoText").transform.position = Input.mousePosition + Vector3.up * 50;
            if(TimeController.instance.time < 228)
            {
                GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
                GameObject.Find("RightClick").GetComponent<Image>().enabled = true;
                GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                GameObject.Find("InfoText").GetComponent<Text>().text = "Bed \n Sleep 5 hours: ";
            }
            else
            {
                GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
                GameObject.Find("RightClick").GetComponent<Image>().enabled = true;
                GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                GameObject.Find("InfoText").GetComponent<Text>().text = "Bed \n Sleep to next day: ";
            }
        }

        if (Input.GetMouseButtonDown(1) && !sleeping)
        {
            SleepPanel.SetActive(true);
            if (TimeController.instance.time < 228)
                StartCoroutine(WaitBeforeShow(60));
            else
                StartCoroutine(WaitBeforeShow(120));
        }
        //wait a couple of seconds
    }

    private void OnMouseExit()
    {
        if (GameObject.Find("InfoText") != null)
        {
            GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
            GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoText").GetComponent<Text>().text = "";
        }
    }

    private IEnumerator WaitBeforeShow(int times)
    {
        sleeping = true;
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.canMove = -2;
        yield return new WaitForSeconds(2);
        sleeping = false;


        for (int i = 0; i < DirtManager.instance.dirtGroup.Length; i++)
        {
            if (TimeController.instance.timeMove > 0 &&
                DirtManager.instance.dirtGroup[i].state != "normal" &&
                DirtManager.instance.dirtGroup[i].state != "hovered" &&
                DirtManager.instance.dirtGroup[i].state != "dead")
            {
                DirtManager.instance.dirtGroup[i].growthTime -= 60;
                DirtManager.instance.dirtGroup[i].dryTime -= 60;
            }
        }
        if(times > 100)
        {
            ItemSystem.instance.fertilizerNum = 10;
            ItemSystem.instance.fertilizerNumText.text = "We still have " + ItemSystem.instance.fertilizerNum + " of them to sell today!";
            TimeController.instance.time = 83;
            TimeController.instance.timeMode = 4;

            if (TimeController.instance.day < 7)
                TimeController.instance.day++;
            else
            {
                TimeController.instance.day = 1;
                if (TimeController.instance.season < 4)
                    TimeController.instance.season++;
                else
                {
                    TimeController.instance.season = 1;
                    TimeController.instance.year++;
                }
            }

            switch (TimeController.instance.season)
            {
                case 1:
                    TimeController.instance.seasonText = ", Spring";
                    break;
                case 2:
                    TimeController.instance.seasonText = ", Summer";
                    break;
                case 3:
                    TimeController.instance.seasonText = ", Fall";
                    break;
                case 4:
                    TimeController.instance.seasonText = ", Winter";
                    break;
                default:
                    break;
            }
        }
        else
            TimeController.instance.time += times;
        SleepPanel.SetActive(false);
    }
}



    


