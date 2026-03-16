using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerState _player;


    public static void RegisterPlayer(PlayerState player)
    {
        Instance?.RegisterPlayerInternal(player);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    void HandlePlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    void RegisterPlayerInternal(PlayerState player)
    {
        
        if (player != null)
        {
            _player.OnPlayerDeath -= HandlePlayerDeath;
            _player = player;
            _player.OnPlayerDeath += HandlePlayerDeath;
        }
    }
}

