using GameDevS.Scenes.Util;

namespace GameDevS.Services
{
    public interface IAudioService
    {
        void Play(string soundName);
        void Stop(string soundName);
        public float GetVolume(VolumeType volumeType);
        public void SetVolume(VolumeType volumeType, float volume);
        void PlayMusic(string musicName, bool loop = true);
        void StopMusic();
    }
}
