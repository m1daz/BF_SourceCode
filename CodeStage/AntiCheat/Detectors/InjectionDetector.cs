using System;
using System.IO;
using System.Reflection;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

namespace CodeStage.AntiCheat.Detectors
{
	// Token: 0x0200031D RID: 797
	[AddComponentMenu("")]
	public class InjectionDetector : MonoBehaviour
	{
		// Token: 0x060018AE RID: 6318 RVA: 0x000CE71A File Offset: 0x000CCB1A
		private InjectionDetector()
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x000CE730 File Offset: 0x000CCB30
		public static InjectionDetector Instance
		{
			get
			{
				if (InjectionDetector.instance == null)
				{
					InjectionDetector injectionDetector = (InjectionDetector)UnityEngine.Object.FindObjectOfType(typeof(InjectionDetector));
					if (injectionDetector == null)
					{
						GameObject gameObject = new GameObject("Injection Detector");
						injectionDetector = gameObject.AddComponent<InjectionDetector>();
					}
					return injectionDetector;
				}
				return InjectionDetector.instance;
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000CE787 File Offset: 0x000CCB87
		public static void Dispose()
		{
			InjectionDetector.Instance.DisposeInternal();
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000CE793 File Offset: 0x000CCB93
		private void DisposeInternal()
		{
			this.StopMonitoringInternal();
			InjectionDetector.instance = null;
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000CE7AC File Offset: 0x000CCBAC
		private void Awake()
		{
			if (InjectionDetector.instance != null)
			{
				Debug.LogWarning("[ACT] Only one Injection Detector instance allowed!");
				UnityEngine.Object.Destroy(this);
				return;
			}
			if (!this.IsPlacedCorrectly())
			{
				Debug.LogWarning("[ACT] Injection Detector placed in scene incorrectly and will be auto-destroyed! Please, use \"GameObject->Create Other->Code Stage->Anti-Cheat Toolkit->Injection Detector\" menu to correct this!");
				UnityEngine.Object.Destroy(this);
				return;
			}
			InjectionDetector.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000CE807 File Offset: 0x000CCC07
		private bool IsPlacedCorrectly()
		{
			return base.name == "Injection Detector" && base.GetComponentsInChildren<Component>().Length == 2 && base.transform.childCount == 0;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000CE83D File Offset: 0x000CCC3D
		private void OnLevelWasLoaded(int index)
		{
			if (!this.keepAlive)
			{
				InjectionDetector.Dispose();
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000CE84F File Offset: 0x000CCC4F
		private void OnDisable()
		{
			this.StopMonitoringInternal();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000CE857 File Offset: 0x000CCC57
		private void OnApplicationQuit()
		{
			this.DisposeInternal();
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000CE85F File Offset: 0x000CCC5F
		public static void StartDetection(Action callback)
		{
			if (InjectionDetector.Instance.running)
			{
				Debug.LogWarning("[ACT] Injection Detector already running!");
				return;
			}
			InjectionDetector.Instance.StartDetectionInternal(callback);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000CE888 File Offset: 0x000CCC88
		private void StartDetectionInternal(Action callback)
		{
			this.onInjectionDetected = callback;
			if (this.allowedAssemblies == null)
			{
				this.LoadAndParseAllowedAssemblies();
			}
			if (this.signaturesAreNotGenuine)
			{
				this.InjectionDetected();
				return;
			}
			if (!this.FindInjectionInCurrentAssemblies())
			{
				AppDomain.CurrentDomain.AssemblyLoad += this.OnNewAssemblyLoaded;
				this.running = true;
			}
			else
			{
				this.InjectionDetected();
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000CE8F2 File Offset: 0x000CCCF2
		public static void StopMonitoring()
		{
			InjectionDetector.Instance.StopMonitoringInternal();
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000CE8FE File Offset: 0x000CCCFE
		private void StopMonitoringInternal()
		{
			if (this.running)
			{
				this.onInjectionDetected = null;
				AppDomain.CurrentDomain.AssemblyLoad -= this.OnNewAssemblyLoaded;
				this.running = false;
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000CE92F File Offset: 0x000CCD2F
		private void InjectionDetected()
		{
			if (this.onInjectionDetected != null)
			{
				this.onInjectionDetected();
			}
			if (this.autoDispose)
			{
				InjectionDetector.Dispose();
			}
			else
			{
				this.StopMonitoringInternal();
			}
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000CE962 File Offset: 0x000CCD62
		private void OnNewAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
		{
			if (!this.AssemblyAllowed(args.LoadedAssembly))
			{
				this.InjectionDetected();
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000CE97C File Offset: 0x000CCD7C
		private bool FindInjectionInCurrentAssemblies()
		{
			bool result = false;
			foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (!this.AssemblyAllowed(ass))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000CE9C4 File Offset: 0x000CCDC4
		private bool AssemblyAllowed(Assembly ass)
		{
			string name = ass.GetName().Name;
			int assemblyHash = this.GetAssemblyHash(ass);
			bool result = false;
			for (int i = 0; i < this.allowedAssemblies.Length; i++)
			{
				InjectionDetector.AllowedAssembly allowedAssembly = this.allowedAssemblies[i];
				if (allowedAssembly.name == name && Array.IndexOf<int>(allowedAssembly.hashes, assemblyHash) != -1)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000CEA38 File Offset: 0x000CCE38
		private void LoadAndParseAllowedAssemblies()
		{
			TextAsset textAsset = (TextAsset)Resources.Load("fndid", typeof(TextAsset));
			if (textAsset == null)
			{
				this.signaturesAreNotGenuine = true;
				return;
			}
			string[] separator = new string[]
			{
				":"
			};
			MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			this.allowedAssemblies = new InjectionDetector.AllowedAssembly[num];
			for (int i = 0; i < num; i++)
			{
				string text = binaryReader.ReadString();
				text = ObscuredString.EncryptDecrypt(text, "Elina");
				string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				int num2 = array.Length;
				if (num2 <= 1)
				{
					this.signaturesAreNotGenuine = true;
					binaryReader.Close();
					memoryStream.Close();
					return;
				}
				string name = array[0];
				int[] array2 = new int[num2 - 1];
				for (int j = 1; j < num2; j++)
				{
					array2[j - 1] = int.Parse(array[j]);
				}
				this.allowedAssemblies[i] = new InjectionDetector.AllowedAssembly(name, array2);
			}
			binaryReader.Close();
			memoryStream.Close();
			Resources.UnloadAsset(textAsset);
			this.hexTable = new string[256];
			for (int k = 0; k < 256; k++)
			{
				this.hexTable[k] = k.ToString("x2");
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000CEBA8 File Offset: 0x000CCFA8
		private int GetAssemblyHash(Assembly ass)
		{
			AssemblyName name = ass.GetName();
			byte[] publicKeyToken = name.GetPublicKeyToken();
			string text;
			if (publicKeyToken.Length == 8)
			{
				text = name.Name + this.PublicKeyTokenToString(publicKeyToken);
			}
			else
			{
				text = name.Name;
			}
			int num = 0;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				num += (int)text[i];
				num += num << 10;
				num ^= num >> 6;
			}
			num += num << 3;
			num ^= num >> 11;
			return num + (num << 15);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x000CEC3C File Offset: 0x000CD03C
		private string PublicKeyTokenToString(byte[] bytes)
		{
			string text = string.Empty;
			for (int i = 0; i < 8; i++)
			{
				text += this.hexTable[(int)bytes[i]];
			}
			return text;
		}

		// Token: 0x04001BD3 RID: 7123
		public bool autoDispose = true;

		// Token: 0x04001BD4 RID: 7124
		public bool keepAlive = true;

		// Token: 0x04001BD5 RID: 7125
		public Action onInjectionDetected;

		// Token: 0x04001BD6 RID: 7126
		private const string COMPONENT_NAME = "Injection Detector";

		// Token: 0x04001BD7 RID: 7127
		private static InjectionDetector instance;

		// Token: 0x04001BD8 RID: 7128
		private bool running;

		// Token: 0x04001BD9 RID: 7129
		private bool signaturesAreNotGenuine;

		// Token: 0x04001BDA RID: 7130
		private InjectionDetector.AllowedAssembly[] allowedAssemblies;

		// Token: 0x04001BDB RID: 7131
		private string[] hexTable;

		// Token: 0x0200031E RID: 798
		private class AllowedAssembly
		{
			// Token: 0x060018C2 RID: 6338 RVA: 0x000CEC73 File Offset: 0x000CD073
			public AllowedAssembly(string name, int[] hashes)
			{
				this.name = name;
				this.hashes = hashes;
			}

			// Token: 0x04001BDC RID: 7132
			public readonly string name;

			// Token: 0x04001BDD RID: 7133
			public readonly int[] hashes;
		}
	}
}
