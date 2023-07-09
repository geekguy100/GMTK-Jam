using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Sene Data")]
    public SceneData sceneData;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Loads the game scene
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadScene(sceneData.gameSceneName);
    }
}
