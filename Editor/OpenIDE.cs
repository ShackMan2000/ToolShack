using Unity.CodeEditor;
using UnityEditor;
using UnityEngine;

public static class OpenIdeOnStart
{
    private const string STATE_KEY = "LN.IDE_OPENED";
    static bool ideOpened = false;

    private static bool ProjectAlreadyOpenedState
    {
        get => SessionState.GetBool(STATE_KEY, false);
        set => SessionState.SetBool(STATE_KEY, value);
    }

    [InitializeOnLoadMethod]
    private static void OpenIdeOnStartMethod()
    {
        // Debug.Log("OpenIdeOnStartMethod " + ProjectAlreadyOpenedState + "udeOpened " + ideOpened);

        if (ProjectAlreadyOpenedState || ideOpened) return;

        CodeEditor.Editor.CurrentCodeEditor.OpenProject();
        ideOpened = true;
        ProjectAlreadyOpenedState = true;
    }
}