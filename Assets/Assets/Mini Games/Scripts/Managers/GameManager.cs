using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
#if DEDICATED_SERVER
            NewGame();
#endif
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NewGame()
    {
        var miniGameTypeScriptableObject = MiniGameLoadingSystem.LoadRandomMiniGame();
        StartCoroutine(GameTimer(miniGameTypeScriptableObject.gameDuration));
        Debug.Log("New Game: " + miniGameTypeScriptableObject.minigameName);
    }
    
    private IEnumerator GameTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        NewGame();
    }
}