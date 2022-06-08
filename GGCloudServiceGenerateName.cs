using System;
using System.Text.RegularExpressions;

// Token: 0x0200047C RID: 1148
public class GGCloudServiceGenerateName
{
	// Token: 0x0600213E RID: 8510 RVA: 0x000F79BC File Offset: 0x000F5DBC
	public static string GenerateSurname()
	{
		string text = string.Empty;
		string[] array = "a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,i,i,i,o,o,o,u,y,ee,ee,ea,ea,ey,eau,eigh,oa,oo,ou,ough,ay".Split(new char[]
		{
			','
		});
		string[] array2 = "s,s,s,s,t,t,t,t,t,n,n,r,l,d,sm,sl,sh,sh,th,th,th".Split(new char[]
		{
			','
		});
		string[] array3 = "sh,sh,st,st,b,c,f,g,h,k,l,m,p,p,ph,wh".Split(new char[]
		{
			','
		});
		string[] array4 = "x,ss,ss,ch,ch,ck,ck,dd,kn,rt,gh,mm,nd,nd,nn,pp,ps,tt,ff,rr,rk,mp,ll".Split(new char[]
		{
			','
		});
		string[] array5 = "j,j,j,v,v,w,w,w,z,qu,qu".Split(new char[]
		{
			','
		});
		Random random = new Random(Guid.NewGuid().GetHashCode());
		int[] array6 = new int[]
		{
			2,
			2,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			4
		};
		int num = array6[random.Next(array6.Length)];
		for (int i = 0; i < num; i++)
		{
			int num2 = random.Next(1000);
			string[] array7;
			if (num2 < 775)
			{
				array7 = array2;
			}
			else if (num2 < 875 && i > 0)
			{
				array7 = array4;
			}
			else if (num2 < 985)
			{
				array7 = array3;
			}
			else
			{
				array7 = array5;
			}
			text += array7[random.Next(array7.Length)];
			text += array[random.Next(array.Length)];
			if (text.Length > 4 && random.Next(1000) < 800)
			{
				break;
			}
			if (text.Length > 6 && random.Next(1000) < 950)
			{
				break;
			}
			if (text.Length > 7)
			{
				break;
			}
		}
		int num3 = random.Next(1000);
		if (text.Length > 6)
		{
			num3 -= text.Length * 25;
		}
		else
		{
			num3 += text.Length * 10;
		}
		if (num3 >= 400)
		{
			if (num3 < 775)
			{
				text += array2[random.Next(array2.Length)];
			}
			else if (num3 < 825)
			{
				text += array3[random.Next(array3.Length)];
			}
			else if (num3 < 840)
			{
				text += "ski";
			}
			else if (num3 < 860)
			{
				text += "son";
			}
			else
			{
				if (Regex.IsMatch(text, "(.+)(ay|e|ee|ea|oo)$") || text.Length < 5)
				{
					return "Mc" + text.Substring(0, 1).ToUpper() + text.Substring(1);
				}
				text += "ez";
			}
		}
		return text.Substring(0, 1).ToUpper() + text.Substring(1);
	}
}
