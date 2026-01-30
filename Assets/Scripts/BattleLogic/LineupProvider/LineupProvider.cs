using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class LineupProvider : MonoBehaviour, ILineupPositionProvider
{
    [SerializeField] private int countInLine = 5;
    [SerializeField] private float distanceBetweenUnits = 3f;

    private UnitsManager unitsManager;
    private Dictionary<int, Vector2> cachedPositions;


    [Inject]
    public void Construct(UnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;
    }


    public void OnEnable()
    {
        cachedPositions = new Dictionary<int, Vector2>();
        var units = unitsManager.AllUnits;
        var positions = CalculateLineUpPositions(units.Count);

        for (var i = 0; i < units.Count; i++)
        {
            var item = units[i];
            var pos = positions[i];
            cachedPositions.Add(item.GetInstanceID(), pos);
        }

    }


    public Vector3 GetPosition(BattleEntity entity)
    {
        if(!cachedPositions.TryGetValue(entity.GetInstanceID(), out Vector2 position))
        {
            return entity.transform.position;
        }
        
        return position;
    }


    private List<Vector2> CalculateLineUpPositions(int count)
    {
        var positions = new List<Vector2>();
        var linesCount = Mathf.CeilToInt((float)count / countInLine);

        for (var lineIndex = 0; lineIndex < linesCount; lineIndex++)
        {
            var unitsInThisLine = Mathf.Min(count - lineIndex * countInLine, countInLine);
            var yPos = -lineIndex * distanceBetweenUnits;
            for (var unitIndex = 0; unitIndex < unitsInThisLine; unitIndex++)
            {
                var xPos = (unitIndex - (unitsInThisLine - 1) / 2.0f) * distanceBetweenUnits;
                positions.Add(new Vector2(xPos, yPos));
            }
        }

        return positions;
    }
}
