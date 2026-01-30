using System.Collections.Generic;

public interface IUnitManager
{
    public List<BattleEntity> GetUnitsByTeam(Team team);
    public void Register(BattleEntity unit);
}
