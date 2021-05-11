using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {
    public TextMeshProUGUI textComp;
    GameManager gm;
    // Start is called before the first frame update
    void Start() {
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update() {
        string response;
        if ( gm.endGameStatus == GameManager.EndGameState.WON ) {
            gm.SetSoundFx(SoundFXManager.ClipName.WIN);
            response = "You Won!";
        } else {
            gm.SetSoundFx(SoundFXManager.ClipName.LOOSE);
            response = "You Lost!";
        }
        textComp.text = $"{response}";
    }
}
