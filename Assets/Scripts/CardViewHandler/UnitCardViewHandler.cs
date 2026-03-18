using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Handles creation, updating, and destruction of unit card view objects.
/// </summary>
public class UnitCardViewHandler : ICardViewHandler
{
    private readonly CardPrefabFactory cardPrefabFactory;
    private readonly ICellPlacementStrategy placementStrategy;
    
    private Transform parent;
    private List<GameObject> viewObjects = new();

    public CardInstance Instance { get; private set; }
    public BoardCellPosition Position { get; private set; }



    public UnitCardViewHandler(CardPrefabFactory cardPrefabFactory, ICellPlacementStrategy placementStrategy)
    {
        this.cardPrefabFactory = cardPrefabFactory;
        this.placementStrategy = placementStrategy;
    }


    /// <summary>
    /// Initializes view handler for the given card instance.
    /// Does not spawn GameObjects immediately.
    /// </summary>
    public void CreateView(CardInstance instance, Transform parent)
    {
        if (instance is UnitCardInstance)
        {
            Instance = instance;
            this.parent = parent;

            RemoveView();
        }
    }


    /// <summary>
    /// Destroys all view objects.
    /// </summary>
    public void RemoveView()
    {
        if (viewObjects == null) return;
        foreach (var gameObject in viewObjects)
        {
            if(gameObject != null)
                GameObject.Destroy(gameObject);
        }

        viewObjects.Clear();
    }


    /// <summary>
    /// Updates or creates GameObjects based on currentSettings card state.
    /// </summary>
    public void UpdateView()
    {
        if (Instance is not UnitCardInstance unitInstance)
            return;

        var positions = placementStrategy.GetPositions(Instance.IndexCoords, unitInstance.CurrentUnitCount);
        UpdateOrCreateViews(positions);
    }


    /// <summary>
    /// Ensures the correct number of view objects exist and are positioned properly.
    /// </summary>
    private void UpdateOrCreateViews(Vector3[] positions)
    {
        int existingCount = viewObjects.Count;

        // Update existing objects
        for (int i = 0; i < Mathf.Min(existingCount, positions.Length); i++)
        {
            var obj = viewObjects[i];
            if (obj != null)
                obj.transform.position = positions[i];
        }

        // Add new objects if needed
        for (int i = existingCount; i < positions.Length; i++)
        {
            if (cardPrefabFactory.TryGetGameObject(Instance.Data, out var obj))
            {
                obj.transform.position = positions[i];
                if (parent != null) obj.transform.SetParent(parent, worldPositionStays: true);
                viewObjects.Add(obj);
            }
        }

        // Remove excess objects
        if (existingCount > positions.Length)
        {
            for (int i = existingCount - 1; i >= positions.Length; i--)
            {
                if (viewObjects[i] != null)
                    GameObject.Destroy(viewObjects[i]);

                viewObjects.RemoveAt(i);
            }
        }
    }
}
