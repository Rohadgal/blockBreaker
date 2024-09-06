using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks; // Serialized for debugging
    SceneLoader sceneLoader;
    
    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(1/2);
        sceneLoader.LoadNextScene();
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            
            StartCoroutine(WaitThreeSeconds());
        }
    }

}