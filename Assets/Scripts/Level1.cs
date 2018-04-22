using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {

    public GameObject localTellerStartRight;
    public GameObject localTellerWaitStop;
    public GameObject localTellerWhyNotStop;

    bool hasWalkedLeft = false;
    bool hasStopped = false;

    private void Start()
    {
        GameManager.instance.StopCommanded += OnStopCommand;
        GameManager.instance.LeftCommanded += OnLeftCommand;
        GameManager.instance.RightCommanded += OnRightCommand;
    }

    private void OnDestroy()
    {
        GameManager.instance.StopCommanded -= OnStopCommand;
        GameManager.instance.LeftCommanded -= OnLeftCommand;
        GameManager.instance.RightCommanded -= OnRightCommand;
    }

    public void OnStopCommand()
    {
        if (hasStopped) return;
        Dbg.Log(this, "onstopcommand");
        hasStopped = true;
        Dbg.Log(this, "OnStopCommand");
        Destroy(localTellerWaitStop);
        Destroy(localTellerWhyNotStop);
        if (hasWalkedLeft) GameManager.instance.Tell(new List<string>(){
            "Thank you!",
            "Just keep on now.",
            "Type \"right\" again."
        });
        else GameManager.instance.Tell(new List<string>(){
            "Thank you!",
            "And now what?",
            "Type \"left\" maybe?"
        });
    }

    public void OnLeftCommand()
    {
        hasWalkedLeft = true;
    }

    public void OnRightCommand()
    {
        Dbg.Log(this, "OnRightCommand");
        Destroy(localTellerStartRight);
    }
}
