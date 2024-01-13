using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    GameObject player, cameraFollow;

    float speed = 120;
    public int RotateFixedY;
    int left, right;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraFollow = GameObject.Find("CameraFollow");


        transform.position = new Vector3(cameraFollow.transform.position.x,
            cameraFollow.transform.position.y + 4, cameraFollow.transform.position.z - 5);
        gameObject.transform.parent = cameraFollow.transform;


        cameraFollow.transform.Rotate(0, RotateFixedY, 0);
    }

    private void Update()
    {   
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            foreach (GameObject players in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (players.transform.position.y > -1)
                {
                    Destroy(players);
                }
            }
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        cameraFollow.transform.position = player.transform.position;

        if (Input.GetKey(KeyCode.E))
        {
            right = 1;
        }
        else
        {
            right = 0;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            left = 1;
        }
        else
        {
            left = 0;
        }

        if(player.GetComponent<PlayerController>().canMove > 0)
            cameraFollow.transform.Rotate(0, speed * (right - left) * Time.deltaTime, 0);
    }
}
