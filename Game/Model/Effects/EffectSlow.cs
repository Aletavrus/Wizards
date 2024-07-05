using System;
using Game.Model.Player;

namespace Game.Model.Effects;

public class EffectSlow : EffectBase
{
    private int _reducedMoves;
	public EffectSlow(int reducedMoves, int duration) : base(duration)
	{
        _reducedMoves = reducedMoves;
        this.duration = duration - 1;
	}

    public override void ActivateEffects(PlayerBase playerBase)
    {
        playerBase.movesLeft = _reducedMoves;
    }
}
