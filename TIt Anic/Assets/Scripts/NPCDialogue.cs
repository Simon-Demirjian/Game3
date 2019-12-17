using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    private GameObject player;
    private List<GameObject> npcList = new List<GameObject>();
    private GameObject[] npcs;

    private bool npcNear = false;
    private bool lookingAtNPC = false;

    private float distance;
    private GameObject closest;

    private int counter = 0;
    private bool guiOn = false;
    private int npcNum = 0;

    private static Canvas interactionTextHolder;
    private static Text interactionText;
    private static bool called = false;

    private static Canvas dialogueHolder;
    private static Text dialogueText;
    private static Text nameText;

    public void NewScene()
    {
        npcList = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        distance = 2.0f;
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            npcList.Add(npc);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = 2.0f;
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            npcList.Add(npc);
        }

        dialogueHolder = GameObject.FindGameObjectWithTag("Dialogue Holder").GetComponent<Canvas>();
        dialogueText = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponent<Text>();
        nameText = GameObject.FindGameObjectWithTag("Name Text").GetComponent<Text>();
        interactionTextHolder = GameObject.FindGameObjectWithTag("Interaction Text Holder").GetComponent<Canvas>();
        interactionText = GameObject.FindGameObjectWithTag("Interaction Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        // Lock the player's movement if they are talking 
        // End the locking when they press E again
        if (guiOn)
        {
            player.GetComponent<MovePlayer>().canMove = false;
            if (Input.GetKeyDown(KeyCode.E))
            {
                guiOn = false;
                player.GetComponent<MovePlayer>().canMove = true;
            }
        }
        else
        {

            //if player is near a npc, allow them to converse
            foreach (GameObject npc in npcList)
            {
                //player is within a certain radius of npc
                if (player.transform.position.x > npc.transform.position.x - distance &&
                    player.transform.position.x < npc.transform.position.x + distance &&
                    player.transform.position.z > npc.transform.position.z - distance &&
                    player.transform.position.z < npc.transform.position.z + distance)
                {
                    npcNear = true;

                    //set closest
                    if (closest == null)
                    {
                        closest = npc;
                    }
                    else if (closest != null || closest != npc)
                    {
                        //if this npc is closer
                        if (Vector3.Distance(player.transform.position, npc.transform.position) < Vector3.Distance(player.transform.position, closest.transform.position))
                        {
                            closest = npc;
                        }
                    }

                    npcNum = counter;
                    Debug.Log(npcNum);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        guiOn = true;
                        // If this is Member2, move him
                        // band-aid code that moves member 2 away from a door :^)
                        if (closest.GetComponent<NPC>().npcName == "Member2" && player.GetComponent<InteractingManager>().inventory.Contains("Cocktail"))
                        {
                            closest.transform.Translate(new Vector3(2.0f, 0.0f, 0.0f));
                        }
                    }
                }
                else
                {
                    counter++;
                }
            }
            //if none of the npcs were near, don't bring up the option
            if (counter == npcList.Count)
            {
                npcNear = false;
                guiOn = false;
            }

            counter = 0;
        }
    }

    void OnGUI()
    {
        GUI.contentColor = Color.red;
        if(guiOn)
        {
            // Turns on the dialogue boxes
            dialogueHolder.enabled = true;
            interactionTextHolder.enabled = false;
            nameText.text = npcList[npcNum].GetComponent<NPC>().npcName;
            dialogueText.text = npcList[npcNum].GetComponent<NPC>().Call();
        }
        else
        {
            // Switches off dialogue boxes
            dialogueHolder.enabled = false;
            if (npcNear && !guiOn) //if the player is near a npc, tell them they can converse
            {
                InteractionText("Press E to talk");
            }
            else if(!called)
            {
                interactionTextHolder.enabled = false;
            }
            called = false;
        }
    }

    public static void InteractionText(string text)
    {
        dialogueHolder.enabled = false;
        interactionTextHolder.enabled = true;
        interactionText.text = text;
        called = true;
    }
}
