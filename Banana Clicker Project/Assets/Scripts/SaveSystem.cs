using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    

        // Define the save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    [System.Serializable]
    public class SettingsData
    {
        public int score;
        public int clickScore;
        public int auto;
    }

    // Save data to JSON
    public void Save(SettingsData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    // Load data from JSON
    public SettingsData Load()
    {
        if (SaveExists())
        {
            string json = File.ReadAllText(saveFilePath);
            SettingsData data = JsonUtility.FromJson<SettingsData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return null;
        }
    }

    // Load settings data from server
    public async Task LoadServerSettings(ServerInteraction server)
    {
        var settingsData = await server.LoadJson<SettingsData>("settings.json");

        if (settingsData != null)
        {
            Save(settingsData);
        }
        else
        {
            Debug.LogError("Error loading settings");
        }
    }

    // Check if save exists
    public bool SaveExists()
    {
        return File.Exists(saveFilePath);
    }
}
