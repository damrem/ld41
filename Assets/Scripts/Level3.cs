using System;
using UnityEngine;

public class Level3 : MonoBehaviour {

    void Start()
    {
        Dbg.Log(this, GameManager.instance.GetComponent<Camera>());
        if (!Loader.HasRespawned) GameManager.instance.Tell("Lots of pits on this one...", "Prepare to jump!");
    }
}
