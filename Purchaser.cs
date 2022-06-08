using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

// Token: 0x0200027E RID: 638
public class Purchaser : MonoBehaviour, IStoreListener
{
	// Token: 0x06001226 RID: 4646 RVA: 0x000A3DAD File Offset: 0x000A21AD
	private void OnDestroy()
	{
		if (Purchaser.mInstance == this)
		{
			Purchaser.mInstance = null;
		}
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x000A3DC8 File Offset: 0x000A21C8
	private void Awake()
	{
		Purchaser.mInstance = this;
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		configurationBuilder.AddProduct("coinspack5000", ProductType.Consumable, new IDs
		{
			{
				"com.gog.coinspack5000",
				new string[]
				{
					"AppleAppStore"
				}
			},
			{
				"com.gog.coinspack5000",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"com.gog.coinspack5000",
				new string[]
				{
					"AmazonApps"
				}
			}
		});
		configurationBuilder.AddProduct("gemspack15", ProductType.Consumable, new IDs
		{
			{
				"com.gog.gemspack15",
				new string[]
				{
					"AppleAppStore"
				}
			},
			{
				"com.gog.gemspack15",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"com.gog.gemspack15",
				new string[]
				{
					"AmazonApps"
				}
			}
		});
		configurationBuilder.AddProduct("gemspack30", ProductType.Consumable, new IDs
		{
			{
				"com.gog.gemspack30",
				new string[]
				{
					"AppleAppStore"
				}
			},
			{
				"com.gog.gemspack30",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"com.gog.gemspack30",
				new string[]
				{
					"AmazonApps"
				}
			}
		});
		configurationBuilder.AddProduct("gemspack80", ProductType.Consumable, new IDs
		{
			{
				"com.gog.gemspack80",
				new string[]
				{
					"AppleAppStore"
				}
			},
			{
				"com.gog.gemspack80",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"com.gog.gemspack80",
				new string[]
				{
					"AmazonApps"
				}
			}
		});
		configurationBuilder.AddProduct("gemspack165", ProductType.Consumable, new IDs
		{
			{
				"com.gog.gemspack165",
				new string[]
				{
					"AppleAppStore"
				}
			},
			{
				"com.gog.gemspack165",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"com.gog.gemspack165",
				new string[]
				{
					"AmazonApps"
				}
			}
		});
		configurationBuilder.AddProduct("gemspack335", ProductType.Consumable, new IDs
		{
			{
				"com.gog.gemspack335",
				new string[]
				{
					"AppleAppStore"
				}
			},
			{
				"com.gog.gemspack335",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"com.gog.gemspack335",
				new string[]
				{
					"AmazonApps"
				}
			}
		});
		configurationBuilder.AddProduct("gemspack850", ProductType.Consumable, new IDs
		{
			{
				"com.gog.gemspack850",
				new string[]
				{
					"AppleAppStore"
				}
			},
			{
				"com.gog.gemspack850",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"com.gog.gemspack850",
				new string[]
				{
					"AmazonApps"
				}
			}
		});
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x000A408C File Offset: 0x000A248C
	public void OnPurchaseFailed(Product item, PurchaseFailureReason r)
	{
		Debug.Log("Purchase failed: " + item.definition.id);
		Debug.Log(r);
		if (UINewStoreGemAndCoinPurchaseWindowDirector.mInstance != null)
		{
			UINewStoreGemAndCoinPurchaseWindowDirector.mInstance.PurchaseFail();
		}
		if (UIGameStoreDirector.mInstance != null)
		{
			UIGameStoreDirector.mInstance.PurchaseFail();
		}
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x000A40F4 File Offset: 0x000A24F4
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		Debug.Log("Purchase OK: " + e.purchasedProduct.definition.id);
		Debug.Log("Receipt: " + e.purchasedProduct.receipt);
		float num = 1f;
		if (UIUserDataController.GetOffsaleTimeSpan().TotalSeconds > 0.0)
		{
			num = UIUserDataController.GetOffsaleFactor();
		}
		string id = e.purchasedProduct.definition.id;
		switch (id)
		{
		case "coinspack5000":
			GrowthManagerKit.AddCoins((int)(5000f * num));
			break;
		case "gemspack15":
			GrowthManagerKit.AddGems((int)(15f * num));
			this.RechargeRewardRecord((int)(15f * num));
			break;
		case "gemspack30":
			GrowthManagerKit.AddGems((int)(30f * num));
			this.RechargeRewardRecord((int)(30f * num));
			break;
		case "gemspack80":
			GrowthManagerKit.AddGems((int)(80f * num));
			this.RechargeRewardRecord((int)(80f * num));
			break;
		case "gemspack165":
			GrowthManagerKit.AddGems((int)(165f * num));
			this.RechargeRewardRecord((int)(165f * num));
			break;
		case "gemspack335":
			GrowthManagerKit.AddGems((int)(335f * num));
			this.RechargeRewardRecord((int)(335f * num));
			break;
		case "gemspack850":
			GrowthManagerKit.AddGems((int)(850f * num));
			this.RechargeRewardRecord((int)(850f * num));
			break;
		}
		if (UINewStoreGemAndCoinPurchaseWindowDirector.mInstance != null)
		{
			UINewStoreGemAndCoinPurchaseWindowDirector.mInstance.PurchaseSuccess();
		}
		if (UIGameStoreDirector.mInstance != null)
		{
			UIGameStoreDirector.mInstance.PurchaseSuccess();
		}
		GOGPlayerPrefabs.Save();
		return PurchaseProcessingResult.Complete;
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x000A4332 File Offset: 0x000A2732
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.m_Controller = controller;
		this.m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x000A4347 File Offset: 0x000A2747
	public void OnPurchaseClicked(string productId)
	{
		this.m_Controller.InitiatePurchase(productId);
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000A4355 File Offset: 0x000A2755
	private void OnTransactionsRestored(bool success)
	{
		Debug.Log("Transactions restored.");
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x000A4361 File Offset: 0x000A2761
	public void RestoredEvent()
	{
		this.m_AppleExtensions.RestoreTransactions(new Action<bool>(this.OnTransactionsRestored));
		Debug.Log("Restore");
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x000A4384 File Offset: 0x000A2784
	private void OnDeferred(Product item)
	{
		Debug.Log("Purchase deferred: " + item.definition.id);
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x000A43A0 File Offset: 0x000A27A0
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Billing failed to initialize!");
		if (error != InitializationFailureReason.AppNotKnown)
		{
			if (error != InitializationFailureReason.PurchasingUnavailable)
			{
				if (error == InitializationFailureReason.NoProductsAvailable)
				{
					Debug.Log("No products available for purchase!");
				}
			}
			else
			{
				Debug.Log("Billing disabled!");
			}
		}
		else
		{
			Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
		}
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x000A43FD File Offset: 0x000A27FD
	public void BuyItem(string e)
	{
		this.OnPurchaseClicked(e);
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x000A4406 File Offset: 0x000A2806
	private void OnClick()
	{
		this.OnPurchaseClicked("com.cnrjb.coins1000");
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x000A4413 File Offset: 0x000A2813
	private void RechargeRewardRecord(int gemNum)
	{
		GrowthManagerKit.AddHolidayRechargeRecord(gemNum);
	}

	// Token: 0x040014FE RID: 5374
	public static Purchaser mInstance;

	// Token: 0x040014FF RID: 5375
	private IStoreController m_Controller;

	// Token: 0x04001500 RID: 5376
	private IAppleExtensions m_AppleExtensions;
}
