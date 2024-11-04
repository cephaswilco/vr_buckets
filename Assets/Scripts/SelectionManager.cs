using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    // Scene name to load on selection
    [SerializeField]
    private string sceneToLoad;

    // Call this method to highlight the button
    public void Highlighted()
    {
        // Implement your highlight logic here
        Debug.Log("Button Highlighted");
    }

    // Call this method to unhighlight the button
    public void Unhighlighted()
    {
        // Implement your unhighlight logic here
        Debug.Log("Button Unhighlighted");
    }

    // Call this method to select the button
    public void Selected()
    {
        // Implement your selection logic here
        Debug.Log("Button Selected");
        LoadScene();
    }

    // Load the specified scene
    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not set or is empty.");
        }
    }
}