using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    public static Level Instance;

    void Awake ()
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

    // Update is called once per frame
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
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }

}
