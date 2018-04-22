using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

public enum WalkDirection {None, Left, Right};

public class Game : MonoBehaviour {

    public GameObject player;
    Rigidbody2D playerBody;
    public GameObject playerFocus;
    Walk playerWalk;
    public static StoryTeller teller;

    public Text commandLine;
    Dictionary<string, Delegates.VoidVoid> commandMap;

    public Text consoleText;

    public static Dictionary<WalkDirection, Vector2> directedWalkVectorMap;

    public float GRAVITY = 5;

    void Start ()
    {
        Physics2D.gravity = Vector2.down * GRAVITY;

        playerBody = player.GetComponent<Rigidbody2D>();
        playerWalk = player.GetComponent<Walk>();

        teller = GetComponent<StoryTeller>();

        commandMap = new Dictionary<string, Delegates.VoidVoid>()
        {
            {"jump", JumpCommand },
            {"left", LeftCommand},
            {"right", RightCommand},
            {"stop", StopCommand},
            {"crouch", CrouchCommand },
            {"squat", CrouchCommand },
            {"stand", StandupCommand },
            {"get up", StandupCommand },
            {"stand up", StandupCommand },
        };

        directedWalkVectorMap = new Dictionary<WalkDirection, Vector2>()
        {
            { WalkDirection.Left, Vector2.left },
            { WalkDirection.None, Vector2.zero },
            { WalkDirection.Right, Vector2.right }
        };
    }

    private void StandupCommand()
    {
        teller.Comment("You stand up.");
        playerBody.transform.localScale = new Vector3(1, 1, 1);
    }

    private void CrouchCommand()
    {
        teller.Comment("You crouch.");
        playerBody.transform.localScale = new Vector3(1, 0.5f, 1);
    }

    void StopCommand()
    {
        teller.Comment("You stop walking.");
        playerWalk.Stop();
        
    }

    void RightCommand()
    {
        teller.Comment("You walk right.");
        playerWalk.Right();
    }

    void LeftCommand()
    {
        teller.Comment("You walk left.");
        playerWalk.Left();
    }

    void JumpCommand()
    {
        teller.Comment("You jump.");
        player.GetComponent<JumpBehavior>().Jump();
    }

    void Update () {
        Vector2 position = playerBody.velocity.normalized * 5f;
        position.y /= 2;
        playerFocus.transform.localPosition = position;
    }

    void OnGUI()
    {
        Event evt = Event.current;
        if (evt.isKey && evt.type == EventType.KeyUp)
        {
            //Dbg.Log(this, evt.keyCode);
            if (evt.keyCode.ToString().Length == 1)
                commandLine.text += evt.keyCode.ToString().ToLower();
            else if (evt.keyCode == KeyCode.Return)
                HandleTextCommand(commandLine.text);
            else if (evt.keyCode == KeyCode.Backspace)
                if (commandLine.text.Length > 0) commandLine.text = commandLine.text.Substring(0, commandLine.text.Length - 1);
        }
    }

    void HandleTextCommand(string textCommand)
    {
        Dbg.Log("HandleTextCommand", textCommand);
        textCommand = textCommand.ToLower();
        if (commandMap.ContainsKey(textCommand))
            commandMap.Get(textCommand)();
        else teller.Comment("You act confusingly.");

        commandLine.text = "";
    }

}
