using UnityEditor;
using UnityEngine;

public static class CopyGameObjectPath
{
    [MenuItem("Congroo/Copy GameObject Path &O")]
    public static void CopySelectedGameObjectPath()
    {
        GameObject selectedObject = Selection.activeGameObject;
        if (selectedObject != null)
        {
            string path = GetFullPath(selectedObject);
            GUIUtility.systemCopyBuffer = path;
            Debug.Log("GameObject path copied to clipboard: " + path);
        }
        else
        {
            Debug.LogWarning("No object is currently selected.");
        }
    }

    private static string GetFullPath(GameObject go)
    {
        Transform t = go.transform;
        string path = t.name;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
}