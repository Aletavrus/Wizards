using Game.Model.Player;

namespace Game.Model.Effects;

public abstract class EffectBase
{
    protected int duration;

    protected EffectBase(int duration)
    {
        this.duration = duration;
    }

    /// <summary>
    /// Reduce the duration of effect
    /// </summary>
    /// <param name="amount">How much to reduce</param>
    /// <returns>if effect is still active</returns>
    public bool ReduceDuration(int amount)
    {
        duration -= amount;

        if (duration <= 0)
        {
            return false;
        }

        return true;
    }

    public virtual void ActivateEffects(PlayerBase playerBase)
    {
        
    }
}