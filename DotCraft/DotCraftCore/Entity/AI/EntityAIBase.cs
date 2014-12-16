namespace DotCraftCore.Entity.AI
{

	public abstract class EntityAIBase
	{
///    
///     <summary> * A bitmask telling which other tasks may not run concurrently. The test is a simple bitwise AND - if it yields
///     * zero, the two tasks may run concurrently, if not - they must run exclusively from each other. </summary>
///     
		private int mutexBits;
		

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public abstract bool shouldExecute();

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public virtual bool continueExecuting()
		{
			return this.shouldExecute();
		}

///    
///     <summary> * Determine if this AI Task is interruptible by a higher (= lower value) priority task. </summary>
///     
		public virtual bool isInterruptible()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public virtual void startExecuting()
		{
		}

///    
///     <summary> * Resets the task </summary>
///     
		public virtual void resetTask()
		{
		}

///    
///     <summary> * Updates the task </summary>
///     
		public virtual void updateTask()
		{
		}

///    
///     <summary> * Sets a bitmask telling which other tasks may not run concurrently. The test is a simple bitwise AND - if it
///     * yields zero, the two tasks may run concurrently, if not - they must run exclusively from each other. </summary>
///     
		public virtual int MutexBits
		{
			set
			{
				this.mutexBits = value;
			}
			get
			{
				return this.mutexBits;
			}
		}

///    
///     <summary> * Get a bitmask telling which other tasks may not run concurrently. The test is a simple bitwise AND - if it yields
///     * zero, the two tasks may run concurrently, if not - they must run exclusively from each other. </summary>
///     
	}

}