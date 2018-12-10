using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace MeowKun
{
    class Sounds
    {
        private SoundEffectInstance laserSoundInstance;
        private SoundEffectInstance explosionSoundInstance;
        
        public void Initialize(SoundEffect laserSound, SoundEffect explosionSound)
        {
            laserSoundInstance = laserSound.CreateInstance();
            explosionSoundInstance = explosionSound.CreateInstance();
            
        }
        //HERE WE WILL RETURN ALL TH SOUNDS ACROSS ALL THE CLASSES
        public SoundEffectInstance LAZER
        {
            get { return laserSoundInstance; }
        }
        public SoundEffectInstance EXPLOSION
        {
            get { return explosionSoundInstance; }
        }

    }
}
