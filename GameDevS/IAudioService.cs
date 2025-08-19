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
        public float GetVolume(VolumeType volumeType);
        //void SetSoundEffectVolume(float volume);
        //void SetMusicVolume(float volume);
        public void SetVolume(VolumeType volumeType, float volume);
        void PlayMusic(string musicName, bool loop = true);
        void StopMusic();
    }
}
