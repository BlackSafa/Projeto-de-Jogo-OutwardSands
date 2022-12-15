using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneArtifact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneOrganizer.sceneArtifacts.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
