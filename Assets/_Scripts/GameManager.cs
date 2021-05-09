public class GameManager {

    private static GameManager _instance;

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    public enum GameState { MENU, GAME, PAUSE, ENDGAME, OPTIONS };

    public GameState currentState { get; private set; }

    public float remainingTime;

    public PlayerController player { get; set; }

    private GameManager() {
        currentState = GameState.MENU;
    }

    public static GameManager GetInstance() {
        if ( _instance == null ) {
            _instance = new GameManager();
        }

        return _instance;
    }
    public void changeState(GameState nextState) {
        if ( currentState != GameState.PAUSE || currentState != GameState.OPTIONS && nextState == GameState.GAME ) Reset();
        currentState = nextState;
        changeStateDelegate();

        //if (gameState == GameState.PAUSE || gameState == GameState.MENU || gameState == GameState.ENDGAME) {
        //    audioMgr.PauseAllMusic();
        //}

        //if (gameState == GameState.GAME) {
        //    if (currentTurn == PlayerTurn.PLAYER1) {
        //        audioMgr.SetLulaMusic();
        //    }
        //    else if (currentTurn == PlayerTurn.PLAYER2) {
        //        audioMgr.SetBolsonoaroMusic();
        //    }
        //}
    }
    public void Reset() {
        remainingTime = 0;
        player.ResetPosition();
    }

}