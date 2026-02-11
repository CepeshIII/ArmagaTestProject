using System.Collections.Generic;

public interface IUnitManager
{
    public IEnumerable<BattleEntity> GetUnitsByTeam(Team team);
    public void Register(BattleEntity unit);
}
