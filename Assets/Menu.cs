using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void LoadMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void LoadHowToPlay()
    {
        Application.LoadLevel("HowToPlay");
    }

    public void LoadGamePlay()
    {
        Application.LoadLevel("MainGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
