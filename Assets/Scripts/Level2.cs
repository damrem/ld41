using UnityEngine;
using System.Collections.Generic;

public class Level2 : MonoBehaviour
{
    void Start()
    {
        Camera camera = FindObjectOfType<Camera>();
        camera.transform.position = new Vector3(0, 0, -10);
        if (!Loader.HasRespawned) GameManager.instance.Tell(new List<string>()
        {
            "Oooooh...",
            "A 2nd room...",
            "Who would have thought?",
        });
    }
}
