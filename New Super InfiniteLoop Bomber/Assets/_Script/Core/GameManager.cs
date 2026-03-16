using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerState _player;
    private int _currentLevelIndex = 0;

    [Header("Scene management")]
    [SerializeField] private SceneDatabase _sceneDatabase;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //We load bootstrap and then go to first level
        LoadLevelByIndex(_currentLevelIndex);
    }

    void OnEnable()
    {
        if (_player != null)
        {
            _player.OnPlayerDeath += HandlePlayerDeath;
            
        }
    }
    void OnDisable()
    {
        if (_player != null)
        {
            _player.OnPlayerDeath -= HandlePlayerDeath;
        }
    }

    public static void RegisterPlayer(PlayerState player)
    {
        Instance?.RegisterPlayerInternal(player);
    }

    //Called when player dies
    void HandlePlayerDeath()
    {
        Invoke(nameof(ReloadCurrentLevel), 1f); // respawn after 1 sec
    }
    void RegisterPlayerInternal(PlayerState player)
    {
        
        if (player != null)
        {
            if (_player != null) 
            {
                _player.OnPlayerDeath -= HandlePlayerDeath;
            }
                
            _player = player;
            _player.OnPlayerDeath += HandlePlayerDeath;
        }
    }

    void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(_sceneDatabase.GetLevel(_currentLevelIndex).sceneName);
    }

    void LoadLevelByIndex(int index)
    {
        if (index < 0 || index >= _sceneDatabase.LevelsCount)
        {
            Debug.LogError("Invalid level index");
            return;
        }

        _currentLevelIndex = index;
        SceneManager.LoadScene(_sceneDatabase.GetLevel(index).sceneName);
    }

    void LoadNextLevel()
    {
        if (_currentLevelIndex < 0 || _currentLevelIndex >= _sceneDatabase.LevelsCount)
        {
            Debug.LogError("Invalid level index");
            return;
        }

        LoadLevelByIndex(_currentLevelIndex + 1);
    }
}

