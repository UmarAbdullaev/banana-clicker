using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    [SerializeField] private GameObject saveSystemPrefab;
    [SerializeField] private TextMeshProUGUI welcomeText;

    private float loadingTime = 3f;

    [System.Serializable]
    public class WelcomeData
    {
        public string message;
    }

    private async void Start()
    {
        await InitializationAsync();
    }

    private async Task InitializationAsync()
    {
        float timeAnchor = Time.time;
        ServerInteraction server = new ServerInteraction();

        SaveSystem saveSystem = Instantiate(saveSystemPrefab).GetComponent<SaveSystem>();

        Debug.Log("Starting initialization...");

        // Load welcome message
        await LoadWelcomeMessage(server);

        // Load settings
        if (!saveSystem.SaveExists())
        {
            await saveSystem.LoadServerSettings(server);
        }
        else
        {
            Debug.Log("Save data exists. Skipping settings load.");
        }

        // Ensure loading screen is visible for a minimum time
        await Task.Delay(Mathf.Max((int)((loadingTime - (Time.time - timeAnchor)) * 1000), 0));

        Debug.Log("Initialization complete. Loading next scene...");
        SceneManager.LoadScene(1);
    }

    private async Task LoadWelcomeMessage(ServerInteraction server)
    {
        var welcomeMessage = await server.LoadJson<WelcomeData>("welcome.json");

        if (welcomeMessage != null)
        {
            welcomeText.text = welcomeMessage.message;
        }
        else
        {
            welcomeText.text = "<color=red>Error loading welcome message</color>";
        }
    }
}
