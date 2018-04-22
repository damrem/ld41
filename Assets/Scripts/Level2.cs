using UnityEngine;
using System.Collections.Generic;

public class Level2 : MonoBehaviour
{
    void Start()
    {
        if (!Loader.HasRespawned) GameManager.instance.Tell(new List<string>()
        {
            "Oooooh...",
            "A 2nd room...",
            "Who would have thought?",
        });
    }
}
