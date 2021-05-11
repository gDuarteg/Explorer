using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SetVolume : MonoBehaviour {
    public AudioMixer mixer;

    public string volName;

    public void SetLevel(float sliderValue) {
        mixer.SetFloat(volName, Mathf.Log10(sliderValue) * 20);
    }
}
