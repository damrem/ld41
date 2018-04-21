using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

public enum WalkDirection {None, Left, Right};

public class Game : MonoBehaviour {

    public GameObject player;
    Rigidbody playerBody;
    float initialPlayerBodyDrag;
    public Text inputText;
    Dictionary<string, Delegates.VoidVoid> commandMap;
    const float WALK_ACCELERATION = 50;
    const float WALK_MAX_VELOCITY = 5;
    WalkDirection walkDirection = WalkDirection.None;
    Dictionary<WalkDirection, Vector3> directedWalkVectorMap;

    // Use this for initialization
    void Start () {
        playerBody = player.GetComponent<Rigidbody>();
        initialPlayerBodyDrag = playerBody.drag;

        commandMap = new Dictionary<string, Delegates.VoidVoid>();
        commandMap.Add("JUMP", Jump);
        commandMap.Add("LEFT", LeftCommand);
        commandMap.Add("RIGHT", RightCommand);
        commandMap.Add("STOP", StopCommand);

        directedWalkVectorMap = new Dictionary<WalkDirection, Vector3>()
        {
            { WalkDirection.Left, Vector3.left },
            { WalkDirection.None, Vector3.zero },
            { WalkDirection.Right, Vector3.right }
        };
    }

    void StopCommand()
    {
        Dbg.Log("StopCommand");
        player.GetComponent<Rigidbody>().drag = 5;
        walkDirection = WalkDirection.None;
    }

    private void StopWalking()
    {
        Dbg.Log("StopWalking");
        //player.GetComponent<Rigidbody>().AddForce(Vector3.left);
    }

    void RightCommand()
    {
        Dbg.Log("RightCommand");
        playerBody.drag = initialPlayerBodyDrag;
        walkDirection = WalkDirection.Right;
    }

    void Walk(WalkDirection direction)
    {
        Vector3 directedVector = directedWalkVectorMap.Get(direction);
        if (playerBody.velocity.magnitude < WALK_MAX_VELOCITY) playerBody.AddForce(directedVector * WALK_ACCELERATION);
        if (playerBody.velocity.magnitude > WALK_MAX_VELOCITY) playerBody.velocity.SetMagnitude(WALK_MAX_VELOCITY);
    }

    private void WalkRight()
    {
        Dbg.Log("WalkRight");
        Walk(WalkDirection.Right);
    }

    void LeftCommand()
    {
        Dbg.Log("LeftCommand");
        playerBody.drag = initialPlayerBodyDrag;
        walkDirection = WalkDirection.Left;
    }

    void WalkLeft()
    {
        Dbg.Log(this, "WalkLeft");
        Walk(WalkDirection.Left);
    }

    private void Jump()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
        //switch (walkingDirection)
        //{
        //    case WalkDirection.None:
        //        StopWalking();
        //        break;
        //    case WalkDirection.Left:
        //        WalkLeft();
        //        break;
        //    case WalkDirection.Right:
        //        WalkRight();
        //        break;
        //}
        Walk(walkDirection);
    }

    void OnGUI()
    {
        Event evt = Event.current;
        if (evt.isKey && evt.type == EventType.KeyUp)
        {
            //Dbg.Log(this, evt.keyCode);

            if (evt.keyCode.ToString().Length==1)
            {
                inputText.text += evt.keyCode.ToString();
            }
            else if (evt.keyCode == KeyCode.Return)
            {
                HandleTextCommand(inputText.text);
                inputText.text = "";
            }
        }
    }

    void HandleTextCommand(string textCommand)
    {
        Dbg.Log("HandleTextCommand", textCommand);
        //Delegates.VoidVoid reaction = commandMap.Get(textCommand);
        //Dbg.Log("reaction", reaction);
        //Dbg.Log("reaction", reaction != null);
        //Dbg.Log("contains", commandMap.ContainsKey(textCommand));
        if (commandMap.ContainsKey(textCommand))
            //{
            //    Dbg.Log(this, "call reaction?");
            //    //reaction();
            commandMap.Get(textCommand)();
        //}
        //Dbg.Log(this, textCommand == "LEFT");
        //if (textCommand == "LEFT")
        //{
        //    Dbg.Log(this, "textCommand  is LEFT, should WalkLeft");
        //    WalkLeft();
        //}

    }
}
