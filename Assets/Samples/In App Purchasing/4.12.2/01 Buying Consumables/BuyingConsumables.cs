#if UNITY_ANDROID
using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

namespace Samples.Purchasing.Core.BuyingConsumables
{
    public class BuyingConsumables : MonoBehaviour, IDetailedStoreListener
    {
        public static BuyingConsumables Instance { get; private set; }

        IStoreController m_StoreController; // The Unity Purchasing system.

        //Your products IDs. They should match the ids of your products in your store.
        public string[] consumableProductID;
        public string nonConsumableProductID;

        public int[] consumableRewardedStars;


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(Instance.gameObject);
            }
        }

        void Start()
        {
            InitializePurchasing();
        }

        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //Add products that will be purchasable and indicate its type.

            foreach(var productID in consumableProductID)
            {
                builder.AddProduct(productID, ProductType.Consumable);
            }

            builder.AddProduct(nonConsumableProductID, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyConsumable(int index)
        {
            m_StoreController.InitiatePurchase(consumableProductID[index]);
        }

        public void BuyNonConsumable()
        {
            m_StoreController.InitiatePurchase(nonConsumableProductID);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (message != null)
            {
                errorMessage += $" More details: {message}";
            }

            Debug.Log(errorMessage);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            //Retrieve the purchased product
            var product = args.purchasedProduct;

            int index = 0;

            if (product.definition.id == nonConsumableProductID)
            {
                AdManager.RemoveAd();
            }
            else
            {
                foreach (var productID in consumableProductID)
                {
                    if (product.definition.id == productID)
                    {
                        AddStars(consumableRewardedStars[index]);
                    }

                    index++;
                }
            }


            Debug.Log($"Purchase Complete - Product: {product.definition.id}");

            //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
                $" Purchase failure reason: {failureDescription.reason}," +
                $" Purchase failure details: {failureDescription.message}");
        }

        void AddStars(int count)
        {
            StarManager.AddStarCount(count);
        }

        

    }
}
#endif