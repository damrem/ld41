using System;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    void Start()
    {
        //GameManager.instance.gameObject.Find
        //Camera camera = FindObjectOfType<Camera>();
        //camera.transform.position = new Vector3(0, 0, -10);
        //camera.orthographicSize = 10;
        if (!Loader.HasRespawned) GameManager.instance.Tell(
            "Beware!", 
            "Here come... MONSTERS!", 
            "As a matter of fact, you can defeat them...", 
            "... by jumping on their head. Very original."
        );
    }
}
