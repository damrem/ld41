using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameObject gameManagerPrefab;          //GameManager prefab to instantiate.
    public GameObject soundManager;         //SoundManager prefab to instantiate.

    public int startLevelIndex = 1;


    private static bool hasRespawned;
    public static bool HasRespawned { get { return hasRespawned; } }

    static int currentLevelIndex;
    public static int CurrentLevelIndex
    {
        get
        {
            return currentLevelIndex;
        }
    }

    // Use this for initialization
    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)

            //Instantiate gameManager prefab
            Instantiate(gameManagerPrefab);

        currentLevelIndex = startLevelIndex - 1;
        NextLevel();

        //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
        //if (SoundManager.instance == null)

            //Instantiate SoundManager prefab
          //  Instantiate(soundManager);
    }

    public static void Respawn()
    {
        Dbg.Log("", "Respawn");
        hasRespawned = true;
        SceneManager.LoadScene("Level" + currentLevelIndex);
    }

    public static void NextLevel()
    {
        hasRespawned = false;
        currentLevelIndex++;
        SceneManager.LoadScene("Level"+currentLevelIndex);
        if (currentLevelIndex == 5) GameManager.instance.mustGiveCommandFeedback = false;
    }
}
