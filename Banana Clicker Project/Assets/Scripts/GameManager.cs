using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Banana banana;

    [Space]
    [SerializeField] private SpriteRenderer bananaRenderer;
    [SerializeField] private SpriteRenderer bananaBackgroundRenderer;

    private void Start()
    {
        Load();
    }

    public async Task LoadAssetBundleAsync(ServerInteraction server)
    {
        AssetBundle bundle = await server.LoadAssetBundleAsync("banana");

        if (bundle != null)
        {
            Sprite banana = await server.LoadAssetAsync<Sprite>(bundle, "banana_single");
            Sprite bananaWhite = await server.LoadAssetAsync<Sprite>(bundle, "banana_single_white");

            if (banana != null && bananaWhite != null)
            {
                Debug.Log("Sprites successfully loaded!");

                bananaRenderer.sprite = banana;
                bananaBackgroundRenderer.sprite = bananaWhite;
            }
            else
            {
                Debug.LogError("One or both sprites were not found in the Asset Bundle!");
            }

            bundle.Unload(false);
        }
        else
        {
            Debug.LogError("Failed to load the AssetBundle!");
        }
    }

    private async void Load()
    {
        ServerInteraction server = new ServerInteraction();

        await LoadAssetBundleAsync(server);

        banana.Load();
    }

    private void Save()
    {
        banana.Save();
    }

    public async void ReloadContent()
    {
        ServerInteraction server = new ServerInteraction();

        await LoadAssetBundleAsync(server);

        await SaveSystem.Instance.LoadServerSettings(server);

        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
