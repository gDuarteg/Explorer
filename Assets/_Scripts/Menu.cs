using UnityEngine;

public class Menu : MonoBehaviour {

    public GameObject game;
    GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    public void PlayGame() {
        gm.changeState(GameManager.GameState.GAME);
    }

    public void GoToOptions() {
        gm.changeState(GameManager.GameState.OPTIONS);
    }

    public void GoToTutorial() {
        gm.changeState(GameManager.GameState.TUTORIAL);
    }

    public void UnPauseGame() {
        gm.changeState(GameManager.GameState.GAME);
    }

    public void GoToMainMenu() {
        gm.changeState(GameManager.GameState.MENU);
    }
}
