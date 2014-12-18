using System.Collections;
using System.Threading;

namespace DotCraftCore.nWorld.nStorage
{


	public class ThreadedFileIOBase : Runnable
	{
	/// <summary> Instance of ThreadedFileIOBase  </summary>
		public static readonly ThreadedFileIOBase threadedIOInstance = new ThreadedFileIOBase();
		private IList threadedIOQueue = Collections.synchronizedList(new ArrayList());
		private volatile long writeQueuedCounter;
		private volatile long savedIOCounter;
		private volatile bool isThreadWaiting;
		

		private ThreadedFileIOBase()
		{
			Thread var1 = new Thread(this, "File IO Thread");
			var1.Priority = 1;
			var1.Start();
		}

		public virtual void run()
		{
			while (true)
			{
				this.processQueue();
			}
		}

///    
///     <summary> * Process the items that are in the queue </summary>
///     
		private void processQueue()
		{
			for (int var1 = 0; var1 < this.threadedIOQueue.Count; ++var1)
			{
				IThreadedFileIO var2 = (IThreadedFileIO)this.threadedIOQueue.get(var1);
				bool var3 = var2.writeNextIO();

				if (!var3)
				{
					this.threadedIOQueue.Remove(var1--);
					++this.savedIOCounter;
				}

				try
				{
					Thread.Sleep(this.isThreadWaiting ? 0L : 10L);
				}
				catch (InterruptedException var6)
				{
					var6.printStackTrace();
				}
			}

			if (this.threadedIOQueue.Count == 0)
			{
				try
				{
					Thread.Sleep(25L);
				}
				catch (InterruptedException var5)
				{
					var5.printStackTrace();
				}
			}
		}

///    
///     <summary> * threaded io </summary>
///     
		public virtual void queueIO(IThreadedFileIO p_75735_1_)
		{
			if (!this.threadedIOQueue.Contains(p_75735_1_))
			{
				++this.writeQueuedCounter;
				this.threadedIOQueue.Add(p_75735_1_);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void waitForFinish() throws InterruptedException
		public virtual void waitForFinish()
		{
			this.isThreadWaiting = true;

			while (this.writeQueuedCounter != this.savedIOCounter)
			{
				Thread.Sleep(10L);
			}

			this.isThreadWaiting = false;
		}
	}

}