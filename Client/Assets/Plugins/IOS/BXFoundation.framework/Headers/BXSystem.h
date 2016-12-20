#import <UIKit/UIKit.h>

@interface BXSystem : NSObject

/*
 * mac address
 */
+ (NSString*)macAddress;

/*
 * advertising id = IDFA
 */
+ (NSString*)ADID;

+ (NSString*)systemName;        // e.g. @"iOS"
+ (NSString*)model;             // e.g. @"iPhone", @"iPod touch"
+ (NSString*)localizedModel;    // localized version of model
+ (CGFloat)systemVersion;       // e.g. 4.0

+ (NSString*)appName;           // bundle display name
+ (NSString*)appVersion;        // bundle version
+ (NSString*)appBuild;          // bundle build version

/*
 * The screen size for application.
 */
+ (NSInteger)appScreenWidth;
+ (NSInteger)appScreenHeight;

/*
 * NSUserDefaults set / get object
 */
+ (id)userDefaultsObjectForKey:(NSString*)key;
+ (void)setUserDefaultsObject:(id)object key:(NSString*)key;
+ (void)removeUserDefaultsObjectForKey:(NSString*)key;

/*
 * some path
 */
+ (NSString*)appPath;
+ (NSString*)appLibraryPath;
+ (NSString*)appDocumentPath;

/*
 * auto get controller
 */
+ (UIViewController*)rootController;

/*
 * Check the network status
 */
+ (BOOL)isNetworkAvaliable;
+ (void)checkNetworkStatus:(void(^)(BOOL isAvaliable))status;

/*
 * Check the size of application on iPad.
 * It will return YES, if the size is phone application size.
 */
+ (BOOL)isPhoneSizeOnPad;

@end
