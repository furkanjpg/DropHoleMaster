using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioSource dropSound;
    [SerializeField] AudioSource loseSound;
    [SerializeField] AudioSource winSound;

    public static float holescale = 0.8f;

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
                //deli�in b�y�me oran�
                holescale += .025f;
                // Delik b�y�d���nde �alacak ses
                dropSound.Play();

                if (Level.Instance.objectsInScene == 0)
                {
                    UIManager.Instance.ShowLevelCompletedUI();
                    // KAZANILDI�INDA �ALACAK SES
                    winSound.Play();
                    Level.Instance.PlayWinFx();
                    Invoke ("NextLevel", 2f);
                }

            }
            if (tag.Equals ("Obstacle"))
            {
                Game.isGameover = true;
                // KAYBED�LD���NDE �ALACAK SES
                loseSound.Play();
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
