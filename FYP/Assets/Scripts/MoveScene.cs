using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    private Camera mainCamera;

    public Vector3 spawnPoint;
    private PlayerController playerController;
    public Animator transitionAnim;
    public CutsceneManager cutsceneManager;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();        
        cutsceneManager = GameObject.FindObjectOfType<CutsceneManager>();
        if(GameObject.FindGameObjectWithTag("TransitionPanel") != null)
            transitionAnim = GameObject.FindGameObjectWithTag("TransitionPanel").GetComponent<Animator>();
        if (transitionAnim != null)
            transitionAnim.Play("FadeIn");
        if (GameObject.Find("GameStartCutscene(Clone)") == null)
            StartCoroutine(LoadScene());
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("TransitionPanel") != null)
            transitionAnim = GameObject.FindGameObjectWithTag("TransitionPanel").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            StartCoroutine(SceneMove());
        }
    }

    IEnumerator SceneMove()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.canMove = -1;
        TimeController.instance.timeMove = -2;
        if (transitionAnim != null)
            transitionAnim.Play("FadeOut");
        yield return new WaitForSeconds(1);
        LevelManager.instance.spawnPoint = spawnPoint;
        SceneManager.LoadScene(sceneName);
    }
    
    IEnumerator LoadScene(){
        if (GameObject.Find("GameStartCutscene(Clone)") == null)
        {
            cutsceneManager.Cutscene(new Vector3(-105.2f,28.5f,-178.6f),
                mainCamera.transform.position,new Vector3(0,120,0),mainCamera.transform.eulerAngles);
        }
        else
        {
            cutsceneManager.StopCutscene();           
        }
        yield return new WaitForSeconds(0.8f);
        cutsceneManager.StopCutscene();
    }
    
}
