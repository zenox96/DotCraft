using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotCraftCore.Block
{
    public class SoundType
    {
        public readonly float field_150499_b; //volume or pitch
        public readonly float field_150500_c; //volume or pitch


        public SoundType(string soundName, float p_i45393_2_, float p_i45393_3_)
        {
            this.SoundName = soundName;
            this.DigSoundName = "dig." + soundName;
            this.StepSoundName = "step." + soundName;

            this.field_150499_b = p_i45393_2_;
            this.field_150500_c = p_i45393_3_;
        }

        public SoundType(string soundName, string digSoundName, string stepSoundName, float p_i45393_2_, float p_i45393_3_)
        {
            this.SoundName = soundName;
            this.DigSoundName = "dig." + digSoundName;
            this.StepSoundName = "step." + stepSoundName;

            this.field_150499_b = p_i45393_2_;
            this.field_150500_c = p_i45393_3_;
        }

        public virtual float func_150497_c( )
        {
            return this.field_150499_b;
        }

        public virtual float func_150494_d( )
        {
            return this.field_150500_c;
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