using UnityEngine;
using Zenject;


public class NearestTargetFinder : ITargetFinder
{
    private readonly UnitsManager unitsManager;


    [Inject]
    public NearestTargetFinder(UnitsManager unitsManager)
    {
        this.unitsManager = unitsManager;
    }


    public TargetData FindTarget(Transform transform, Team team)
    {
        var enemies = unitsManager.GetUnitsByTeam((Team)(1 - (int)team));

        var minDistance = float.MaxValue;  
        var minDistanceEnemy = (BattleEntity)null;
        Transform minDistanceEnemyTransform = null;
        Vector3 directionToEnemy = default;

        foreach (var enemy in enemies)
        {
            var enemyTransform = enemy.transform;
            var direction = enemyTransform.position - transform.position;
            var distance = direction.magnitude;

            if (distance < minDistance)
            {
                minDistanceEnemy = enemy;
                minDistance = distance;
                minDistanceEnemyTransform = enemyTransform;
                directionToEnemy = direction;
            }
        }

        var targetData = new TargetData(minDistanceEnemy, minDistanceEnemyTransform, minDistance, directionToEnemy);
        return targetData;
    }
}
