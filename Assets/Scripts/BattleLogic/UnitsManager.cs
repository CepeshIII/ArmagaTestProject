using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;


public class UnitsManager: MonoBehaviour, IUnitManager, IInitializable
{
    [SerializeField]
    private List<BattleEntity> units = new();

    private List<BattleEntity> initialUnits;

    public List<BattleEntity> AllUnits => units;



    [Inject]
    public void Construct(List<BattleEntity> units)
    {
        Debug.Log("UnitsManager Construct");
        initialUnits = units;
    }


    private void Awake()
    {
        Debug.Log("UnitsManager Awake");
    }


    public void Initialize()
    {
        Debug.Log("UnitsManager Initialize");

        initialUnits.ForEach(x => Register(x));
    }


    public List<BattleEntity> GetUnitsByTeam(Team team)
    {
        return units.Where(x => x.Context.BattleEntityData.team == team).ToList();
    }


    public void Register(BattleEntity unit)
    {
        units.Add(unit);
        unit.OnDied += (sender, args) =>
        {
            units.Remove(unit);
        };
    }
}
