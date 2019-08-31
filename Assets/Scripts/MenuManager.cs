using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[Header("Menu Stuff")]
	[SerializeField] private GameObject _mainMenu;
	[SerializeField] private GameObject _optionsMenu;
	[SerializeField] private GameObject _creditsMenu;
    [SerializeField] Texture2D cursor;
    [SerializeField] Animator animator;

    bool won;

    private void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        won = GetComponent<DataSave>().Load();
        if (won)
        {
            animator.enabled = true;
        }
    }

    public void StartGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OptionsMenu()
	{
		_mainMenu.SetActive(false);
		_optionsMenu.SetActive(true);
	}

	public void OptionsBack()
	{
		_optionsMenu.SetActive(false);
		_mainMenu.SetActive(true);
	}

	public void OptionsToCredits()
	{
		_optionsMenu.SetActive(false);
		_creditsMenu.SetActive(true);
	}

	public void CreditsBack()
	{
		_creditsMenu.SetActive(false);
		_optionsMenu.SetActive(true);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}