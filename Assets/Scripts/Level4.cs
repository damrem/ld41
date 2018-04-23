using System;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    void Start()
    {
        Camera camera = FindObjectOfType<Camera>();
        camera.transform.position = new Vector3(0, 0, -10);

        //GameManager.instance.player.transform.position = new Vector2(76, 0);
        if (!Loader.HasRespawned) GameManager.instance.Tell(
            "The smell of death surrounds you.",
            "This is the last levelllll!!!",
            "Here come... MONSTERS!", 
            "Fear! Fear!", 
            "Go now..."
        );
    }
}
