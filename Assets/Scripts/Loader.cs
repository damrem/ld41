using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public GameObject gameManagerPrefab;          //GameManager prefab to instantiate.
    public GameObject soundManager;         //SoundManager prefab to instantiate.

    public int startLevelIndex = 1;

    static int currentLevelIndex;

    private static bool hasRespawned;
    public static bool HasRespawned { get { return hasRespawned; } }

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
        hasRespawned = true;
        SceneManager.LoadScene("Level" + currentLevelIndex);
    }

    public static void NextLevel()
    {
        hasRespawned = false;
        currentLevelIndex++;
        SceneManager.LoadScene("Level"+currentLevelIndex);
    }
}
