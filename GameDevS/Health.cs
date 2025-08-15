using Microsoft.Xna.Framework;

namespace GameDevS
{
    internal class Health
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
