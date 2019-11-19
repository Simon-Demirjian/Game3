using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingManager : MonoBehaviour
{
    public GameObject player;
    public List<string> evidenceCollected;
    public List<string> inventory;

    private GameObject[] containers;
    private List<GameObject> containerList = new List<GameObject>();
    private GameObject[] doors;
    private List<GameObject> doorList = new List<GameObject>();
    private bool containerNear;
    private bool doorNear;
    private GameObject closest;

    private float distance;
    


    // Start is called before the first frame update
    void Start()
    {
        distance = 1.0f;

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
        DoorCheck();
        InteractableCheck();
    }

    void DoorCheck()
    {
        int counter = 0;
        foreach(GameObject door in doorList)
        {
            if (player.transform.position.x > door.transform.position.x - distance &&
                player.transform.position.x < door.transform.position.x + distance &&
                player.transform.position.z > door.transform.position.z - distance &&
                player.transform.position.z < door.transform.position.z + distance)
            {
                doorNear = true;

                //set closest
                if(closest == null)
                {
                    closest = door;
                }
                else if(closest != null || closest != door)
                {
                    //if this item is closer
                    if(Vector3.Distance(player.transform.position, door.transform.position) < Vector3.Distance(player.transform.position, closest.transform.position))
                    {
                        closest = door;
                    }
                }
            }
            else
            {
                counter++;
            }
        }
        //if none of the items were near, don't bring up the option
        if (counter == doorList.Count)
        {
            doorNear = false;
        }

        //if the player is near an item, let them pick it up
        if (doorNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //add code for door being in open or closed state
                DoorScript doorScript = closest.GetComponent<DoorScript>();
                //change boolean state from open to closed or vice versa
                if (doorScript.open)
                {
                    doorScript.open = false;
                }
                else if (!doorScript.open)
                {
                    doorScript.open = true;
                }
            }
        }
    }

    void InteractableCheck()
    {
        int counter = 0;
        foreach (GameObject container in containerList)
        {
            if (player.transform.position.x > container.transform.position.x - distance &&
                player.transform.position.x < container.transform.position.x + distance &&
                player.transform.position.z > container.transform.position.z - distance &&
                player.transform.position.z < container.transform.position.z + distance)
            {
                containerNear  = true;

                //set closest
                if (closest == null)
                {
                    closest = container;
                }
                else if (closest != null || closest != container)
                {
                    //if this item is closer
                    if (Vector3.Distance(player.transform.position, container.transform.position) < Vector3.Distance(player.transform.position, closest.transform.position))
                    {
                        closest = container;
                    }
                }
            }
            else
            {
                counter++;
            }
        }
        //if none of the items were near, don't bring up the option
        if (counter == containerList.Count)
        {
            containerNear = false;
        }

        //if the player is near an item, let them pick it up
        if (containerNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ContainerScript containerScript = closest.GetComponent<ContainerScript>();
                //change the container's state to being open
                //change boolean state from open to closed or vice versa
                if (containerScript.open)
                {
                    containerScript.open = false;
                }
                else if (!containerScript.open)
                {
                    containerScript.open = true;
                }

                //if the container held an item, add it to the inventory
                if (containerScript.containsEvidence)
                {
                    evidenceCollected.Add(containerScript.item);
                    inventory.Add(containerScript.item);

                    containerScript.item = "Empty";
                    containerScript.contains = false;
                    containerScript.containsEvidence = false;
                }
                else if (containerScript.contains)
                {
                    inventory.Add(containerScript.item);

                    containerScript.item = "Empty";
                    containerScript.contains = false;
                }
            }
        }
    }

    void OnGUI()
    {
        //prompt to interact with item
        if (containerNear)
        {
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 250, 50), "Press E to interact");
        }
        else if (doorNear)
        {
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 250, 50), "Press E to open/close");
        }
    }
}
