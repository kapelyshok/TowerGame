using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasButtons : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}