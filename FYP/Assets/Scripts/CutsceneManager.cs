using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    public Camera cutsceneCam;

    private Camera mainCamera;
    
    Vector3 endPosition;

    Vector3 endRotation;

    private bool playCutscene = false;
    public bool original = false;

    void Start(){
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();        
        DontDestroyOnLoad(gameObject);   
    }
    private void Update()
    {
        if (playCutscene == true)
        {
            cutsceneCam.transform.position = new Vector3(
                    /*(cutsceneCam.transform.position.x + endPosition.x) / 2,
                    (cutsceneCam.transform.position.y + endPosition.y) / 2,
                    (cutsceneCam.transform.position.z + endPosition.z) / 2*/
                    endPosition.x, endPosition.y, endPosition.z);

            cutsceneCam.transform.rotation = Quaternion.Euler((cutsceneCam.transform.rotation.eulerAngles + 
                endRotation) / 2);
        }
    }

    public void Cutscene(Vector3 startPos, Vector3 endPos, Vector3 startRotate, Vector3 endRotate)
    {
        cutsceneCam.gameObject.SetActive(true);
        playCutscene = true;
        cutsceneCam.transform.position = startPos;

        cutsceneCam.transform.rotation = Quaternion.identity;
        cutsceneCam.transform.Rotate(startRotate);

        endPosition = endPos;
        endRotation = endRotate;
    }

    public void StopCutscene()
    {
        StartCoroutine(BackToMainCam());
    }

    IEnumerator BackToMainCam(){
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        Cutscene(cutsceneCam.transform.position, mainCamera.transform.position,
                cutsceneCam.transform.eulerAngles, mainCamera.transform.eulerAngles);

        yield return new WaitForSeconds(0.2f);
        cutsceneCam.gameObject.SetActive(false);
        playCutscene = false;
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("WinScreen").buildIndex || level == 0)
            Destroy(gameObject);
        else
        {
            original = true;
        }
    }

}
