using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingManager : MonoBehaviour
{
    public GameObject player;
    public List<string> evidenceCollected;

    private GameObject[] containers;
    private List<GameObject> containerList;
    private GameObject[] doors;
    private List<GameObject> doorList;


    // Start is called before the first frame update
    void Start()
    {
        //generate container list based on tag
        containers = GameObject.FindGameObjectsWithTag("Container");
        foreach(GameObject container in containers)
        {
            containerList.Add(container);
        }

        //generate door list based on tag
        doors = GameObject.FindGameObjectsWithTag("Door");
        foreach(GameObject door in doors)
        {
            doorList.Add(door);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
