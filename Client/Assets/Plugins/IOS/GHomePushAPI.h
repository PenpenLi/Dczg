//
//  GHomePush.h
//
#import <UIKit/UIKit.h>

NSString* GHOME_PUSH_VERSION();     // 2.0.0.1

@interface GHomePushAPI : NSObject
/**
 * @brief 注册APNS类型，在APP启动时调用
 *
 * @param types   UIRemoteNotificationTypeAlert、UIRemoteNotificationTypeBadge、UIRemoteNotificationTypeSound的组合
 */
+ (void)registerForRemoteNotificationTypes:(NSInteger)types;

/**
 * @brief 设置notification的devicetoken
 *
 * @param deviceToken   系统产生的deviceToken
 */
+ (void)setDeviceToken:(NSData*)deviceToken;

/**
 * @brief 设置应用信息
 *
 * @param appId         应用ID，同GameId
 *        appKey        应用key
 */
+ (void)setAppId:(NSString*)appId
          appKey:(NSString*)appKey;

/**
 * @brief 设置用户信息
 *
 * @param areaId        区ID，默认为@"0"
 *        userId        mid
 *        roleName      角色名，默认为@"0"
 */
+ (void)setAreaId:(NSString*)areaId
         roleName:(NSString*)roleName;


/**
 * @brief 程序未运行或者运行时，收到推送消息
 *
 * @param   notification    通知内容
 *
 * @return  NSString        具体的通知文字
 */
+ (NSString*)receiveRemoteNotification:(NSDictionary*)notification;

/**
 * @brief 当前图标的数字
 */
+ (NSUInteger)applicationBadgeNumber;

/**
 * @brief 设置图标的数字
 */
+ (void)setApplicationBadgeNumber:(NSUInteger)number;

@end
