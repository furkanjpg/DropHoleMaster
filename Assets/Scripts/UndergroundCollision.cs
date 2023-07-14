using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
    // Start is called before the first frame update
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
