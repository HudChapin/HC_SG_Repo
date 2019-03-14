using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;

namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Gameflow Start = new Gameflow();
            Start.StartGame();
        }
    }
}
