using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame
{
    class _8bitSound : ISound
    {
        private List<SoundEffect> CoinSounds;
        private List<SoundEffect> WrongSounds;

        public _8bitSound()
        {
            Stream stream = TitleContainer.OpenStream("Assets/Sounds/8bit/coin-2.wav");
            Stream stream2 = TitleContainer.OpenStream("Assets/Sounds/8bit/coin.wav");
            Stream stream3 = TitleContainer.OpenStream("Assets/Sounds/8bit/wrong.wav");
            CoinSounds = new List<SoundEffect>();
            CoinSounds.Add(SoundEffect.FromStream(stream));
            CoinSounds.Add(SoundEffect.FromStream(stream2));
            WrongSounds = new List<SoundEffect>();
            WrongSounds.Add(SoundEffect.FromStream(stream3));
        }

        public void PlayCoinSound()
        {
            var rnd = new Random();
            FrameworkDispatcher.Update();
            CoinSounds[rnd.Next(0, CoinSounds.Count)].Play();
        }

        public void PlayMissSound()
        {

        }
        public void PlayWrongSound()
        {
            var rnd = new Random();
            FrameworkDispatcher.Update();
            WrongSounds[rnd.Next(0, WrongSounds.Count)].Play();
        }
    }
}
