using UnityEngine;

public class EntityDataManager : Singleton<EntityDataManager>
{
    private EntityData[] entitiesData;
    private string path = "EntitiesData";


    new public void Awake()
    {
        base.Awake();
        LoadEntityData();
    }


    public EntityData[] GetEntitiesData() 
    { 
        return entitiesData;
    }


    public void LoadEntityData()
    {
        DataLoader.TryLoadData<EntityData>(path, out entitiesData);
    }
}
