public class AttackContext
{
    public float RechargeTimer;
    public BattleEntity Target;
    public AttackPhase phase;


    public void Reset()
    {
        RechargeTimer = 0;
        Target = null;
        phase = AttackPhase.None;
    }
}
