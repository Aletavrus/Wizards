using Game.Model.Player;

namespace Game.Model.Effects;

public class EffectPoison : EffectBase
{
    private int _poisonDamage;
    
    public EffectPoison(int poisonDamage, int duration) : base(duration)
    {
        this.duration = duration;
        _poisonDamage = poisonDamage;
    }

    public override void ActivateEffects(PlayerBase player)
    {
        player.Damage(_poisonDamage);
    }

    
}