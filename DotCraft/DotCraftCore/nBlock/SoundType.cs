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

        public virtual readonly float Volume
        {
            get;
            set;
        }

        public virtual readonly float Pitch
        {
            get;
            set;
        }

        public virtual readonly string SoundName
        {
            get;
            set;
        }

        public virtual readonly string DigSoundName
        {
            get;
            set;
        }

        public virtual readonly string StepSoundName
        {
            get;
            set;
        }
    }
}