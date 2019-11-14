using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject player;
    private List<GameObject> npcList = new List<GameObject>();
    private GameObject[] npcs;

    private bool npcNear = false;
    private bool lookingAtNPC = false;

    private float distance;
    private GameObject closest;

    // Start is called before the first frame update
    void Start()
    {
        distance = 2.0f;
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcs)
        {
            npcList.Add(npc);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
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

                if (Input.GetKeyDown(KeyCode.E))
                {

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
        }
    }

    void OnGUI()
    {
        if (npcNear) //if the player is near a npc, tell them they can converse
        {
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 250, 50), "Press E to talk");
        }
    }
}
