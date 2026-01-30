using UnityEngine;

public interface ILineupPositionProvider
{
    Vector3 GetPosition(BattleEntity entity);
}