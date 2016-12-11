using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
  
    public class IAPManager : MonoBehaviour, IStoreListener
    {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        public static string FULL_VERSION_KEY = "full_version";

        // Google Play Store-specific product identifier subscription product.
        private static string kProductNameGooglePlaySubscription = "com.ensorcell.vees";
        

        //Local params
        enum States {first,second,third};
        States currentState;
        public Text titleDisplay,priceDisplay;
        float price,maxPrice,minPrice;
        public GameObject increaseButton, decreaseButton, priceText;

        void Start()
        {
            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }

            //Local Params
            currentState = States.first;
            price = 1;
            maxPrice = 10;
            minPrice = 1;
        }
        
        void Update()
        {
            switch(currentState)
            {
                case States.first:
                titleDisplay.text = "We are a young and small team who makes games with lots of passion";
                if(Input.GetMouseButtonDown(0))
                {
                    currentState = States.second;
                }
                break;
                case States.second:
                titleDisplay.text = "If you think the game's worth it,Help us buy some beer and we will remove the ads for you :)";
                if (Input.GetMouseButtonDown(0))
                {
                    currentState = States.third;
                }
                break;
                case States.third:
                increaseButton.active = true;
                decreaseButton.active = true;
                priceText.active = true;
                if (price != 1)
                {
                    titleDisplay.text = price + " Beers";
                }
                else
                {
                    titleDisplay.text = price + "Beer";
                }
                break;
            }

            setPriceDisplay();
        }

        public void IncreasePrice()
        {
            if(price<maxPrice)
            {
                price++;
            }
        }

        public void DecreasePrice()
        {
            if(price>minPrice)
            {
                price--;
            }
        }

        public void confirmPurchase()
        {
             if (currentState == States.third)
            {
                Debug.Log("IAP Purchase of " + price + " to be done");
            }
        }

        void setPriceDisplay()
        {
            priceDisplay.text = (price - 0.01f) + "$";
        }

        public void InitializePurchasing()
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(FULL_VERSION_KEY, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }


        private bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void BuyFullVersion()
        {
            // Buy the non-consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(FULL_VERSION_KEY);
        }


        void BuyProductID(string productId)
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("Purchase setup failed due to" + error);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (String.Equals(args.purchasedProduct.definition.id, FULL_VERSION_KEY, StringComparison.Ordinal))
            {
                Debug.Log("Thanks for buying the full version :)");
                // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
            }

            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else
            {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }