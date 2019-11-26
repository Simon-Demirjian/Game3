using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    public bool open;
    public bool contains;
    public bool containsEvidence;
    public string item;
    public string requiredItem;
    public bool playerHasRequiredItem;
    public Sprite image;

    // Start is called before the first frame update
    void Start()
    {
        if(requiredItem == "")
            playerHasRequiredItem = true;
        else
            playerHasRequiredItem = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
