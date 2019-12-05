using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open;
    public string key;
    public bool isLocked;

    private bool previousState;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        previousState = open;
    }

    // Update is called once per frame
    void Update()
    {
        if (open && !previousState)
        {
            this.transform.RotateAround(this.transform.localPosition - new Vector3(this.transform.localScale.x / 2, 0, 0), new Vector3(0, 1, 0), 90);
            //this.transform.Rotate(transform.position, 90);
        }
        else if (!open && previousState)
        {
            //this.transform.Rotate(transform.position, -90);
            this.transform.RotateAround(this.transform.localPosition - new Vector3(0, 0, this.transform.localScale.x / -2), new Vector3(0, 1, 0), -90);
        }

        previousState = open;
    }
}
