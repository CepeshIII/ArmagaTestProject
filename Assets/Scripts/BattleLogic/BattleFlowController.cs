using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public interface IBattleState
{
    void Enter();
    void Tick();
    void Exit();
}


public sealed class BattleFlowController: ITickable
{
    private IBattleState currentState;

    private readonly Dictionary<BattleState, IBattleState> states;

    private readonly UnitsManager unitsManager;
    private readonly BattlePhaseController phaseController;

    private BattleState currentStateId;

    public event Action<Team> OnBattleFinished;


    public BattleFlowController(
        UnitsManager unitsManager,
        BattlePhaseController phaseController)
    {
        this.unitsManager = unitsManager;
        this.phaseController = phaseController;

        states = new Dictionary<BattleState, IBattleState>
        {
            { BattleState.Lineup, new LineupState(this) },
            { BattleState.Facing, new FacingState(this) },
            { BattleState.Battle, new BattleStateCombat(this) },
            { BattleState.ReturnToCell, new ReturnToCellState(this) }
        };
    }


    public void StartBattle()
    {
        ChangeState(BattleState.Lineup);
    }


    public void Tick()
    {
        currentState?.Tick();
    }


    public void ChangeState(BattleState newState)
    {
        currentState?.Exit();

        currentStateId = newState;
        currentState = states[newState];

        currentState.Enter();
    }


    internal void NotifyBattleFinished(Team winner)
    {
        OnBattleFinished?.Invoke(winner);
    }


    // === Queries used by states ===

    internal IEnumerable<BattleEntity> AllUnits => unitsManager.AllUnits;

    internal bool AreAllUnitsLinedUp()
    {
        foreach (var unit in AllUnits)
        {
            if (!unit.IsAlive)
                continue;

            var IsUnitInPosition = unit.Context.MovementData.threshold >= unit.CurrentMoveData.distanceToTarget;
            if (!IsUnitInPosition)
                return false;
        }
        return true;
    }


    internal bool IsTeamAlive(Team team)
    {
        foreach (var unit in unitsManager.GetUnitsByTeam(team))
        {
            if (unit.IsAlive)
                return true;
        }
        return false;
    }


    internal void ApplyPhase(BattlePhase phase)
    {
        phaseController.ApplyPhase(phase, AllUnits);
    }


    public enum BattleState
    {
        Lineup,
        Facing,
        Battle,
        ReturnToCell
    }
}

internal sealed class LineupState : IBattleState
{
    private readonly BattleFlowController flow;

    public LineupState(BattleFlowController flow)
    {
        this.flow = flow;
    }

    public void Enter()
    {
        flow.ApplyPhase(BattlePhase.LiningUp);
    }

    public void Tick()
    {
        if (flow.AreAllUnitsLinedUp())
        {
            flow.ChangeState(BattleFlowController.BattleState.Facing);
        }
    }

    public void Exit() { }
}


internal sealed class FacingState : IBattleState
{
    private readonly BattleFlowController flow;
    private float timer = 3f;
    private float time;


    public FacingState(BattleFlowController flow)
    {
        this.flow = flow;
    }

    public void Enter()
    {
        flow.ApplyPhase(BattlePhase.Facing);
        time = timer;
    }

    public void Tick()
    {
        time -= Time.deltaTime;

        if (time <= 0f)
        {
            flow.ChangeState(BattleFlowController.BattleState.Battle);
        }
    }

    public void Exit() { }
}



internal sealed class BattleStateCombat : IBattleState
{
    private readonly BattleFlowController flow;

    public BattleStateCombat(BattleFlowController flow)
    {
        this.flow = flow;
    }

    public void Enter()
    {
        flow.ApplyPhase(BattlePhase.Combat);
    }

    public void Tick()
    {
        if (!flow.IsTeamAlive(Team.Enemy))
        {
            flow.ChangeState(BattleFlowController.BattleState.ReturnToCell);
            return;
        }

        if (!flow.IsTeamAlive(Team.Player))
        {
            flow.ApplyPhase(BattlePhase.Facing);
            flow.NotifyBattleFinished(Team.Enemy);
        }
    }

    public void Exit() { }
}


internal sealed class ReturnToCellState : IBattleState
{
    private readonly BattleFlowController flow;

    public ReturnToCellState(BattleFlowController flow)
    {
        this.flow = flow;
    }

    public void Enter()
    {
        flow.ApplyPhase(BattlePhase.ReturningToCells);
    }

    public void Tick()
    {
        if (flow.AreAllUnitsLinedUp())
        {
            flow.NotifyBattleFinished(Team.Player);
            flow.ApplyPhase(BattlePhase.Facing);
        }
    }

    public void Exit() { }
}
