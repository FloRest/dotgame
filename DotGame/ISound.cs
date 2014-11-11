using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame
{
    interface ISound
    {
        void PlayCoinSound();
        void PlayMissSound();
        void PlayWrongSound();
    }
}
