using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

    static public bool didWin = false;

    void OnTriggerEnter () {
        didWin = true;
    }

}
