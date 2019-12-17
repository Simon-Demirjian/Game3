using UnityEngine;

public class NPC : MonoBehaviour
{
    private GameObject player;
    private InteractingManager interactionManager;

    public string npcName;
    public string message;

    public bool condition;
    public bool orConditional;
    public string secondMessage;
    public int numberOfItems;
    public string[] requiredItems = new string[0];

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactionManager = player.GetComponent<InteractingManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Gets the objects line to display
    /// </summary>
    /// <returns>String to be displayed</returns>
    public string Call()
    {

        if(!condition)
        {
            return message;
        }

        else
        {
            if(!orConditional)
            {
                for (int i = 0; i < numberOfItems; i++)
                {
                    if (!interactionManager.evidenceCollected.Contains(requiredItems[i]))
                    {
                        return message;
                    }
                }
                return secondMessage;
            }
            else
            {
                for (int i = 0; i < numberOfItems; i++)
                {
                    if (interactionManager.evidenceCollected.Contains(requiredItems[i]))
                    {
                        return secondMessage;
                    }
                }
                return message;
            }
        }
    }
}