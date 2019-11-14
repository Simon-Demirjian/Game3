using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    public bool open;
    public bool contains;
    public bool containsEvidence;
    public string item;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        contains = false;
        containsEvidence = false;
        item = "test item";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
