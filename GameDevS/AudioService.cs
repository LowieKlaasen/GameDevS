using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
namespace GameDevS
{
    public class AudioService : IAudioService
    {
        private readonly Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        private readonly Dictionary<string, SoundEffectInstance> activeSounds = new Dictionary<string, SoundEffectInstance>();

        private float masterVolume = 1f;

        public AudioService(ContentManager contentManager)
        {
            sounds["jump"] = contentManager.Load<SoundEffect>("audio/action_jump");
        }

        public void Play(string soundName)
        {
            if (sounds.TryGetValue(soundName, out var sound))
            {
                SoundEffectInstance instance = sound.CreateInstance();
                instance.Volume = masterVolume;
                instance.Play();
                activeSounds[soundName] = instance;
            }
        }

        public void Stop(string soundName)
        {
            if (activeSounds.TryGetValue(soundName, out var instance))
            {
                instance.Stop();
                activeSounds.Remove(soundName);
            }
        }

        public void SetVolume(float volume)
        {
            masterVolume = MathHelper.Clamp(volume, 0f, 1f);
        }
    }
}
