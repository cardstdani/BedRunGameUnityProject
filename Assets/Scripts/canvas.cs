using UnityEngine.SceneManagement;
using UnityEngine;

public class canvas : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void menu()
    {
        SceneManager.LoadSceneAsync("Principal");
    }
}
