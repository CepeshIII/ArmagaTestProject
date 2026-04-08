public static class EffectFilterEvaluator
{
    public static bool CanApply(EffectInstance instance, CardInstance target)
    {
        var filter = instance.Data.filter;

        if (!MatchTargetType(filter.targetType, target))
            return false;

        if (!MatchTeam(filter.teamFilter, instance, target))
            return false;

        return true;
    }


    private static bool MatchTargetType(EffectTarget type, CardInstance target)
    {
        return type switch
        {
            EffectTarget.Unit => target is UnitCardInstance,
            EffectTarget.Building => target is BuildingCardInstance,
            _ => false
        };
    }

    private static bool MatchTeam(
        EffectTeamFilter filter,
        EffectInstance instance,
        CardInstance target)
    {
        if (filter == EffectTeamFilter.Any)
            return true;

        var source = instance.Source as CardInstance;
        if (source == null)
            return true;

        return filter switch
        {
            EffectTeamFilter.Self => source == target,
            EffectTeamFilter.Ally => source.Team == target.Team,
            EffectTeamFilter.Enemy => source.Team != target.Team,
            _ => true
        };
    }
}