using UnityEngine;

public class ExitButton : MonoBehaviour
{
  public void ExitGame()
    {
        // Oyun editörde çalışıyorsa
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Oyunu kapat
            Application.Quit();
        #endif
    }
}
