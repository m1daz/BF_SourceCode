using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
public class BetterList<T>
{
	// Token: 0x06002857 RID: 10327 RVA: 0x00129CBC File Offset: 0x001280BC
	public IEnumerator<T> GetEnumerator()
	{
		if (this.buffer != null)
		{
			for (int i = 0; i < this.size; i++)
			{
				yield return this.buffer[i];
			}
		}
		yield break;
	}

	// Token: 0x17000228 RID: 552
	[DebuggerHidden]
	public T this[int i]
	{
		get
		{
			return this.buffer[i];
		}
		set
		{
			this.buffer[i] = value;
		}
	}

	// Token: 0x0600285A RID: 10330 RVA: 0x00129CF4 File Offset: 0x001280F4
	private void AllocateMore()
	{
		T[] array = (this.buffer == null) ? new T[32] : new T[Mathf.Max(this.buffer.Length << 1, 32)];
		if (this.buffer != null && this.size > 0)
		{
			this.buffer.CopyTo(array, 0);
		}
		this.buffer = array;
	}

	// Token: 0x0600285B RID: 10331 RVA: 0x00129D5C File Offset: 0x0012815C
	private void Trim()
	{
		if (this.size > 0)
		{
			if (this.size < this.buffer.Length)
			{
				T[] array = new T[this.size];
				for (int i = 0; i < this.size; i++)
				{
					array[i] = this.buffer[i];
				}
				this.buffer = array;
			}
		}
		else
		{
			this.buffer = null;
		}
	}

	// Token: 0x0600285C RID: 10332 RVA: 0x00129DD1 File Offset: 0x001281D1
	public void Clear()
	{
		this.size = 0;
	}

	// Token: 0x0600285D RID: 10333 RVA: 0x00129DDA File Offset: 0x001281DA
	public void Release()
	{
		this.size = 0;
		this.buffer = null;
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x00129DEC File Offset: 0x001281EC
	public void Add(T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		this.buffer[this.size++] = item;
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x00129E3C File Offset: 0x0012823C
	public void Insert(int index, T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		if (index > -1 && index < this.size)
		{
			for (int i = this.size; i > index; i--)
			{
				this.buffer[i] = this.buffer[i - 1];
			}
			this.buffer[index] = item;
			this.size++;
		}
		else
		{
			this.Add(item);
		}
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x00129ED8 File Offset: 0x001282D8
	public bool Contains(T item)
	{
		if (this.buffer == null)
		{
			return false;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x00129F30 File Offset: 0x00128330
	public int IndexOf(T item)
	{
		if (this.buffer == null)
		{
			return -1;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x00129F88 File Offset: 0x00128388
	public bool Remove(T item)
	{
		if (this.buffer != null)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.size; i++)
			{
				if (@default.Equals(this.buffer[i], item))
				{
					this.size--;
					this.buffer[i] = default(T);
					for (int j = i; j < this.size; j++)
					{
						this.buffer[j] = this.buffer[j + 1];
					}
					this.buffer[this.size] = default(T);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002863 RID: 10339 RVA: 0x0012A048 File Offset: 0x00128448
	public void RemoveAt(int index)
	{
		if (this.buffer != null && index > -1 && index < this.size)
		{
			this.size--;
			this.buffer[index] = default(T);
			for (int i = index; i < this.size; i++)
			{
				this.buffer[i] = this.buffer[i + 1];
			}
			this.buffer[this.size] = default(T);
		}
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x0012A0E4 File Offset: 0x001284E4
	public T Pop()
	{
		if (this.buffer != null && this.size != 0)
		{
			T result = this.buffer[--this.size];
			this.buffer[this.size] = default(T);
			return result;
		}
		return default(T);
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x0012A149 File Offset: 0x00128549
	public T[] ToArray()
	{
		this.Trim();
		return this.buffer;
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x0012A158 File Offset: 0x00128558
	[DebuggerHidden]
	[DebuggerStepThrough]
	public void Sort(BetterList<T>.CompareFunc comparer)
	{
		int num = 0;
		int num2 = this.size - 1;
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = num; i < num2; i++)
			{
				if (comparer(this.buffer[i], this.buffer[i + 1]) > 0)
				{
					T t = this.buffer[i];
					this.buffer[i] = this.buffer[i + 1];
					this.buffer[i + 1] = t;
					flag = true;
				}
				else if (!flag)
				{
					num = ((i != 0) ? (i - 1) : 0);
				}
			}
		}
	}

	// Token: 0x04002944 RID: 10564
	public T[] buffer;

	// Token: 0x04002945 RID: 10565
	public int size;

	// Token: 0x020005A8 RID: 1448
	// (Invoke) Token: 0x06002868 RID: 10344
	public delegate int CompareFunc(T left, T right);
}
