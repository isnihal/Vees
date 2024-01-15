﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;//Remove if iOS


// public class IAPManager : MonoBehaviour, IStoreListener
// {
//     private static IStoreController m_StoreController;          // The Unity Purchasing system.
//     private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.


//     public static string oneBeer = "beer1";
//     public static string twoBeer = "beer2";
//     public static string threeBeer = "beer3";
//     public static string fourBeer = "beer4";
//     public static string fiveBeer = "beer5";

//     static bool hasUserPurchased;

//     int iapIndex;
//     //Minor bug avoidance 
//     float timeLeft;
//     bool startTimer,timerFinsihed;
//     //--------------
//     enum States {first,second,third};
//     States currentState;
//     public GameObject instructionText, priceText, increaseButton, decreaseButton;

//     // Google Play Store-specific product identifier subscription product.
//     private static string kProductNameGooglePlaySubscription = "com.truffleandcaffeine.vees";

//     void Start()
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             hasUserPurchased = false;
//             // If we haven't set up the Unity Purchasing reference
//             if (m_StoreController == null)
//             {
//                 // Begin to configure our connection to Purchasing
//                 InitializePurchasing();
//             }
//             if (Application.loadedLevel != 1)
//             {
//                 currentState = States.first;
//             }

//             iapIndex = 1;
//             timeLeft = 1f;
//             startTimer = false;
//             timerFinsihed = false;
//         }
//     }

//     void Update()
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             if (Application.loadedLevel == 1)
//             {
//                 return;
//             }

//             updateHasUserPurchased();

//             if (!hasUserPurchased)
//             {
//                 switch (currentState)
//                 {
//                     case States.first:
//                         instructionText.GetComponent<Text>().text = "We are a young and passionate team committed to making awesome games.";
//                         if (Input.GetMouseButtonDown(0))
//                         {
//                             currentState = States.second;
//                         }
//                         break;
//                     case States.second:
//                         instructionText.GetComponent<Text>().text = "If you think the game's worth it, Help us buy a beer and we will remove the ads for you :)";
//                         if (Input.GetMouseButtonDown(0))
//                         {
//                             currentState = States.third;
//                         }
//                         break;
//                     case States.third:
//                         startTimer = true;
//                         if (startTimer)
//                         {
//                             timeLeft -= Time.deltaTime;
//                             if (timeLeft <= 0)
//                             {
//                                 startTimer = false;
//                                 timerFinsihed = true;
//                             }
//                         }
//                         if (iapIndex == 1)
//                         {
//                             instructionText.GetComponent<Text>().text = "1 Beer";
//                         }
//                         else
//                         {
//                             instructionText.GetComponent<Text>().text = iapIndex + " Beers";
//                         }
//                         priceText.active = true;
//                         increaseButton.active = true;
//                         decreaseButton.active = true;
//                         priceText.GetComponent<Text>().text = (iapIndex - 0.01) + " $";
//                         break;
//                 }
//             }
//             else
//             {
//                 instructionText.GetComponent<Text>().text = "Thank you for purchasing Vees,Your game will now be ad free :)";
//                 priceText.active = false;
//                 increaseButton.active = false;
//                 decreaseButton.active = false;
//             }
//         }
//     }

//     void updateHasUserPurchased()
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             Product beer1 = m_StoreController.products.WithID(oneBeer);
//             if (beer1 != null && beer1.hasReceipt)
//             {
//                 // Owned Non Consumables and Subscriptions should always have receipts.
//                 // So here the Non Consumable product has already been bought.
//                 hasUserPurchased = true;
//             }

//             Product beer2 = m_StoreController.products.WithID(twoBeer);
//             if (beer2 != null && beer2.hasReceipt)
//             {
//                 hasUserPurchased = true;
//             }

//             Product beer3 = m_StoreController.products.WithID(threeBeer);
//             if (beer3 != null && beer3.hasReceipt)
//             {

//                 hasUserPurchased = true;
//             }

//             Product beer4 = m_StoreController.products.WithID(fourBeer);
//             if (beer4 != null && beer4.hasReceipt)
//             {

//                 hasUserPurchased = true;
//             }

//             Product beer5 = m_StoreController.products.WithID(fiveBeer);
//             if (beer5 != null && beer5.hasReceipt)
//             {

//                 hasUserPurchased = true;
//             }
//         }
//     }

//     public void InitializePurchasing()
//     {
//         //if (PlatformManager.platform == "ANDROID")
//         //{
//         //    // If we have already connected to Purchasing ...
//         //    if (IsInitialized())
//         //    {
//         //        // ... we are done here.
//         //        return;
//         //    }

//         //    // Create a builder, first passing in a suite of Unity provided stores.
//         //    var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//         //    //TODO:Add all IAP Products here
//         //    builder.AddProduct(oneBeer, ProductType.NonConsumable);
//         //    builder.AddProduct(twoBeer, ProductType.NonConsumable);
//         //    builder.AddProduct(threeBeer, ProductType.NonConsumable);
//         //    builder.AddProduct(fourBeer, ProductType.NonConsumable);
//         //    builder.AddProduct(fiveBeer, ProductType.NonConsumable);


//         //    // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
//         //    // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
//         //    UnityPurchasing.Initialize(this, builder);
//         //}
//     }

//     private bool IsInitialized()
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             // Only say we are initialized if both the Purchasing references are set.
//             return m_StoreController != null && m_StoreExtensionProvider != null;
//         }
//         else
//             return false;
//     }

//     public void BuyNonConsumable()
//     {
//         // Buy the non-consumable product using its general identifier. Expect a response either 
//         // through ProcessPurchase or OnPurchaseFailed asynchronously.

//         if (PlatformManager.platform == "ANDROID")
//         {
//             if (currentState == States.third && timerFinsihed)
//             {
//                 switch (iapIndex)
//                 {
//                     case 1:
//                         BuyProductID(oneBeer);
//                         break;
//                     case 2:
//                         BuyProductID(twoBeer);
//                         break;
//                     case 3:
//                         BuyProductID(threeBeer);
//                         break;
//                     case 4:
//                         BuyProductID(fourBeer);
//                         break;
//                     case 5:
//                         BuyProductID(fiveBeer);
//                         break;
//                 }
//             }
//         }
//     }

//     void BuyProductID(string productId)
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             // If Purchasing has been initialized ...
//             if (IsInitialized())
//             {
//                 // ... look up the Product reference with the general product identifier and the Purchasing 
//                 // system's products collection.
//                 Product product = m_StoreController.products.WithID(productId);

//                 // If the look up found a product for this device's store and that product is ready to be sold ... 
//                 if (product != null && product.availableToPurchase)
//                 {
//                     Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//                     // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
//                     // asynchronously.
//                     m_StoreController.InitiatePurchase(product);
//                 }
//                 // Otherwise ...
//                 else
//                 {
//                     // ... report the product look-up failure situation  
//                     Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//                 }
//             }
//             // Otherwise ...
//             else
//             {
//                 // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
//                 // retrying initiailization.
//                 Debug.Log("BuyProductID FAIL. Not initialized.");
//             }
//         }
//     }


//     // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
//     // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
//     public void RestorePurchases()
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             // If Purchasing has not yet been set up ...
//             if (!IsInitialized())
//             {
//                 // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
//                 Debug.Log("RestorePurchases FAIL. Not initialized.");
//                 return;
//             }
//         }
//     }

//     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             // Purchasing has succeeded initializing. Collect our Purchasing references.
//             Debug.Log("OnInitialized: PASS");

//             // Overall Purchasing system, configured with products for this application.
//             m_StoreController = controller;
//             // Store specific subsystem, for accessing device-specific store features.
//             m_StoreExtensionProvider = extensions;
//         }
//     }

//     public void OnInitializeFailed(InitializationFailureReason error)
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
//             Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
//         }
//     }

//     public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             if (String.Equals(args.purchasedProduct.definition.id, oneBeer, StringComparison.Ordinal))
//             {
//                 Debug.Log("Alert: 1 Beer Purchased");
//                 hasUserPurchased = true;
//             }

//             else if (String.Equals(args.purchasedProduct.definition.id, twoBeer, StringComparison.Ordinal))
//             {
//                 Debug.Log("Alert: 2 Beer Purchased");
//                 hasUserPurchased = true;
//             }

//             else if (String.Equals(args.purchasedProduct.definition.id, threeBeer, StringComparison.Ordinal))
//             {
//                 Debug.Log("Alert: 3 Beer Purchased");
//                 hasUserPurchased = true;
//             }

//             else if (String.Equals(args.purchasedProduct.definition.id, fourBeer, StringComparison.Ordinal))
//             {
//                 Debug.Log("Alert: 4 Beer Purchased");
//                 hasUserPurchased = true;
//             }

//             else if (String.Equals(args.purchasedProduct.definition.id, fiveBeer, StringComparison.Ordinal))
//             {
//                 Debug.Log("Alert: 5 Beer Purchased");
//                 hasUserPurchased = true;
//             }

//             else
//             {
//                 Debug.Log("IAP Purchase fail");
//             }

//             // Return a flag indicating whether this product has completely been received, or if the application needs 
//             // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
//             // saving purchased products to the cloud, and when that save is delayed. 
//             return PurchaseProcessingResult.Complete;
//         }
//         else
//         {
//             return PurchaseProcessingResult.Pending;
//         }
//     }

//     public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
//             // this reason with the user to guide their troubleshooting actions.
//             Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//         }
//     }

//     public static bool hasUserPurchasedVees()
//     {

//         if (PlatformManager.platform == "ANDROID")
//         {
//             Product beer1 = m_StoreController.products.WithID(oneBeer);
//             if (beer1 != null && beer1.hasReceipt)
//             {
//                 // Owned Non Consumables and Subscriptions should always have receipts.
//                 // So here the Non Consumable product has already been bought.
//                 return true;
//             }

//             Product beer2 = m_StoreController.products.WithID(twoBeer);
//             if (beer2 != null && beer2.hasReceipt)
//             {
//                 return true;
//             }

//             Product beer3 = m_StoreController.products.WithID(threeBeer);
//             if (beer3 != null && beer3.hasReceipt)
//             {

//                 return true;
//             }

//             Product beer4 = m_StoreController.products.WithID(fourBeer);
//             if (beer4 != null && beer4.hasReceipt)
//             {

//                 return true;
//             }

//             Product beer5 = m_StoreController.products.WithID(fiveBeer);
//             if (beer5 != null && beer5.hasReceipt)
//             {

//                 return true;
//             }
//             return false;
//         }
//         else
//             return false;
//     }

//     public void increaseIndex()
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             if (iapIndex < 5)
//             {
//                 iapIndex++;
//             }
//         }
//     }

//     public void decreaseIndex()
//     {
//         if (PlatformManager.platform == "ANDROID")
//         {
//             if (iapIndex > 1)
//             {
//                 iapIndex--;
//             }
//         }
//     }
// }