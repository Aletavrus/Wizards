using System;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Avalonia;
using Game.Model.Player;
using Game.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;

namespace Game.Model.Spells;

public class SpellTargeted(GameMap GameMap)
{
    public GameMap GameMap { get; set; } = GameMap;

    public int Execute(Point location)
    {
        int damage = 0;
        Debug.WriteLine("Clicked on cell. Executing spell");
        if (GameMap.GameObjects[Convert.ToInt16(location.X)/100, Convert.ToInt16(location.Y)/100] != 1)
        {
            Debug.WriteLine("Not on target. No damage");
        }
        else
        {
            Debug.WriteLine("Player found. Damaging player");
            damage = Random.Shared.Next(1, 11);
        }
        Debug.WriteLine("Stopped casting spell");
        return damage;

        //int[] result = new int[2];
        //int damage = 0;
        //Debug.WriteLine("Clicked on cell. Executing spell");
        //if (GameMap.GameObjects[Convert.ToInt16(location.X) / 100, Convert.ToInt16(location.Y) / 100] == 0)
        //{
        //    Debug.WriteLine("Not on target. No damage");
        //    result[0] = 0;
        //}
        //else
        //{
        //    Debug.WriteLine("Player found. Damaging player");
        //    result[0] = GameMap.GameObjects[Convert.ToInt16(location.X) / 100, Convert.ToInt16(location.Y) / 100];
        //    damage = Random.Shared.Next(1, 11);
        //}
        //result[1] = damage;
        //Debug.WriteLine("Stopped casting spell");
        //return result;
    }
}
