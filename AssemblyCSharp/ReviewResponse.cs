using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.review;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004CF RID: 1231
	public class ReviewResponse : App42CallBack
	{
		// Token: 0x06002281 RID: 8833 RVA: 0x000FFE58 File Offset: 0x000FE258
		public void OnSuccess(object response)
		{
			this.result = response.ToString();
			if (response is Review)
			{
				Review review = (Review)response;
				Debug.Log("GetItemId  : " + review.GetItemId());
				Debug.Log("GetRating  : " + review.GetRating());
				Debug.Log("GetReviewId  : " + review.GetReviewId());
				Debug.Log("GetStatus  : " + review.GetStatus());
				Debug.Log("GetStrResponse  : " + review.GetStrResponse());
				Debug.Log("GetComment  : " + review.GetComment());
				Debug.Log("GetCreatedOn  : " + review.GetCreatedOn());
				Debug.Log("GetUserId  : " + review.GetUserId());
			}
			else
			{
				IList<Review> list = (IList<Review>)response;
				for (int i = 0; i < list.Count; i++)
				{
					Debug.Log("GetItemId  : " + list[i].GetItemId());
					Debug.Log("GetRating  : " + list[i].GetRating());
					Debug.Log("GetReviewId  : " + list[i].GetReviewId());
					Debug.Log("GetStatus  : " + list[i].GetStatus());
					Debug.Log("GetStrResponse  : " + list[i].GetStrResponse());
					Debug.Log("GetComment  : " + list[i].GetComment());
					Debug.Log("GetCreatedOn  : " + list[i].GetCreatedOn());
					Debug.Log("GetUserId  : " + list[i].GetUserId());
				}
			}
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0010003A File Offset: 0x000FE43A
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception : " + e);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00100058 File Offset: 0x000FE458
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x04002338 RID: 9016
		private string result = string.Empty;
	}
}
