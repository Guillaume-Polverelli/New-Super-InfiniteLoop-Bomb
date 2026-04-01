using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/SceneDatabase")]
public class SceneDatabase : ScriptableObject
{
    [System.Serializable]
    public struct SceneEntry
    {
        public string sceneName; // Exact Scene name in the Build Settings
        public string displayName; // Optional for display
    }

    [SerializeField] private SceneEntry bootstrapScene;
    [SerializeField] private SceneEntry[] levels;

    public int LevelsCount => levels.Length;

    public SceneEntry GetLevel(int index)
    {
        if (index < 0 || index >= levels.Length)
        {
            Debug.LogError($"SceneDatabase : invalid level index {index}");
            return default;
        }
        return levels[index];
    }

    public SceneEntry Bootstrap => bootstrapScene;
}

