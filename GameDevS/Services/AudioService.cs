using GameDevS.Scenes.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace GameDevS.Services
{
    public class AudioService : IAudioService
    {
        private readonly Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        private readonly Dictionary<string, SoundEffectInstance> activeSounds = new Dictionary<string, SoundEffectInstance>();

        private readonly Dictionary<string, Song> songs = new Dictionary<string, Song>();

        private float soundEffectVolume = 1f;
        private float musicVolume = 1f;

        public AudioService(ContentManager contentManager)
        {
            sounds["jump"] = contentManager.Load<SoundEffect>("audio/action_jump");
            sounds["monsterDeath"] = contentManager.Load<SoundEffect>("audio/death_squish_quieter");
            sounds["playerHurt"] = contentManager.Load<SoundEffect>("audio/LSW_GuardHurt_quieter");
            sounds["coinCollected"] = contentManager.Load<SoundEffect>("audio/neoGeoCoin");
            sounds["playerDeath"] = contentManager.Load<SoundEffect>("audio/mega-man-out");

            songs["jungleBG"] = contentManager.Load<Song>("audio/jungleMusic");
            songs["introMusic"] = contentManager.Load<Song>("audio/hidden_tombs");
            songs["templeBG"] = contentManager.Load<Song>("audio/AztecTemple");
        }

        public void Play(string soundName)
        {
            if (sounds.TryGetValue(soundName, out var sound))
            {
                SoundEffectInstance instance = sound.CreateInstance();
                instance.Volume = soundEffectVolume;
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

        public float GetVolume(VolumeType volumeType)
        {
            if (volumeType == VolumeType.MUSIC)
            {
                return musicVolume;
            }
            return soundEffectVolume;
        }

        public void SetVolume(VolumeType volumeType, float volume)
        {
            if (volumeType == VolumeType.MUSIC)
            {
                musicVolume = MathHelper.Clamp(volume, 0f, 1f);
                MediaPlayer.Volume = musicVolume;
                System.Diagnostics.Debug.WriteLine(volumeType + ": " + volume);
                return;
            }
            soundEffectVolume = MathHelper.Clamp(volume, 0f, 1f);

            System.Diagnostics.Debug.WriteLine(volumeType + ": " + volume);
        }

        public void PlayMusic(string musicName, bool loop = true) 
        {
            if (songs.TryGetValue(musicName, out Song song))
            {
                MediaPlayer.Volume = musicVolume;
                MediaPlayer.IsRepeating = loop;
                MediaPlayer.Play(song);
            }
        }

        public void StopMusic() 
        {
            MediaPlayer.Stop();
        }
    }
}
