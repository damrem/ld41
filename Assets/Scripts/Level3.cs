using System;
using UnityEngine;

public class Level3 : MonoBehaviour {

    void Start()
    {
        Camera camera = FindObjectOfType<Camera>();
        camera.transform.position = new Vector3(0, 0, -10);

        if (!Loader.HasRespawned) GameManager.instance.Tell("Lots of pits on this one...", "Prepare to jump!");
    }
}
