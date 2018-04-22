using System;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {

    public GameObject localTellerStartRight;
    public GameObject localTellerWaitStop;
    public GameObject localTellerWhyNotStop;

    bool hasWalkedLeft = false;
    bool hasStopped = false;

    public void OnStopCommand()
    {
        if (hasStopped) return;
        hasStopped = true;
        Dbg.Log(this, "OnStopCommand");
        Destroy(localTellerWaitStop);
        Destroy(localTellerWhyNotStop);
        if (hasWalkedLeft) Game.teller.Comment(new List<string>(){
            "Thank you!",
            "Just keep on now.",
            "Type \"right\" again."
        });
        else Game.teller.Comment(new List<string>(){
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
