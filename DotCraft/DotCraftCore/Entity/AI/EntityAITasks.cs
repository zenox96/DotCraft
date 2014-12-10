using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using Profiler = DotCraftCore.profiler.Profiler;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class EntityAITasks
	{
		private static readonly Logger logger = LogManager.Logger;

	/// <summary> A list of EntityAITaskEntrys in EntityAITasks.  </summary>
		private IList taskEntries = new ArrayList();

	/// <summary> A list of EntityAITaskEntrys that are currently being executed.  </summary>
		private IList executingTaskEntries = new ArrayList();

	/// <summary> Instance of Profiler.  </summary>
		private readonly Profiler theProfiler;
		private int tickCount;
		private int tickRate = 3;
		private const string __OBFID = "CL_00001588";

		public EntityAITasks(Profiler p_i1628_1_)
		{
			this.theProfiler = p_i1628_1_;
		}

		public virtual void addTask(int p_75776_1_, EntityAIBase p_75776_2_)
		{
			this.taskEntries.Add(new EntityAITasks.EntityAITaskEntry(p_75776_1_, p_75776_2_));
		}

///    
///     <summary> * removes the indicated task from the entity's AI tasks. </summary>
///     
		public virtual void removeTask(EntityAIBase p_85156_1_)
		{
			IEnumerator var2 = this.taskEntries.GetEnumerator();

			while (var2.MoveNext())
			{
				EntityAITasks.EntityAITaskEntry var3 = (EntityAITasks.EntityAITaskEntry)var2.Current;
				EntityAIBase var4 = var3.action;

				if (var4 == p_85156_1_)
				{
					if (this.executingTaskEntries.Contains(var3))
					{
						var4.resetTask();
						this.executingTaskEntries.Remove(var3);
					}

					var2.remove();
				}
			}
		}

		public virtual void onUpdateTasks()
		{
			ArrayList var1 = new ArrayList();
			IEnumerator var2;
			EntityAITasks.EntityAITaskEntry var3;

			if (this.tickCount++ % this.tickRate == 0)
			{
				var2 = this.taskEntries.GetEnumerator();

				while (var2.MoveNext())
				{
					var3 = (EntityAITasks.EntityAITaskEntry)var2.Current;
					bool var4 = this.executingTaskEntries.Contains(var3);

					if (var4)
					{
						if (this.canUse(var3) && this.canContinue(var3))
						{
							continue;
						}

						var3.action.resetTask();
						this.executingTaskEntries.Remove(var3);
					}

					if (this.canUse(var3) && var3.action.shouldExecute())
					{
						var1.Add(var3);
						this.executingTaskEntries.Add(var3);
					}
				}
			}
			else
			{
				var2 = this.executingTaskEntries.GetEnumerator();

				while (var2.MoveNext())
				{
					var3 = (EntityAITasks.EntityAITaskEntry)var2.Current;

					if (!var3.action.continueExecuting())
					{
						var3.action.resetTask();
						var2.remove();
					}
				}
			}

			this.theProfiler.startSection("goalStart");
			var2 = var1.GetEnumerator();

			while (var2.MoveNext())
			{
				var3 = (EntityAITasks.EntityAITaskEntry)var2.Current;
				this.theProfiler.startSection(var3.action.GetType().SimpleName);
				var3.action.startExecuting();
				this.theProfiler.endSection();
			}

			this.theProfiler.endSection();
			this.theProfiler.startSection("goalTick");
			var2 = this.executingTaskEntries.GetEnumerator();

			while (var2.MoveNext())
			{
				var3 = (EntityAITasks.EntityAITaskEntry)var2.Current;
				var3.action.updateTask();
			}

			this.theProfiler.endSection();
		}

///    
///     <summary> * Determine if a specific AI Task should continue being executed. </summary>
///     
		private bool canContinue(EntityAITasks.EntityAITaskEntry p_75773_1_)
		{
			this.theProfiler.startSection("canContinue");
			bool var2 = p_75773_1_.action.continueExecuting();
			this.theProfiler.endSection();
			return var2;
		}

///    
///     <summary> * Determine if a specific AI Task can be executed, which means that all running higher (= lower int value) priority
///     * tasks are compatible with it or all lower priority tasks can be interrupted. </summary>
///     
		private bool canUse(EntityAITasks.EntityAITaskEntry p_75775_1_)
		{
			this.theProfiler.startSection("canUse");
			IEnumerator var2 = this.taskEntries.GetEnumerator();

			while (var2.MoveNext())
			{
				EntityAITasks.EntityAITaskEntry var3 = (EntityAITasks.EntityAITaskEntry)var2.Current;

				if (var3 != p_75775_1_)
				{
					if (p_75775_1_.priority >= var3.priority)
					{
						if (this.executingTaskEntries.Contains(var3) && !this.areTasksCompatible(p_75775_1_, var3))
						{
							this.theProfiler.endSection();
							return false;
						}
					}
					else if (this.executingTaskEntries.Contains(var3) && !var3.action.Interruptible)
					{
						this.theProfiler.endSection();
						return false;
					}
				}
			}

			this.theProfiler.endSection();
			return true;
		}

///    
///     <summary> * Returns whether two EntityAITaskEntries can be executed concurrently </summary>
///     
		private bool areTasksCompatible(EntityAITasks.EntityAITaskEntry p_75777_1_, EntityAITasks.EntityAITaskEntry p_75777_2_)
		{
			return (p_75777_1_.action.MutexBits & p_75777_2_.action.MutexBits) == 0;
		}

		internal class EntityAITaskEntry
		{
			public EntityAIBase action;
			public int priority;
			private const string __OBFID = "CL_00001589";

			public EntityAITaskEntry(int p_i1627_2_, EntityAIBase p_i1627_3_)
			{
				this.priority = p_i1627_2_;
				this.action = p_i1627_3_;
			}
		}
	}

}