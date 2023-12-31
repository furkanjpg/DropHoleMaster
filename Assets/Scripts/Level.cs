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
        //PlayerPrefs.DeleteAll();
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
        HoleMovement.movemnet = false;
    }

    public void LoadNextLevel()
    {
        Invoke("NextLevel", 1.0f);
    }

    void NextLevel()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene >= PlayerPrefs.GetInt("Levels"))
        {
            PlayerPrefs.SetInt("Levels", activeScene + 1);
            SceneManager.LoadScene(activeScene + 1);
        }
        else SceneManager.LoadScene(activeScene + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
}
