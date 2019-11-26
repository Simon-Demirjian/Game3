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
        if(inventorySprites.Count > inventorySize)
        {
            //find the panels of the inventory
            Transform inventoryPanel = transform.Find("Inventory");
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
    }
}
