using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Helpers;

public enum WalkDirection {None, Left, Right};

public class Game : MonoBehaviour {
    public static int levelIndex = 0;
    public GameObject player;
    Rigidbody2D playerBody;
    //public GameObject playerFocus;
    Walk playerWalk;
    public static StoryTeller teller;

    public Text commandLine;
    Dictionary<string, Delegates.VoidVoid> commandMap;

    public Text consoleText;

    public static Dictionary<WalkDirection, Vector2> directedWalkVectorMap;

    public float gravity = 50;

    //private void Awake()
    //{
    //    Dbg.Log(this, "awake");
    //    //DontDestroyOnLoad(GetComponent<Canvas>());
    //    //DontDestroyOnLoad(GetComponent<Light>());
    //    //DontDestroyOnLoad(GetComponent<Camera>());
    //}

    void Start ()
    {
        Dbg.Log(this, "start");
        //SceneManager.LoadScene("Level1");

        Physics2D.gravity = Vector2.down * gravity;

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
            {"enter",EnterCommand }
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
        SendMessage("OnStandup", SendMessageOptions.DontRequireReceiver);
    }

    private void CrouchCommand()
    {
        teller.Comment("You crouch.");
        playerBody.transform.localScale = new Vector3(1, 0.5f, 1);
        SendMessage("OnCrouchCommand", SendMessageOptions.DontRequireReceiver);
    }

    void StopCommand()
    {
        teller.Comment("You stop walking.");
        playerWalk.Stop();
        SendMessage("OnStopCommand", SendMessageOptions.DontRequireReceiver);
        //SendMessageUpwards("OnStopCommand", SendMessageOptions.DontRequireReceiver);
    }

    void RightCommand()
    {
        teller.Comment("You walk right.");
        playerWalk.Right();
        SendMessage("OnRightCommand", SendMessageOptions.DontRequireReceiver);
        //SendMessageUpwards("OnRightCommand", SendMessageOptions.DontRequireReceiver);
    }

    void LeftCommand()
    {
        teller.Comment("You walk left.");
        playerWalk.Left();
        SendMessage("OnLeftCommand", SendMessageOptions.DontRequireReceiver);
        //SendMessageUpwards("OnLeftCommand", SendMessageOptions.DontRequireReceiver);
    }

    void JumpCommand()
    {
        teller.Comment("You jump.");
        player.GetComponent<JumpBehavior>().Jump();
        SendMessage("OnJumpCommand", SendMessageOptions.DontRequireReceiver);
        //SendMessageUpwards("OnJumpCommand", SendMessageOptions.DontRequireReceiver);
    }

    void EnterCommand()
    {
        if (player.GetComponent<HitStuff>().IsAtDoor)
        {
            teller.Tell("You're leaving the level...");
            SceneManager.LoadScene(levelIndex + 1);
        }
        else
        {
            teller.Tell("Enter? But enter what?!?");
        }
        SendMessage("OnEnterCommand", SendMessageOptions.DontRequireReceiver);
    }

    void Update () {
        Vector2 position = playerBody.velocity.normalized * 5f;
        position.y /= 2;
        //playerFocus.transform.localPosition = position;
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
