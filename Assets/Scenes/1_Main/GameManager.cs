// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Transform logoIMG;

    private void Start()
    {
		LogoAnim();
	}

	// LOGO ANIMASYONU
	private void LogoAnim()
	{
		Vector3 targetScale = new Vector3(logoIMG.transform.localScale.x + 0.04f, logoIMG.transform.localScale.y + 0.04f, logoIMG.transform.localScale.z + 0.04f);
		logoIMG.transform.DOScale(targetScale, 1f)
			.SetLoops(-1, LoopType.Yoyo);
	}

	public void ChangeScene()
	{
		int currentIndex = SceneManager.GetActiveScene().buildIndex;

		if (PlayerPrefs.GetInt("Levels") > 1)
		{
			SceneManager.LoadScene(PlayerPrefs.GetInt("Levels"));
		}
		else SceneManager.LoadScene(1);
	}
}
