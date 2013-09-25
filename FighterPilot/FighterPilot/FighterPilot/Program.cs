using System;

public enum enumTeam { RedTeam, BlueTeam, GreenTeam, Unassigned }
public enum enumFighterState { Null, Follow, Flee, Arrive }
public enum enumParticleEffect { Fade, Intensify }
public enum enumCoreSize { Small, Medium, Large }

namespace FighterPilot
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

