using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevS
{
    public interface IAudioService
    {
        void Play(string soundName);
        void Stop(string soundName);
        void SetVolume(float volume);
    }
}
