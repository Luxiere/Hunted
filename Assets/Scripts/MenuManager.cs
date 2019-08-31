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
    [SerializeField] private GameObject _weaponMenu;
    [SerializeField] Texture2D cursor;
    [SerializeField] Animator animator;

    public static bool won;
    
    private IEnumerator Start()
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width/2, cursor.height/2), CursorMode.Auto);
        yield return new WaitForSeconds(2f);
        if (won)
        {
            animator.enabled = true;
        }
    }

    public void WeaponMenu()
    {
        _weaponMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void WeaponBack()
    {
        _weaponMenu.SetActive(false);
        _mainMenu.SetActive(true);
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