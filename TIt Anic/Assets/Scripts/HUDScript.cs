using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public GameObject player;

    private InteractingManager interactScript;
    private List<string> inventory;
    private Dictionary<string, Sprite> inventorySprites;
    
    private int inventorySize;

    // Start is called before the first frame update
    void Start()
    {
        interactScript = player.GetComponent<InteractingManager>();
        inventory = interactScript.inventory;
        inventorySprites = interactScript.inventorySprites;

        inventorySize = 0;
    }

    // Update is called once per frame
    void Update()
    {
        inventorySprites = interactScript.inventorySprites;
        Transform inventoryPanel = transform.Find("Inventory");
        if (inventorySprites.Count > inventorySize)
        {
            //find the panels of the inventory
            foreach(Transform slot in inventoryPanel)
            {
                //get the image panel
                Image image = slot.GetChild(0).GetComponent<Image>();

                //an empty slot is found
                if (!image.enabled)
                {
                    image.enabled = true;
                    image.sprite = inventorySprites[inventory[inventory.Count - 1]];

                    break;
                }
            }
        }
        inventorySize = inventorySprites.Count;

        //check that removes tiems if they are removed from the inventory
        for(int i = 0; i < inventory.Count; i++)
        {
            Image image = inventoryPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            image.sprite = inventorySprites[inventory[i]];
        }
        for(int i = inventory.Count; i < inventoryPanel.childCount; i++)
        {
            Image image = inventoryPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            image.enabled = false;
        }
    }
}
