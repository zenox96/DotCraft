namespace DotCraftCore.nUtil
{

	using Minecraft = DotCraftCore.client.Minecraft;

	public class Timer
	{
	/// <summary> The number of timer ticks per second of real time  </summary>
		internal float ticksPerSecond;

///    
///     <summary> * The time reported by the high-resolution clock at the last call of updateTimer(), in seconds </summary>
///     
		private double lastHRTime;

///    
///     <summary> * How many full ticks have turned over since the last call to updateTimer(), capped at 10. </summary>
///     
		public int elapsedTicks;

///    
///     <summary> * How much time has elapsed since the last tick, in ticks, for use by display rendering routines (range: 0.0 -
///     * 1.0).  This field is frozen if the display is paused to eliminate jitter. </summary>
///     
		public float renderPartialTicks;

///    
///     <summary> * A multiplier to make the timer (and therefore the game) go faster or slower.  0.5 makes the game run at half-
///     * speed. </summary>
///     
		public float timerSpeed = 1.0F;

///    
///     <summary> * How much time has elapsed since the last tick, in ticks (range: 0.0 - 1.0). </summary>
///     
		public float elapsedPartialTicks;

///    
///     <summary> * The time reported by the system clock at the last sync, in milliseconds </summary>
///     
		private long lastSyncSysClock;

///    
///     <summary> * The time reported by the high-resolution clock at the last sync, in milliseconds </summary>
///     
		private long lastSyncHRClock;
		private long field_74285_i;

///    
///     <summary> * A ratio used to sync the high-resolution clock to the system clock, updated once per second </summary>
///     
		private double timeSyncAdjustment = 1.0D;
		

		public Timer(float p_i1018_1_)
		{
			this.ticksPerSecond = p_i1018_1_;
			this.lastSyncSysClock = Minecraft.SystemTime;
			this.lastSyncHRClock = System.nanoTime() / 1000000L;
		}

///    
///     <summary> * Updates all fields of the Timer using the current time </summary>
///     
		public virtual void updateTimer()
		{
			long var1 = Minecraft.SystemTime;
			long var3 = var1 - this.lastSyncSysClock;
			long var5 = System.nanoTime() / 1000000L;
			double var7 = (double)var5 / 1000.0D;

			if(var3 <= 1000L && var3 >= 0L)
			{
				this.field_74285_i += var3;

				if(this.field_74285_i > 1000L)
				{
					long var9 = var5 - this.lastSyncHRClock;
					double var11 = (double)this.field_74285_i / (double)var9;
					this.timeSyncAdjustment += (var11 - this.timeSyncAdjustment) * 0.20000000298023224D;
					this.lastSyncHRClock = var5;
					this.field_74285_i = 0L;
				}

				if(this.field_74285_i < 0L)
				{
					this.lastSyncHRClock = var5;
				}
			}
			else
			{
				this.lastHRTime = var7;
			}

			this.lastSyncSysClock = var1;
			double var13 = (var7 - this.lastHRTime) * this.timeSyncAdjustment;
			this.lastHRTime = var7;

			if(var13 < 0.0D)
			{
				var13 = 0.0D;
			}

			if(var13 > 1.0D)
			{
				var13 = 1.0D;
			}

			this.elapsedPartialTicks = (float)((double)this.elapsedPartialTicks + var13 * (double)this.timerSpeed * (double)this.ticksPerSecond);
			this.elapsedTicks = (int)this.elapsedPartialTicks;
			this.elapsedPartialTicks -= (float)this.elapsedTicks;

			if(this.elapsedTicks > 10)
			{
				this.elapsedTicks = 10;
			}

			this.renderPartialTicks = this.elapsedPartialTicks;
		}
	}

}