using System;
using UnityEngine;
using Zenject;


public class IBattleEntityFactory : PlaceholderFactory<IBattleEntity>
{
}

public interface ICombatant
{ 
}

public interface IMeleeAttacker : ICombatant
{
}

public interface IRangedAttacker : ICombatant
{
}


public class UnitTestFactory: MonoBehaviour
{
    [SerializeField] private bool spawnUnit;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float radius;
    [SerializeField] private int unitCount;
    [SerializeField] private BattleEntityDefinition battleEntityDefinition;


    private DiContainer container;
    private UnitsManager unitManager;
    private BattleEntityFactory battleEntityFactory;


    [Inject]
    public void Construct(UnitsManager unitManager, DiContainer container, BattleEntityFactory battleEntityFactory)
    {
        this.unitManager = unitManager;
        this.container = container;
        this.battleEntityFactory = battleEntityFactory;
    }


    private void Awake()
    {
        
    }


    private void OnEnable()
    {
        
    }


    private void Update()
    {
        if (spawnUnit)
        {
            //var subContainer = settingsContainer.CreateSubContainer();
            //subContainer.Bind<BattleEntityData>().FromMethod(x => unitData).AsTransient(); 
            //var unit = subContainer.InstantiatePrefab(prefab, transform);

            var randRadius = UnityEngine.Random.Range(0, radius);
            var angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var position = new Vector3(MathF.Cos(angle), MathF.Sin(angle)) * randRadius;

            try
            {
                var unit = battleEntityFactory.Create(battleEntityDefinition, position, Quaternion.identity);
                var strategySet = battleEntityDefinition.GetStrategySet();
                var context = battleEntityDefinition.GetEntityContext();

                unit.Initialize(battleEntityDefinition.GetInstanceID(), context);

                unitManager.Register(unit);
            }
            finally
            {
                spawnUnit = !spawnUnit;
            }

        }
    }

}
