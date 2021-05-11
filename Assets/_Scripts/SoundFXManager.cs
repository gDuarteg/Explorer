using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour {

    public static AudioSource audioSrc;

    public static AudioClip walkingSound, runningSound, winSound, looseSound;
    public enum ClipName { WALKING, RUNNING, WIN, LOOSE, IDLE };

    GameManager gm;
    // Start is called before the first frame update
    void Start() {
        walkingSound = Resources.Load<AudioClip>("SoundFX/walking");
        runningSound = Resources.Load<AudioClip>("SoundFX/running");
        winSound = Resources.Load<AudioClip>("SoundFX/win");
        looseSound = Resources.Load<AudioClip>("SoundFX/loose");

        audioSrc = GetComponent<AudioSource>();

        gm = GameManager.GetInstance();
        gm.soundFxMgr = this;
    }
    IEnumerator Wait() {
        yield return new WaitForSeconds(5);

    }
    public static void PlaySound(ClipName clip) {
        audioSrc.Stop();
        switch ( clip ) {
            case ClipName.WALKING:
                audioSrc.clip = walkingSound;
                audioSrc.Play();
                break;
            case ClipName.IDLE:
                break;
            case ClipName.RUNNING:
                audioSrc.clip = runningSound;
                audioSrc.Play();
                break;
            case ClipName.WIN:
                audioSrc.PlayOneShot(winSound);
                break;
            case ClipName.LOOSE:
                audioSrc.PlayOneShot(looseSound);
                break;
            default:
                break;
        }
    }
}
