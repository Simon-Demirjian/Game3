using UnityEditor;



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
            script.orConditional = EditorGUILayout.Toggle("Or Instead of And", script.orConditional);
            script.secondMessage = EditorGUILayout.TextField("Second Message", script.secondMessage);

            script.numberOfItems = EditorGUILayout.IntSlider("Number of Items", script.numberOfItems, 0, 5);
            string[] temp = script.requiredItems;
            script.requiredItems = new string[script.numberOfItems];
            for (int i = 0; i < temp.Length && i < script.numberOfItems; i++)
            {
                script.requiredItems[i] = temp[i];
            }

            EditorGUI.indentLevel++;
            for (int i = 1; i <= script.numberOfItems; i++)
            {
                script.requiredItems[i - 1] = EditorGUILayout.TextField("Object Name", script.requiredItems[i - 1]);
            }
            EditorGUI.indentLevel--;

            EditorGUI.indentLevel--;
        }
    }
}
