using System;
using System.Diagnostics;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class PerformanceObscuredTests : MonoBehaviour
{
	// Token: 0x060018A0 RID: 6304 RVA: 0x000CD707 File Offset: 0x000CBB07
	private void Start()
	{
		base.Invoke("StartTests", 1f);
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x000CD71C File Offset: 0x000CBB1C
	private void StartTests()
	{
		UnityEngine.Debug.Log("--- OBSCURED TYPES PERFORMANCE TESTS ---\n");
		if (this.boolTest)
		{
			this.TestBool();
		}
		if (this.byteTest)
		{
			this.TestByte();
		}
		if (this.shortTest)
		{
			this.TestShort();
		}
		if (this.ushortTest)
		{
			this.TestUShort();
		}
		if (this.intTest)
		{
			this.TestInt();
		}
		if (this.uintTest)
		{
			this.TestUInt();
		}
		if (this.longTest)
		{
			this.TestLong();
		}
		if (this.floatTest)
		{
			this.TestFloat();
		}
		if (this.doubleTest)
		{
			this.TestDouble();
		}
		if (this.stringTest)
		{
			this.TestString();
		}
		if (this.vector3Test)
		{
			this.TestVector3();
		}
		if (this.prefsTest)
		{
			this.TestPrefs();
		}
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x000CD800 File Offset: 0x000CBC00
	private void TestBool()
	{
		UnityEngine.Debug.Log("  Testing ObscuredBool vs bool preformance:\n  " + this.boolIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredBool value = true;
		bool flag = value;
		bool flag2 = false;
		for (int i = 0; i < this.boolIterations; i++)
		{
			flag2 = value;
		}
		for (int j = 0; j < this.boolIterations; j++)
		{
			value = flag2;
		}
		UnityEngine.Debug.Log("    ObscuredBool:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.boolIterations; k++)
		{
			flag2 = flag;
		}
		for (int l = 0; l < this.boolIterations; l++)
		{
			flag = flag2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    bool:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (flag2)
		{
		}
		if (value)
		{
		}
		if (flag)
		{
		}
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x000CD928 File Offset: 0x000CBD28
	private void TestByte()
	{
		UnityEngine.Debug.Log("  Testing ObscuredByte vs byte preformance:\n  " + this.byteIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredByte value = 100;
		byte b = value;
		byte b2 = 0;
		for (int i = 0; i < this.byteIterations; i++)
		{
			b2 = value;
		}
		for (int j = 0; j < this.byteIterations; j++)
		{
			value = b2;
		}
		UnityEngine.Debug.Log("    ObscuredByte:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.byteIterations; k++)
		{
			b2 = b;
		}
		for (int l = 0; l < this.byteIterations; l++)
		{
			b = b2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    byte:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (b2 != 0)
		{
		}
		if (value != 0)
		{
		}
		if (b != 0)
		{
		}
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x000CDA50 File Offset: 0x000CBE50
	private void TestShort()
	{
		UnityEngine.Debug.Log("  Testing ObscuredShort vs short preformance:\n  " + this.shortIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredShort value = 100;
		short num = value;
		short num2 = 0;
		for (int i = 0; i < this.shortIterations; i++)
		{
			num2 = value;
		}
		for (int j = 0; j < this.shortIterations; j++)
		{
			value = num2;
		}
		UnityEngine.Debug.Log("    ObscuredShort:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.shortIterations; k++)
		{
			num2 = num;
		}
		for (int l = 0; l < this.shortIterations; l++)
		{
			num = num2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    short:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (num2 != 0)
		{
		}
		if (value != 0)
		{
		}
		if (num != 0)
		{
		}
	}

	// Token: 0x060018A5 RID: 6309 RVA: 0x000CDB78 File Offset: 0x000CBF78
	private void TestUShort()
	{
		UnityEngine.Debug.Log("  Testing ObscuredUShort vs ushort preformance:\n  " + this.ushortIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredUShort value = 100;
		ushort num = value;
		ushort num2 = 0;
		for (int i = 0; i < this.ushortIterations; i++)
		{
			num2 = value;
		}
		for (int j = 0; j < this.ushortIterations; j++)
		{
			value = num2;
		}
		UnityEngine.Debug.Log("    ObscuredUShort:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.ushortIterations; k++)
		{
			num2 = num;
		}
		for (int l = 0; l < this.ushortIterations; l++)
		{
			num = num2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    ushort:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (num2 != 0)
		{
		}
		if (value != 0)
		{
		}
		if (num != 0)
		{
		}
	}

	// Token: 0x060018A6 RID: 6310 RVA: 0x000CDCA0 File Offset: 0x000CC0A0
	private void TestDouble()
	{
		UnityEngine.Debug.Log("  Testing ObscuredDouble vs double preformance:\n  " + this.doubleIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredDouble value = 100.0;
		double num = value;
		double num2 = 0.0;
		for (int i = 0; i < this.doubleIterations; i++)
		{
			num2 = value;
		}
		for (int j = 0; j < this.doubleIterations; j++)
		{
			value = num2;
		}
		UnityEngine.Debug.Log("    ObscuredDouble:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.doubleIterations; k++)
		{
			num2 = num;
		}
		for (int l = 0; l < this.doubleIterations; l++)
		{
			num = num2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    double:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (num2 != 0.0)
		{
		}
		if (value != 0.0)
		{
		}
		if (num != 0.0)
		{
		}
	}

	// Token: 0x060018A7 RID: 6311 RVA: 0x000CDDF4 File Offset: 0x000CC1F4
	private void TestFloat()
	{
		UnityEngine.Debug.Log("  Testing ObscuredFloat vs float preformance:\n  " + this.floatIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredFloat value = 100f;
		float num = value;
		float num2 = 0f;
		for (int i = 0; i < this.floatIterations; i++)
		{
			num2 = value;
		}
		for (int j = 0; j < this.floatIterations; j++)
		{
			value = num2;
		}
		UnityEngine.Debug.Log("    ObscuredFloat:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.floatIterations; k++)
		{
			num2 = num;
		}
		for (int l = 0; l < this.floatIterations; l++)
		{
			num = num2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    float:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (num2 != 0f)
		{
		}
		if (value != 0f)
		{
		}
		if (num != 0f)
		{
		}
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x000CDF34 File Offset: 0x000CC334
	private void TestInt()
	{
		UnityEngine.Debug.Log("  Testing ObscuredInt vs int preformance:\n  " + this.intIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredInt value = 100;
		int num = value;
		int num2 = 0;
		for (int i = 0; i < this.intIterations; i++)
		{
			num2 = value;
		}
		for (int j = 0; j < this.intIterations; j++)
		{
			value = num2;
		}
		UnityEngine.Debug.Log("    ObscuredInt:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.intIterations; k++)
		{
			num2 = num;
		}
		for (int l = 0; l < this.intIterations; l++)
		{
			num = num2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    int:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (num2 != 0)
		{
		}
		if (value != 0)
		{
		}
		if (num != 0)
		{
		}
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x000CE05C File Offset: 0x000CC45C
	private void TestLong()
	{
		UnityEngine.Debug.Log("  Testing ObscuredLong vs long preformance:\n  " + this.longIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredLong value = 100L;
		long num = value;
		long num2 = 0L;
		for (int i = 0; i < this.longIterations; i++)
		{
			num2 = value;
		}
		for (int j = 0; j < this.longIterations; j++)
		{
			value = num2;
		}
		UnityEngine.Debug.Log("    ObscuredLong:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.longIterations; k++)
		{
			num2 = num;
		}
		for (int l = 0; l < this.longIterations; l++)
		{
			num = num2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    long:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (num2 != 0L)
		{
		}
		if (value != 0L)
		{
		}
		if (num != 0L)
		{
		}
	}

	// Token: 0x060018AA RID: 6314 RVA: 0x000CE18C File Offset: 0x000CC58C
	private void TestString()
	{
		UnityEngine.Debug.Log("  Testing ObscuredString vs string preformance:\n  " + this.stringIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredString obscuredString = "abcd";
		string text = obscuredString;
		string text2 = string.Empty;
		for (int i = 0; i < this.stringIterations; i++)
		{
			text2 = obscuredString;
		}
		for (int j = 0; j < this.stringIterations; j++)
		{
			obscuredString = text2;
		}
		UnityEngine.Debug.Log("    ObscuredString:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.stringIterations; k++)
		{
			text2 = text;
		}
		for (int l = 0; l < this.stringIterations; l++)
		{
			text = text2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    string:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (text2 != string.Empty)
		{
		}
		if (obscuredString != string.Empty)
		{
		}
		if (text != string.Empty)
		{
		}
	}

	// Token: 0x060018AB RID: 6315 RVA: 0x000CE2DC File Offset: 0x000CC6DC
	private void TestUInt()
	{
		UnityEngine.Debug.Log("  Testing ObscuredUInt vs uint preformance:\n  " + this.uintIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredUInt value = 100U;
		uint num = value;
		uint num2 = 0U;
		for (int i = 0; i < this.uintIterations; i++)
		{
			num2 = value;
		}
		for (int j = 0; j < this.uintIterations; j++)
		{
			value = num2;
		}
		UnityEngine.Debug.Log("    ObscuredUInt:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.uintIterations; k++)
		{
			num2 = num;
		}
		for (int l = 0; l < this.uintIterations; l++)
		{
			num = num2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    uint:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (num2 != 0U)
		{
		}
		if (value != 0U)
		{
		}
		if (num != 0U)
		{
		}
	}

	// Token: 0x060018AC RID: 6316 RVA: 0x000CE404 File Offset: 0x000CC804
	private void TestVector3()
	{
		UnityEngine.Debug.Log("  Testing ObscuredVector3 vs Vector3 preformance:\n  " + this.vector3Iterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		ObscuredVector3 value = new Vector3(1f, 2f, 3f);
		Vector3 vector = value;
		Vector3 vector2 = new Vector3(0f, 0f, 0f);
		for (int i = 0; i < this.vector3Iterations; i++)
		{
			vector2 = value;
		}
		for (int j = 0; j < this.vector3Iterations; j++)
		{
			value = vector2;
		}
		UnityEngine.Debug.Log("    ObscuredVector3:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.vector3Iterations; k++)
		{
			vector2 = vector;
		}
		for (int l = 0; l < this.vector3Iterations; l++)
		{
			vector = vector2;
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    Vector3:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		if (vector2 != Vector3.zero)
		{
		}
		if (value != Vector3.zero)
		{
		}
		if (vector != Vector3.zero)
		{
		}
	}

	// Token: 0x060018AD RID: 6317 RVA: 0x000CE570 File Offset: 0x000CC970
	private void TestPrefs()
	{
		UnityEngine.Debug.Log("  Testing ObscuredPrefs vs PlayerPrefs preformance:\n  " + this.prefsIterations + " iterations for read and same for write");
		Stopwatch stopwatch = Stopwatch.StartNew();
		for (int i = 0; i < this.prefsIterations; i++)
		{
			ObscuredPrefs.SetInt("__a", 1);
			ObscuredPrefs.SetFloat("__b", 2f);
			ObscuredPrefs.SetString("__c", "3");
		}
		for (int j = 0; j < this.prefsIterations; j++)
		{
			ObscuredPrefs.GetInt("__a", 1);
			ObscuredPrefs.GetFloat("__b", 2f);
			ObscuredPrefs.GetString("__c", "3");
		}
		UnityEngine.Debug.Log("    ObscuredPrefs:\n    " + stopwatch.ElapsedMilliseconds + " ms ");
		stopwatch.Reset();
		stopwatch.Start();
		for (int k = 0; k < this.prefsIterations; k++)
		{
			PlayerPrefs.SetInt("__a", 1);
			PlayerPrefs.SetFloat("__b", 2f);
			PlayerPrefs.SetString("__c", "3");
		}
		for (int l = 0; l < this.prefsIterations; l++)
		{
			PlayerPrefs.GetInt("__a", 1);
			PlayerPrefs.GetFloat("__b", 2f);
			PlayerPrefs.GetString("__c", "3");
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log("    PlayerPrefs:\n    " + stopwatch.ElapsedMilliseconds + "  ms ");
		PlayerPrefs.DeleteKey("__a");
		PlayerPrefs.DeleteKey("__b");
		PlayerPrefs.DeleteKey("__c");
	}

	// Token: 0x04001BBB RID: 7099
	public bool boolTest = true;

	// Token: 0x04001BBC RID: 7100
	public int boolIterations = 2500000;

	// Token: 0x04001BBD RID: 7101
	public bool byteTest = true;

	// Token: 0x04001BBE RID: 7102
	public int byteIterations = 2500000;

	// Token: 0x04001BBF RID: 7103
	public bool shortTest = true;

	// Token: 0x04001BC0 RID: 7104
	public int shortIterations = 2500000;

	// Token: 0x04001BC1 RID: 7105
	public bool ushortTest = true;

	// Token: 0x04001BC2 RID: 7106
	public int ushortIterations = 2500000;

	// Token: 0x04001BC3 RID: 7107
	public bool intTest = true;

	// Token: 0x04001BC4 RID: 7108
	public int intIterations = 2500000;

	// Token: 0x04001BC5 RID: 7109
	public bool uintTest = true;

	// Token: 0x04001BC6 RID: 7110
	public int uintIterations = 2500000;

	// Token: 0x04001BC7 RID: 7111
	public bool longTest = true;

	// Token: 0x04001BC8 RID: 7112
	public int longIterations = 2500000;

	// Token: 0x04001BC9 RID: 7113
	public bool floatTest = true;

	// Token: 0x04001BCA RID: 7114
	public int floatIterations = 2500000;

	// Token: 0x04001BCB RID: 7115
	public bool doubleTest = true;

	// Token: 0x04001BCC RID: 7116
	public int doubleIterations = 2500000;

	// Token: 0x04001BCD RID: 7117
	public bool stringTest = true;

	// Token: 0x04001BCE RID: 7118
	public int stringIterations = 250000;

	// Token: 0x04001BCF RID: 7119
	public bool vector3Test = true;

	// Token: 0x04001BD0 RID: 7120
	public int vector3Iterations = 2500000;

	// Token: 0x04001BD1 RID: 7121
	public bool prefsTest = true;

	// Token: 0x04001BD2 RID: 7122
	public int prefsIterations = 2500;
}
