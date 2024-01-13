using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject levelManager;


    // Start is called before the first frame update
    void Awake()
    {
        if(LevelManager.instance == null)
            Instantiate(levelManager, Vector3.zero, Quaternion.identity);
    }
}
