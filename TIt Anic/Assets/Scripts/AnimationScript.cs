using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator dressAnim;
    public bool open;
    private bool previousState;


    // Start is called before the first frame update
    void Start()
    {
        dressAnim = GetComponent<Animator>();
        open = true;
        //previousState = open;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            dressAnim.Play("DresserOpen");

            //this.transform.RotateAround(this.transform.localPosition - new Vector3(this.transform.localScale.x / 2, 0, 0), new Vector3(0, 1, 0), 90);
            //this.transform.Rotate(transform.position, 90);
        }
        

        //previousState = open;


    }
}
