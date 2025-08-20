using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameDevS
{
    public class Health
    {
        public int Max { get; private set; }

        public int Current { get; private set; }

        public bool IsAlive => Current > 0;

        public Health(int max)
        {
            Max = max;
            Current = max;
        }

        public void TakeDamage(int amount) 
        {
            Current -= amount;
            if (Current < 0)
            {
                Current = 0;
            }
            Debug.WriteLine("Player Health: " + Current);
        }

        public void Heal(int amount)
        {
            Current += amount;
            if (Current > Max)
            {
                Current = Max;
            }
        }
    }
}
