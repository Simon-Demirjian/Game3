using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPC : MonoBehaviour
{
    private GameObject player;
    private InteractingManager interactionManager;

    public string npcName;
    public string message;

    public bool condition;
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
            for (int i = 0; i < numberOfItems; i++)
            {
                if(!interactionManager.evidenceCollected.Contains(requiredItems[i]))
                {
                    return message;
                }
            }

            return secondMessage;
        }
    }
}

// Editor script so that options don't always show up
[CustomEditor(typeof(NPC))]
public class NPCEditor : Editor
{
    override public void OnInspectorGUI()
    {
        NPC script = target as NPC;

        script.npcName = EditorGUILayout.TextField("Name", script.npcName);
        script.message = EditorGUILayout.TextField("Message", script.message);
        script.condition = EditorGUILayout.Toggle("Conditional Message", script.condition);

        if (script.condition)
        {
            EditorGUI.indentLevel++;
            script.secondMessage = EditorGUILayout.TextField("Second Message", script.secondMessage);

            script.numberOfItems = EditorGUILayout.IntSlider("Number of Items", script.numberOfItems, 0, 5);
            string[] temp = script.requiredItems;
            script.requiredItems = new string[script.numberOfItems];
            for (int i = 0; i < temp.Length; i++)
            {
                script.requiredItems[i] = temp[i];
            }

            EditorGUI.indentLevel++;
            for (int i = 1; i <= script.numberOfItems; i++)
            {
                script.requiredItems[i-1] = EditorGUILayout.TextField("Object Name", script.requiredItems[i-1]);
            }
            EditorGUI.indentLevel--;

            EditorGUI.indentLevel--;
        }
    }
}
