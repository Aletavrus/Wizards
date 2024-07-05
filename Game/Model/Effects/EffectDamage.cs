using Game.Model.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model.Effects;
public class EffectDamage: EffectBase
{
    private int _increaseDamage;
    public EffectDamage(int duration, int increaseDamage): base(duration)
    {
        _increaseDamage = increaseDamage;
    }

    public override void ActivateEffects(PlayerBase playerBase)
    {
        playerBase.damageIncrease = _increaseDamage;
    }
}
