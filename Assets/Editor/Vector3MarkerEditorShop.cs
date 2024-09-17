using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShopManager))]
public class Vector3MarkerEditorShop : Editor
{
    private SerializedProperty vector3List;

    private void OnEnable()
    {
        vector3List = serializedObject.FindProperty("vector3List");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        ShopManager shopManager = (ShopManager)target;

        if (shopManager.vector3List != null)
        {
            for (int i = 0; i < shopManager.vector3List.Count; i++)
            {
                Handles.color = Color.yellow; // Change color if needed

                // Set matrix to identity to draw in world space
                Handles.matrix = Matrix4x4.identity;
                Handles.DrawWireDisc(shopManager.vector3List[i], Vector3.back, 0.5f);

                // Reset matrix to allow other handles to draw in world space
                Handles.matrix = Matrix4x4.identity;

                Handles.Label(shopManager.vector3List[i], "Marker " + i);
                shopManager.vector3List[i] = Handles.PositionHandle(shopManager.vector3List[i], Quaternion.identity);
            }
        }
    }
}