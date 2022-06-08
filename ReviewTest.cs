using System;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.review;
using UnityEngine;

// Token: 0x020004D0 RID: 1232
public class ReviewTest : MonoBehaviour
{
	// Token: 0x06002285 RID: 8837 RVA: 0x0010007E File Offset: 0x000FE47E
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x001000A1 File Offset: 0x000FE4A1
	private void Update()
	{
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x001000A4 File Offset: 0x000FE4A4
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Create Review"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.CreateReview(this.cons.userName, this.cons.itemId, "Awusume", 3.0, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "Get Review ByItem"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetReviewsByItem(this.cons.itemId, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "Get Highest Review ByItem"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetHighestReviewByItem(this.cons.itemId, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "Get Lowest Review ByItem"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetLowestReviewByItem(this.cons.itemId, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "Get Average Review ByItem"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetAverageReviewByItem(this.cons.itemId, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "Get All Reviews"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetAllReviews(this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "Get All Reviews Count"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetAllReviewsCount(this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "Add Comment"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.AddComment(this.cons.userName, this.cons.itemId, "Awsum app", this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "Get Comments ByItem"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetCommentsByItem(this.cons.itemId, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "Get Reviews Count ByItem"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetReviewsCountByItem(this.cons.itemId, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 250f, 30f), "GetReviews Count ByItem Rating"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetReviewsCountByItemAndRating(this.cons.itemId, (double)this.cons.rating, this.callBack);
		}
		if (GUI.Button(new Rect(310f, 300f, 100f, 30f), "Mute"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.Mute(this.cons.reviewId, this.callBack);
		}
		if (GUI.Button(new Rect(420f, 300f, 100f, 30f), "Unmute"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.Unmute(this.cons.reviewId, this.callBack);
		}
		if (GUI.Button(new Rect(530f, 300f, 200f, 30f), "Get All Reviews With Paging"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetAllReviews(this.cons.max, this.cons.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(740f, 300f, 180f, 30f), "Get Reviews ByItem"))
		{
			this.reviewService = this.sp.BuildReviewService();
			this.reviewService.GetReviewsByItem(this.cons.itemId, this.cons.max, this.cons.offSet, this.callBack);
		}
	}

	// Token: 0x04002339 RID: 9017
	private Constant cons = new Constant();

	// Token: 0x0400233A RID: 9018
	private ServiceAPI sp;

	// Token: 0x0400233B RID: 9019
	private ReviewService reviewService;

	// Token: 0x0400233C RID: 9020
	public string success;

	// Token: 0x0400233D RID: 9021
	public string box;

	// Token: 0x0400233E RID: 9022
	private ReviewResponse callBack = new ReviewResponse();
}
