using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour
{
    Text textComp;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        float minutes = Mathf.FloorToInt(gm.remainingTime / 60);
        float seconds = Mathf.FloorToInt(gm.remainingTime % 60);

        textComp.text = string.Format("Tempo {0:00}:{1:00}", minutes, seconds);
    }
}
