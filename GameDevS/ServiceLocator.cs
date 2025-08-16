using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevS
{
    public static class ServiceLocator
    {
        public static IAudioService AudioService { get; set; }
        public static IGameExitService GameExitService { get; set; }
    }
}
