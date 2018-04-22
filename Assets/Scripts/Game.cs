using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

public enum WalkDirection {None, Left, Right};

public class Game : MonoBehaviour {

    public GameObject player;
    Rigidbody2D playerBody;
    float initialPlayerBodyDrag;
    public GameObject playerFocus;
    Walk playerWalk;

    public Text inputText;
    Dictionary<string, Delegates.VoidVoid> commandMap;

    public static Dictionary<WalkDirection, Vector2> directedWalkVectorMap;

    public float GRAVITY = 5;

    void Start () {
        Physics2D.gravity = Vector2.down * GRAVITY;

        playerBody = player.GetComponent<Rigidbody2D>();
        initialPlayerBodyDrag = playerBody.drag;

        playerWalk = player.GetComponent<Walk>();

        commandMap = new Dictionary<string, Delegates.VoidVoid>()
        {
            {"JUMP", JumpCommand },
            {"LEFT", LeftCommand},
            {"RIGHT", RightCommand},
            {"STOP", StopCommand},
            {"CROUCH", CrouchCommand },
            {"SQUAT", CrouchCommand },
            {"STAND", StandupCommand },
            {"GET UP", StandupCommand },
            {"STAND UP", StandupCommand },
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
        playerBody.transform.localScale = new Vector3(1, 1, 1);
    }

    private void CrouchCommand()
    {
        playerBody.transform.localScale = new Vector3(1, 0.5f, 1);
    }

    void StopCommand()
    {
        Dbg.Log(this, "StopCommand");
        //playerBody.drag = STOP_DECELERATION;
        //playerWalk.direction = WalkDirection.None;
        playerWalk.Stop();
        
    }

    void RightCommand()
    {
        Dbg.Log("RightCommand");
        playerWalk.Right();
        //playerBody.drag = initialPlayerBodyDrag;
        //playerWalk.direction = WalkDirection.Right;
    }

    void LeftCommand()
    {
        Dbg.Log("LeftCommand");
        //playerBody.drag = initialPlayerBodyDrag;
        //walkDirection = WalkDirection.Left;
        //player.GetComponent<Walk>().direction = WalkDirection.Left;
        playerWalk.Left();
    }

    void JumpCommand()
    {
        Dbg.Log(this, "jump");
        player.GetComponent<JumpBehavior>().Jump();
    }

    // Update is called once per frame
    void Update () {
        //Walk(walkDirection);
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
            {
                inputText.text += evt.keyCode.ToString();
            }
            else if (evt.keyCode == KeyCode.Return)
            {
                HandleTextCommand(inputText.text);
                inputText.text = "";
            }
            else if (evt.keyCode == KeyCode.Backspace)
                if (inputText.text.Length > 0) inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
        }
    }

    void HandleTextCommand(string textCommand)
    {
        Dbg.Log("HandleTextCommand", textCommand);
        if (commandMap.ContainsKey(textCommand))
            commandMap.Get(textCommand)();
    }
}
