using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWin : MonoBehaviour
{
    public Text text;

    // Update is called once per frame
    void Update()
    {
        if (DetectPlayer.didWin) {
            text.text = "YOU WIN!";
        }
    }
}
