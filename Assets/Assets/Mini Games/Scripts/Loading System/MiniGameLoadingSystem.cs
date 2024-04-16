using System.Collections;
using System.Collections.Generic;
using FishNet;
using FishNet.Managing.Scened;
using UnityEngine;
using UnityEngine.SceneManagement;
using FishNet.Object;

public class MiniGameLoadingSystem : MonoBehaviour
{
    public static MiniGameLoadingSystem instance;

    [SerializeField] private MiniGameTypeScriptableObject[] _miniGameTypes;

    public static MiniGameTypeScriptableObject[] MiniGameTypes => instance._miniGameTypes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static MiniGameTypeScriptableObject LoadRandomMiniGame()
    {
        int randomIndex = Random.Range(0, MiniGameTypes.Length);
        string sceneName = MiniGameTypes[randomIndex].minigameScene;

        SceneLoadData sld = new SceneLoadData(sceneName);
        sld.ReplaceScenes = ReplaceOption.All;

        InstanceFinder.SceneManager.LoadGlobalScenes(sld);
        
        return MiniGameTypes[randomIndex];
    }

    public static void LoadMiniGame(string miniGameName)
    {
        foreach (MiniGameTypeScriptableObject miniGameType in MiniGameTypes)
        {
            if (miniGameType.minigameName == miniGameName)
            {
                SceneLoadData sld = new SceneLoadData(miniGameType.minigameScene);
                sld.ReplaceScenes = ReplaceOption.All;

                InstanceFinder.SceneManager.LoadGlobalScenes(sld);
                return;
            }
        }
    }
}