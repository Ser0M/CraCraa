using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // This function is called when the button is clicked
    public void ExitGame()
    {
        // Quit the application if it’s a standalone build
        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        // If we're in the editor, stop play mode
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}