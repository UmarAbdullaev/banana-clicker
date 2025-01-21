using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class ServerInteraction
{
    private string baseUrl = "http://localhost:5000/";

    // Async method to load JSON from server
    public async Task<T> LoadJson<T>(string file) where T : class
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + file))
        {
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                return JsonUtility.FromJson<T>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError($"Error loading JSON from {file}: {request.error}");
                return null;
            }
        }
    }

    // Generic method to load any asset bundle from the server
    public async Task<AssetBundle> LoadAssetBundleAsync(string file)
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(baseUrl + "assetbundle/" + file);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

            request.Dispose();
            return bundle;
        }
        else
        {
            Debug.LogError($"Failed to load AssetBundle from {baseUrl}assetbundle/{file}: {request.error}");

            request.Dispose();
            return null;
        }
    }

    // Converts the AssetBundleRequest into a Task for async/await support
    public async Task<T> LoadAssetAsync<T>(AssetBundle bundle, string assetName) where T : Object
    {
        TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

        AssetBundleRequest request = bundle.LoadAssetAsync<T>(assetName);
        request.completed += (asyncOperation) =>
        {
            if (request.asset != null)
            {
                tcs.SetResult(request.asset as T);
            }
            else
            {
                tcs.SetException(new System.Exception($"Failed to load asset {assetName}"));
            }
        };

        return await tcs.Task;
    }
}