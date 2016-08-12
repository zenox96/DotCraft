using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotCraftCore.nBlock
{
    public class SoundType
    {
        public SoundType(string soundName, float volume, float pitch)
        {
            this.SoundName = soundName;

            this.DigSoundName = "dig." + soundName;
            this.StepSoundName = "step." + soundName;

            this.Volume = volume;
            this.Pitch = pitch;
        }

        public SoundType(string soundName, string digSoundName, string stepSoundName, float volume, float pitch)
        {
            this.SoundName = soundName;

            this.DigSoundName = "dig." + digSoundName;
            this.StepSoundName = "step." + stepSoundName;

            this.Volume = volume;
            this.Pitch = pitch;
        }

        public virtual float Volume
        {
            get;
            private set;
        }

        public virtual float Pitch
        {
            get;
            private set;
        }

        public virtual string SoundName
        {
            get;
            private set;
        }

        public virtual string DigSoundName
        {
            get;
            private set;
        }

        public virtual string StepSoundName
        {
            get;
            private set;
        }
    }
}