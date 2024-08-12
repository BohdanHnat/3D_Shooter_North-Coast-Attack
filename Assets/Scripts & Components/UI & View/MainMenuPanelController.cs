using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanelController : MonoBehaviour
{
    private void Start()
    {
        Context.Instance.AudioSystem.PlayMusic(new AudioData("Waiting_game(loop)", volume: 0.3f));
    }
    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);

        Context.Instance.AudioSystem.PlayMusic(new AudioData("Assault(loop) - angry - tension - battle", volume: 0.3f));
    }
    public void OnExitPressed()
    {
        Application.Quit();
    }
}
