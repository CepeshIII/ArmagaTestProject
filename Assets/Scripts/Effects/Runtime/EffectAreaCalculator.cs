using System;
using System.Collections.Generic;
using UnityEngine;


public static class EffectAreaCalculator
{
    public static IEnumerable<Vector2Int> GetPositions(EffectArea effectArea, Vector2Int origin, Vector2Int boardSize)
    {
        switch (effectArea.areaType)
        {
            case EffectAreaType.SingleCell:
                yield return origin;
                break;

            case EffectAreaType.Radius:
                for (int x = -effectArea.range; x <= effectArea.range; x++)
                {
                    for (int y = -effectArea.range; y <= effectArea.range; y++)
                    {
                        yield return origin + new Vector2Int(x, y);
                    }
                }
                break;

            case EffectAreaType.Global:
                for (int x = 0; x < boardSize.x; x++)
                {
                    for (int y = 0; y < boardSize.y; y++)
                    {
                        yield return new Vector2Int(x, y);
                    }
                }
                break;
        }
    }
}



