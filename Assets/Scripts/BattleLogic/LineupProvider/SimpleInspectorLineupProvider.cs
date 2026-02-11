using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class SimpleInspectorLineupProvider : MonoBehaviour, ILineupPositionProvider
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

        var count = 0;
        foreach (var position in units) 
        {
            count++;
        }

        var positions = CalculateLineUpPositions(count);

        count = 0;
        foreach (var unit in units)
        {
            var pos = positions[count];
            cachedPositions.Add(unit.GetInstanceID(), pos);
            count++;
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
