using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    public GameObject player;
    private List<GameObject> npcList = new List<GameObject>();
    private GameObject[] npcs;

    private bool npcNear = false;
    private bool lookingAtNPC = false;

    private float distance;
    private GameObject closest;

    private int counter = 0;
    private bool guiOn = false;
    private int npcNum = 0;

    private GameObject dialogueHolder;
    private Text dialogueText;
    private Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        distance = 2.0f;
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            npcList.Add(npc);
        }

        dialogueHolder = GameObject.FindGameObjectWithTag("Dialogue Holder");
        dialogueText = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponent<Text>();
        nameText = GameObject.FindGameObjectWithTag("Name Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
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

    void OnGUI()
    {
        GUI.contentColor = Color.red;
        if(guiOn)
        {
            dialogueHolder.SetActive(true);
            nameText.text = "NPC";
            if (npcNum == 0) //first npc
            {
                dialogueText.text = "Iceberg was just here, you really missed out man!";
            }
            else if (npcNum == 1) //second npc
            {
                dialogueText.text = "I saw Iceberg hanging out near the end of the ship earlier today. What a *cool* guy!";
            }
            else if (npcNum == 2) //third npc
            {
                if (player.GetComponent<InteractingManager>().evidenceCollected.Count >= 3)
                {
                    dialogueText.text = "You win!";
                }
                else
                {
                    dialogueText.text = "I'm gonna need three strong pieces of evidence to agree with your case.";
                }
            }
        }
        else
        {
            dialogueHolder.SetActive(false);
            if (npcNear && !guiOn) //if the player is near a npc, tell them they can converse
            {
                GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 250, 50), "Press E to talk");
            }
        }
    }
}
