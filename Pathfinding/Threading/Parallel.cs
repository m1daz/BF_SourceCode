using System;
using System.Threading;

namespace Pathfinding.Threading
{
	// Token: 0x020000BC RID: 188
	public class Parallel
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00035D98 File Offset: 0x00034198
		private void Initialize()
		{
			Parallel.threadsCount = Environment.ProcessorCount;
			if (Parallel.threadsCount <= 1)
			{
				return;
			}
			this.jobAvailable = new AutoResetEvent[Parallel.threadsCount];
			this.threadIdle = new ManualResetEvent[Parallel.threadsCount];
			this.threads = new Thread[Parallel.threadsCount];
			for (int i = 0; i < Parallel.threadsCount; i++)
			{
				this.jobAvailable[i] = new AutoResetEvent(false);
				this.threadIdle[i] = new ManualResetEvent(true);
				this.threads[i] = new Thread(new ParameterizedThreadStart(this.WorkerThread));
				this.threads[i].IsBackground = false;
				this.threads[i].Start(i);
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00035E58 File Offset: 0x00034258
		public void Close()
		{
			this.loopBody = null;
			for (int i = 0; i < Parallel.threadsCount; i++)
			{
				this.jobAvailable[i].Set();
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00035E90 File Offset: 0x00034290
		public static void For(int start, int stop, Parallel.ForLoopBody loopBody)
		{
			Parallel.For(start, stop, 1, loopBody);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00035E9B File Offset: 0x0003429B
		public static void For(int start, int stop, int stepLength, Parallel.ForLoopBody loopBody)
		{
			Parallel.For(start, stop, stepLength, loopBody, true);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00035EA8 File Offset: 0x000342A8
		public static void For(int start, int stop, int stepLength, Parallel.ForLoopBody loopBody, bool close)
		{
			Parallel parallel = new Parallel();
			parallel.Initialize();
			parallel.ForLoop(start, stop, stepLength, loopBody, close);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00035ED0 File Offset: 0x000342D0
		public void ForLoop(int start, int stop, int stepLength, Parallel.ForLoopBody loopBody, bool close)
		{
			object obj = Parallel.sync;
			lock (obj)
			{
				stepLength = ((stepLength >= 1) ? stepLength : 1);
				this.currentIndex = start;
				this.stopIndex = stop;
				this.loopBody = loopBody;
				this.step = stepLength;
				if (Parallel.threadsCount <= 1)
				{
					for (int i = start; i < stop; i += stepLength)
					{
						loopBody(i);
					}
				}
				else
				{
					for (int j = 0; j < Parallel.threadsCount; j++)
					{
						this.threadIdle[j].Reset();
						this.jobAvailable[j].Set();
					}
					for (int k = 0; k < Parallel.threadsCount; k++)
					{
						this.threadIdle[k].WaitOne();
					}
					if (close)
					{
						this.Close();
					}
				}
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00035FC0 File Offset: 0x000343C0
		private void WorkerThread(object index)
		{
			int num = (int)index;
			for (;;)
			{
				this.jobAvailable[num].WaitOne();
				if (this.loopBody == null)
				{
					break;
				}
				bool flag;
				do
				{
					int num2 = Interlocked.Add(ref this.currentIndex, Parallel.iterationStepLength * this.step);
					int num3 = num2 - Parallel.iterationStepLength * this.step;
					flag = false;
					if (num2 >= this.stopIndex)
					{
						flag = true;
						num2 = this.stopIndex;
					}
					for (int i = num3; i < num2; i += this.step)
					{
						this.loopBody(i);
					}
				}
				while (!flag);
				this.threadIdle[num].Set();
			}
		}

		// Token: 0x040004AD RID: 1197
		private Parallel Instance;

		// Token: 0x040004AE RID: 1198
		private AutoResetEvent[] jobAvailable;

		// Token: 0x040004AF RID: 1199
		private ManualResetEvent[] threadIdle;

		// Token: 0x040004B0 RID: 1200
		private Thread[] threads;

		// Token: 0x040004B1 RID: 1201
		private static object sync = new object();

		// Token: 0x040004B2 RID: 1202
		private int currentIndex;

		// Token: 0x040004B3 RID: 1203
		private int stopIndex;

		// Token: 0x040004B4 RID: 1204
		private int step = 1;

		// Token: 0x040004B5 RID: 1205
		private Parallel.ForLoopBody loopBody;

		// Token: 0x040004B6 RID: 1206
		public static int threadsCount = Environment.ProcessorCount;

		// Token: 0x040004B7 RID: 1207
		public static int iterationStepLength = 10;

		// Token: 0x020000BD RID: 189
		// (Invoke) Token: 0x060005C2 RID: 1474
		public delegate void ForLoopBody(int i);
	}
}
