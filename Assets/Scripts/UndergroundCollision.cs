using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
    public static float holescale=0.8f;
    // Start is called before the first frame update
    private void Start()
    {
        holescale = 0.8f;
    }
    void OnTriggerEnter (Collider other)
    {
        if (!Game.isGameover) 
        {
            string tag = other.tag;

            if (tag.Equals ("Object"))
            {
                Level.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();
                Destroy (other.gameObject);
                //deliðin büyüme oraný
                holescale += .05f;

                if (Level.Instance.objectsInScene == 0)
                {
                    UIManager.Instance.ShowLevelCompletedUI();
                    Level.Instance.PlayWinFx();
                    Invoke ("NextLevel", 2f);
                }

            }
            if (tag.Equals ("Obstacle"))
            {
                Game.isGameover = true;
                Camera.main.transform.DOShakePosition (1f,.2f,20,90f)
                .OnComplete (() => {
                    Level.Instance.RestartLevel ();

                });
            }
        }
    }

    void NextLevel ()
    {
        Level.Instance.LoadNextLevel ();
    }

}
