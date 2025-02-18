﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingManager : MonoBehaviour
{
    public GameObject player;
    public List<string> evidenceCollected;
    public List<string> inventory;
    public Dictionary<string, Sprite> inventorySprites;

    private GameObject[] containers;
    private List<GameObject> containerList = new List<GameObject>();
    private GameObject[] doors;
    private List<GameObject> doorList = new List<GameObject>();
    private GameObject[] lockedDoors;
    private List<GameObject> lockedDoorList = new List<GameObject>();
   

    private bool containerNear;
    private bool doorNear;
    private GameObject closest;

    private float distance;

    private bool collectedEvidence = false;
    private bool collectedItem = false;
    private bool visited = false;
    private bool doorLocked;

    public Animator dressAnim;

    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<NPCDialogue>().NewScene();
            GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<NPCDialogue>().NewScene();
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<InteractingManager>().NewScene();
            GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<InteractingManager>().NewScene();

            GameObject.FindGameObjectsWithTag("Player")[0].transform.position = transform.position;
            GameObject.FindGameObjectsWithTag("Player")[1].transform.position = transform.position;

            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<MovePlayer>().NewScene(GetComponent<MovePlayer>());
            GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<MovePlayer>().NewScene(GetComponent<MovePlayer>());

            GameObject.Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void NewScene()
    {
        containerList = new List<GameObject>();
        doorList = new List<GameObject>();
        lockedDoorList = new List<GameObject>();


        //generate container list based on tag
        containers = GameObject.FindGameObjectsWithTag("Container");
        foreach (GameObject container in containers)
        {
            containerList.Add(container);
        }

        //generate door list based on tag
        doors = GameObject.FindGameObjectsWithTag("UnlockedDoor");
        foreach (GameObject door in doors)
        {
            doorList.Add(door);
        }

        //generate locked door list based on tag
        lockedDoors = GameObject.FindGameObjectsWithTag("LockedDoor");
        foreach (GameObject lockedDoor in lockedDoors)
        {
            lockedDoorList.Add(lockedDoor);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventorySprites = new Dictionary<string, Sprite>();
        distance = 1.0f;

        //generate container list based on tag
        containers = GameObject.FindGameObjectsWithTag("Container");
        foreach(GameObject container in containers)
        {
            containerList.Add(container);
        }

        //generate door list based on tag
        doors = GameObject.FindGameObjectsWithTag("UnlockedDoor");
        foreach(GameObject door in doors)
        {
            doorList.Add(door);
        }

        //generate locked door list based on tag
        lockedDoors = GameObject.FindGameObjectsWithTag("LockedDoor");
        foreach (GameObject lockedDoor in lockedDoors)
        {
            lockedDoorList.Add(lockedDoor);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        doorNear = false;
        DoorCheck();
        LockedDoorCheck();
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

    void LockedDoorCheck()
    {
        int counter = 0;
        foreach (GameObject lockedDoor in lockedDoorList)
        {
            if (player.transform.position.x > lockedDoor.transform.position.x - distance &&
                player.transform.position.x < lockedDoor.transform.position.x + distance &&
                player.transform.position.z > lockedDoor.transform.position.z - distance &&
                player.transform.position.z < lockedDoor.transform.position.z + distance)
            {
                doorNear = true;

                //set closest
                if (closest == null)
                {
                    closest = lockedDoor;
                }
                else if (closest != null || closest != lockedDoor)
                {
                    //if this item is closer
                    if (Vector3.Distance(player.transform.position, lockedDoor.transform.position) < Vector3.Distance(player.transform.position, closest.transform.position))
                    {
                        closest = lockedDoor;
                    }
                }
            }
            else
            {
                counter++;
            }
        }

        //if the player is near an item, let them pick it up
        if (doorNear)
        {
            DoorScript doorScript = closest.GetComponent<DoorScript>();
            doorLocked = doorScript.isLocked;
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (string i in inventory)
                {
                    if (i == doorScript.key)
                    {
                        doorScript.isLocked = false;
                        doorLocked = doorScript.isLocked;
                        //add code for door being in open or closed state
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
            collectedEvidence = false;
            collectedItem = false;
        }

        //if the player is near an item, let them pick it up
        if (containerNear)
        {
            ContainerScript containerScript = closest.GetComponent<ContainerScript>();
            visited = containerScript.visited;

            if (Input.GetKeyDown(KeyCode.E))
            {
                containerScript.visited = true;

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

                if (containerScript.requiredItem.Length == 0 || containerScript.requiredItem[0] == "")
                {
                    if (!containerScript.playerHasRequiredItem && containerScript.requiredItem[0] != "") // if there is a required item for this container, check if the player has it
                    {
                        foreach (string j in inventory)
                        {
                            if (j == containerScript.requiredItem[0])
                            {
                                containerScript.playerHasRequiredItem = true;
                                // Attempting to remove the sprite from the inventory once the required item is used.
                                //inventory.Remove(containerScript.requiredItem);
                                //inventorySprites.Remove(containerScript.requiredItem);
                            }
                        }
                    }
                    if (containerScript.containsEvidence && containerScript.playerHasRequiredItem) //if the container held an item, add it to the inventory
                    {
                        //inventory.Remove(containerScript.requiredItem[0]);
                        //inventorySprites.Remove(containerScript.requiredItem[0]);

                        evidenceCollected.Add(containerScript.item);
                        inventory.Add(containerScript.item);
                        inventorySprites.Add(containerScript.item, containerScript.image);

                        containerScript.item = "Empty";
                        containerScript.contains = false;
                        containerScript.containsEvidence = false;

                        collectedEvidence = true;
                    }
                    else if (containerScript.contains && containerScript.playerHasRequiredItem)
                    {
                        //inventory.Remove(containerScript.requiredItem[0]);
                        //inventorySprites.Remove(containerScript.requiredItem[0]);

                        inventory.Add(containerScript.item);
                        inventorySprites.Add(containerScript.item, containerScript.image);

                        

                        containerScript.item = "Empty";
                        containerScript.contains = false;

                        collectedItem = true;
                    }

                }

                else
                {
                    for (int i = 0; i < containerScript.requiredItem.Length; i++)
                    {
                        if (!containerScript.playerHasRequiredItem && containerScript.requiredItem[i] != "") // if there is a required item for this container, check if the player has it
                        {
                            int check = 0;
                            foreach (string j in inventory)
                            {
                                if (j == containerScript.requiredItem[i])
                                {
                                    if(j == "BrokenVase" || j == "SaucyNovel" || j == "Photo")
                                    {
                                        check++;
                                        if (check == containerScript.requiredItem.Length)
                                        {
                                            containerScript.playerHasRequiredItem = true;

                                        }
                                    }
                                    else
                                    {
                                        containerScript.playerHasRequiredItem = true;

                                    }

                                    // Attempting to remove the sprite from the inventory once the required item is used.
                                    //inventory.Remove(containerScript.requiredItem);
                                    //inventorySprites.Remove(containerScript.requiredItem);
                                }
                            }
                        }
                        if (containerScript.containsEvidence && containerScript.playerHasRequiredItem) //if the container held an item, add it to the inventory
                        {
                            inventory.Remove(containerScript.requiredItem[i]);
                            inventorySprites.Remove(containerScript.requiredItem[i]);

                            evidenceCollected.Add(containerScript.item);
                            inventory.Add(containerScript.item);
                            inventorySprites.Add(containerScript.item, containerScript.image);

                            containerScript.item = "Empty";
                            containerScript.contains = false;
                            containerScript.containsEvidence = false;

                            collectedEvidence = true;
                        }
                        else if (containerScript.contains && containerScript.playerHasRequiredItem)
                        {
                            inventory.Remove(containerScript.requiredItem[i]);
                            inventorySprites.Remove(containerScript.requiredItem[i]);

                            inventory.Add(containerScript.item);
                            inventorySprites.Add(containerScript.item, containerScript.image);

                            containerScript.item = "Empty";
                            containerScript.contains = false;

                            collectedItem = true;
                        }
                    }
                }

                if (inventory.Contains("Photo") && inventory.Contains("Cocktail"))
                {
                    if (inventory.Contains("Money_1") || inventory.Contains("Money_2"))
                    {
                        if (inventory.Contains("Money_1"))
                        {
                            inventory.Remove("Money_1");

                        }

                        else if (inventory.Contains("Money_2"))
                        {
                            inventory.Remove("Money_2");

                        }
                    }


                }
            }
        }
    }

    void OnGUI()
    {
        GUI.contentColor = Color.red;
        //prompt to interact with item
        if (containerNear)
        {
            if(collectedEvidence)
            {
                NPCDialogue.InteractionText("Evidence Collected!");
            }
            else if(collectedItem)
            {
                NPCDialogue.InteractionText("Picked Up Item");
            }
            else if(!visited)
            {
                NPCDialogue.InteractionText("Press E to interact");
            }
            else
            {
                NPCDialogue.InteractionText("Nothing here");
            }
        }
        else if (doorNear)
        {
            if(!doorLocked)
            {
                NPCDialogue.InteractionText("Press E to open/close");
            }
            else
            {
                NPCDialogue.InteractionText("Locked, needs a key");
            }
        }
    }
}
