using System.Collections.Generic;
using UnityEngine;


public enum EffectArea
{
    SingleCell,
    Radius,
    Global,
}


public enum EffectTarget
{
    building,
    units,
}


public static class EffectsAreaParser
{
    public static IEnumerable<Vector2Int> ParseEffectArea(EffectArea effectArea, Vector2Int position, Vector2Int areaSize)
    {
        switch (effectArea)
        {
            case EffectArea.SingleCell:
                yield return position;
                break;

            case EffectArea.Radius:
                for(int x = -1; x <= 1; x++)
                {
                    for(int y = -1; y <= 1; y++)
                    {
                        yield return position + new Vector2Int(x, y);
                    }
                }
                break;

            case EffectArea.Global:
                for (int x = 0; x < areaSize.x; x++)
                {
                    for (int y = 0; y < areaSize.y; y++)
                    {
                        yield return new Vector2Int(x, y);
                    }
                }
                break;
        }
    }
}


