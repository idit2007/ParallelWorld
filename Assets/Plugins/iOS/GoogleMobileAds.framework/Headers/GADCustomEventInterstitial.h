//
//  GADCustomEventInterstitial.h
//  Google Mobile Ads SDK
//
//  Copyright 2012 Google Inc. All rights reserved.
//

#import <UIKit/UIKit.h>

#import <GoogleMobileAds/GADCustomEventInterstitialDelegate.h>
#import <GoogleMobileAds/GADCustomEventRequest.h>

/// The interstitial custom event protocol. Your interstitial custom event handler must implement
/// this protocol.
@protocol GADCustomEventInterstitial<NSObject>

/// Inform |delegate| with the custom event execution results to ensure mediation behaves correctly.
///
/// In your class, define the -delegate and -setDelegate: methods or use "@synthesize delegate". The
/// Google Mobile Ads SDK sets this property on instances of your class.
@property(nonatomic, weak) id<GADCustomEventInterstitialDelegate> delegate;

/// Called by mediation when your custom event is scheduled to be executed. Your implementation
/// should start retrieving the interstitial ad. Report execution results to the delegate. You must
/// wait until -presentFromRootViewController is called before displaying the interstitial ad.
/// \param serverParameter parameter configured in the mediation UI.
/// \param serverLabel label configured in the mediation UI.
/// \param request contains ad request information.
- (void)requestInterstitialAdWithParameter:(NSString *)serverParameter
                                     label:(NSString *)serverLabel
                                   request:(GADCustomEventRequest *)request;

/// Present the interstitial ad as a modal view using the provided view controller. Called only
/// after your class calls -customEventInterstitialDidReceiveAd: on its custom event delegate.
- (void)presentFromRootViewController:(UIViewController *)rootViewController;

@end
