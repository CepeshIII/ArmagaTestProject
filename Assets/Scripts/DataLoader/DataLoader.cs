using UnityEngine;

public static class DataLoader 
{
    public static T LoadData<T>(string path) where T : ScriptableObject
    {
        T data = Resources.Load<T>(path);
        if (data == null)
        {
            Debug.LogError($"Failed to load data at path: {path}");
        }
        return data;
    }


    public static bool TryLoadData<T>(string path, out T[]data) where T : ScriptableObject
    {
        data = Resources.LoadAll<T>(path);
        if (data == null || data.Length == 0)
        {
            Debug.LogError($"Failed to load data with CardType: {typeof(T)} at path: {path}");
            return false;
        }
        else
        {
            Debug.Log($"Loaded {data.Length} Data assets with CardType: {typeof(T)}");
            return true;
        }
    }

}
