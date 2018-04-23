using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Helpers;

public enum WalkDirection {None, Left, Right};

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public static int levelIndex = 0;

    public Canvas canvas;
    public Text commandLine;
    public GameObject player;
    public Text consoleText;

    Rigidbody2D playerBody;
    Walk playerWalk;
    Dictionary<string, Delegates.VoidVoid> commandMap;

    List<string> storyLines = new List<string>();
    public int storyLineCapacity = 12;

    public static Dictionary<WalkDirection, Vector2> directedWalkVectorMap = new Dictionary<WalkDirection, Vector2>()
    {
        { WalkDirection.Left, Vector2.left },
        { WalkDirection.None, Vector2.zero },
        { WalkDirection.Right, Vector2.right }
    };

    public float gravity = 50;
    public bool mustGiveCommandFeedback = true;
    public bool isAtFinalDoor = false;

    public event Action StopCommanded;
    public event Action LeftCommanded;
    public event Action RightCommanded;
    public event Action StandupCommanded;
    public event Action CrouchCommanded;
    public event Action JumpCommanded;
    public event Action ExitCommanded;


    private void Awake()
    {
        Dbg.Log(this, "awake");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(canvas);

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
            {"exit",ExitCommand },
            {"win", EnjoyVictoryCommand }
        };

        playerWalk = player.GetComponent<Walk>();
        playerBody = player.GetComponent<Rigidbody2D>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Dbg.Log(this, "OnSceneLoad", scene, mode);
        if (mode != LoadSceneMode.Single) return;
        playerBody.position = new Vector2();
        playerBody.velocity = new Vector2();
        playerWalk.Stop();
        player.GetComponent<Mortal>().Resurrect();
    }

    void Start ()
    {
        Dbg.Log(this, "start");
        storyLines = new List<string>();
        Physics2D.gravity = Vector2.down * gravity;
    }

    private void EnjoyVictoryCommand()
    {
        if (Loader.CurrentLevelIndex == 4 && isAtFinalDoor)
        {
            if (mustGiveCommandFeedback) Tell("You enjoy victory.");
            Loader.NextLevel();
        }
        else
        {
            if (mustGiveCommandFeedback) Tell("Cheaters will be banned.");
        }
    }

    private void StandupCommand()
    {
        if(mustGiveCommandFeedback) Tell("You stand up.");
        playerBody.transform.localScale = new Vector3(1, 1, 1);
        if (StandupCommanded != null) StandupCommanded.Invoke();
    }

    private void CrouchCommand()
    {
        if (mustGiveCommandFeedback) Tell("You crouch.");
        playerBody.transform.localScale = new Vector3(1, 0.5f, 1);
        if (CrouchCommanded != null) CrouchCommanded.Invoke();
    }

    void StopCommand()
    {
        if (mustGiveCommandFeedback) Tell("You stop walking.");
        playerWalk.Stop();
        if (StopCommanded != null) StopCommanded.Invoke();
    }

    void RightCommand()
    {
        if (mustGiveCommandFeedback) Tell("You walk right.");
        playerWalk.Right();
        if (RightCommanded != null) RightCommanded.Invoke();
    }

    void LeftCommand()
    {
        if (mustGiveCommandFeedback) Tell("You walk left.");
        playerWalk.Left();
        if (LeftCommanded != null) LeftCommanded.Invoke();
    }

    void JumpCommand()
    {
        if (mustGiveCommandFeedback) Tell("You jump.");
        player.GetComponent<JumpBehavior>().Jump();
        if (JumpCommanded != null) JumpCommanded.Invoke();
    }

    void ExitCommand()
    {
        if (player.GetComponent<HitStuff>().IsAtDoor)
        {
            if (mustGiveCommandFeedback) Tell("You're leaving the level...");
            Loader.NextLevel();
        }
        else
        {
            if (mustGiveCommandFeedback) Tell("Exit? You need a door for that!");
        }
        if (ExitCommanded != null) ExitCommanded.Invoke();
    }

    void Update () {
        Vector2 position = playerBody.velocity.normalized * 5f;
        position.y = 2;
        player.transform.GetChild(0).localPosition = position;
        
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
            {
                if (commandLine.text.Length > 0)
                    commandLine.text = commandLine.text.Substring(0, commandLine.text.Length - 1);
            }
            else if (evt.keyCode == KeyCode.Escape)
                commandLine.text = "";
            else if (evt.keyCode == KeyCode.Space)
                commandLine.text += " ";
        }
    }

    void HandleTextCommand(string textCommand)
    {
        if (!mustGiveCommandFeedback) return;
        Dbg.Log("HandleTextCommand", textCommand);
        textCommand = textCommand.ToLower();
        Tell("> " + textCommand);
        if (commandMap.ContainsKey(textCommand))
            commandMap.Get(textCommand)();
        else Tell("You act confusingly.");

        commandLine.text = "";
    }


    public void Tell(params string[] sthg)
    {
        Tell(new List<string>(sthg));
    }

    public void Tell(string sthg)
    {
        storyLines.Add(sthg);
        int startIndex = Math.Max(0, storyLines.Count - storyLineCapacity);
        int count = Math.Min(storyLines.Count, storyLineCapacity);
        List<string> lastLines = storyLines.GetRange(startIndex, count);
        string trimmed = "";
        foreach (string line in lastLines)
            trimmed += line + "\n";
        consoleText.text = trimmed;
    }

    public void Tell(List<string> sthg, float delaySec = 2)
    {
        StartCoroutine(MultiComment(sthg, delaySec));
    }

    public void ClearConsole()
    {
        storyLines.Clear();
        consoleText.text = "";
    }

    IEnumerator MultiComment(List<string> speeches, float delaySec = 2)
    {
        foreach (string speech in speeches)
        {
            Tell(speech);
            yield return new WaitForSeconds(delaySec);
        }
    }

}
