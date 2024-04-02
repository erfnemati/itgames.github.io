using UnityEditor;


public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject m_serializedObject;
    protected SerializedProperty m_CurrentProperty; 
    protected void DrawProperties(SerializedProperty property,bool drawChildren)
    {
        string  lastPropPath = string.Empty;
        foreach(SerializedProperty p in property)
        {
            if(p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded =  EditorGUILayout.Foldout(p.isExpanded,p.displayName);
                EditorGUILayout.EndHorizontal();

                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel--;

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath))
                    continue;
                lastPropPath = p.propertyPath;
                EditorGUILayout.PropertyField(p,drawChildren);

            }
        }
    }
}
