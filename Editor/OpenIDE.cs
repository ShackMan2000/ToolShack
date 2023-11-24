using Unity.CodeEditor;
using UnityEditor;

public static class OpenIdeOnStart
{
    private const string STATE_KEY = "LN.IDE_OPENED";
 
    private static bool ProjectAlreadyOpenedState
    {
        get => SessionState.GetBool(STATE_KEY, false);
        set => SessionState.SetBool(STATE_KEY, value);
    }
       
    [InitializeOnLoadMethod]
    private static void OpenIdeOnStartMethod()
    {
        if (ProjectAlreadyOpenedState) return;
        CodeEditor.Editor.CurrentCodeEditor.OpenProject();
        ProjectAlreadyOpenedState = true;
    }
}
