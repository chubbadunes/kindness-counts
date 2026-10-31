using UnityEngine;
using UnityEngine.Purchasing;

public class StoreManager : MonoBehaviour, IStoreListener
{
    public const string RainbowPackId = "kindness_rainbow_pack";

    private static IStoreController controller;
    private static IExtensionProvider extensions;
    private GameManager gm;

    public void Init(GameManager manager)
    {
        gm = manager;
        if (controller != null)
            return;
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(RainbowPackId, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized
    {
        get { return controller != null; }
    }

    public void BuyRainbowPack()
    {
        if (controller == null)
            return;
        controller.InitiatePurchase(RainbowPackId);
    }

    public void RestorePurchases()
    {
        if (extensions == null)
            return;
        var apple = extensions.GetExtension<IAppleExtensions>();
        if (apple != null)
            apple.RestoreTransactions(result => { });
    }

    public void OnInitialized(IStoreController storeController, IExtensionProvider storeExtensions)
    {
        controller = storeController;
        extensions = storeExtensions;
        if (gm != null && gm.UI != null)
            gm.UI.RefreshBuyButton();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogWarning("IAP init failed: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogWarning("IAP init failed: " + error + " - " + message);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == RainbowPackId && gm != null)
            gm.OnRainbowPackPurchased();
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.LogWarning("Purchase failed: " + product.definition.id + " - " + reason);
    }
}
