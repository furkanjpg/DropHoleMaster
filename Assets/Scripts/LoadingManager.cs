using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Levels", 0));
    }
}
