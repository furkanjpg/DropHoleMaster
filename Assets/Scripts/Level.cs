using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static Level Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] ParticleSystem winFx;

    public int objectsInScene;
    public int totalObjects;

    [SerializeField] Transform objectsParent;

    void Start()
    {
        CountObjects();
    }

    void CountObjects()
    {
        totalObjects = objectsParent.childCount;
        objectsInScene = totalObjects;
    }

    public void PlayWinFx()
    {
        winFx.Play();
    }

    public void LoadNextLevel()
    {
        Invoke("NextLevel", 2.0f);
    }

    void NextLevel()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene >= PlayerPrefs.GetInt("Levels"))
        {
            PlayerPrefs.SetInt("Levels", activeScene + 1);
            SceneManager.LoadScene(activeScene + 1);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
