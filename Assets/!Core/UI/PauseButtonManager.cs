using UnityEngine;

public class PauseButtonManager : MonoBehaviour
{
    [SerializeField] private PauseMenuController controller;
    public async void Resume()
    {
       await controller.ClosePauseMenu();
    }

    public void Respawn()
    {
		Time.timeScale = 1f;
		LevelManager.Instance.ReSpawn();
    }

    public void MainMenu()
    {
        LevelManager.Instance.loadMainMenu();
    }

    public void Quit()
    {
        LevelManager.Instance.QuitGame();
    }

    public async void ClosePauseMenu()
    {

    }
}
