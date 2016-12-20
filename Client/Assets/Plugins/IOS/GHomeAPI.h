//
//  GHomeAPI.h
//  API for GHome
//
#import <Foundation/Foundation.h>

FOUNDATION_EXPORT NSString* GHOME_API_VERSION();
FOUNDATION_EXPORT NSString* GHOME_API_TYPE();

typedef enum
{
    GHOME_NONE_FUNCTION_TYPE,                           // 空方法，一般不调用
    GHOME_APPLICATIONHANDLEOPENURL_FUNCTION_TYPE,       // application启动url处理
    GHOME_PAUSE_FUNCTION_TYPE,                          // 暂停游戏
    
    GHOME_SHOWPLATFORM_FUNCTION_TYPE,                   // 显示平台界面
    GHOME_SHOWTOOLBAR_FUNCTION_TYPE,                    // 显示工具栏
    GHOME_HIDETOOLBAR_FUNCTION_TYPE,                    // 隐藏工具栏
    
    GHOME_SETORIENTATION_FUNCTION_TYPE,                 // 设置屏幕方向
    GHOME_SETSUBACCANDGUEST_FUNCTION_TYPE,              // 是否支持G家游客
    GHOME_SET_CHANNEL_FUNCTION_TYPE,                    // 设置渠道channel
}GHomeExtendFunctionType;

// delegate声明
@protocol GHomeAPIInitializeDelegate;
@protocol GHomeAPILoginDelegate;
@protocol GHomeAPIGetTicketDelegate;
@protocol GHomeAPILogoutDelegate;
@protocol GHomeAPIPayDelegate;
@protocol GHomeAPIGetAreaConfigrationDelegate;
@protocol GHomeAPIGetProductConfigrationDelegate;
@protocol GHomeAPIExtendDelegate;

// 仅允许单例，请使用instance
@interface GHomeAPI : NSObject

// 单例生成
+ (GHomeAPI*)sharedGHome;

// 统一设备唯一标示
+ (NSString*)deviceId;

/**
 * 初始化
 * @param delegate       委托对象
 *       appId          游戏ID
 */
- (void)initialize:(id<GHomeAPIInitializeDelegate>)delegate
             appId:(NSString*)appId;

/**
 * 登录
 * @param delegate       委托对象
 */
- (void)login:(id<GHomeAPILoginDelegate>)delegate;

/**
 * 获取票据，登录后有效
 * @param delegate       委托对象
 *       appId          游戏ID
 * @param areaId         区ID
 */
- (void)getTicket:(id<GHomeAPIGetTicketDelegate>)delegate
            appId:(NSString*)appId
           areaId:(NSString*)areaId;

/**
 * 登录
 * @param areaId         区ID
 */
- (void)loginArea:(NSString*)areaId;

/**
 * 注销（切换账号）
 * @param delegate       委托对象
 */
- (void)logout:(id<GHomeAPILogoutDelegate>)delegate;

/**
 * 获取支付产品信息
 * @param delegate       委托对象
 */
- (void)getProductConfiguration:(id<GHomeAPIGetProductConfigrationDelegate>)delegate;

/**
 * 获取区服信息
 * @param delegate       委托对象
 */
- (void)getAreaConfiguration:(id<GHomeAPIGetAreaConfigrationDelegate>)delegate;

/**
 * 显示支付页面
 * @param delegate       委托对象
 *       productId      产品ID
 *       areaId         区ID
 *       gameOrderId    游戏订单ID
 *       extendInfo     附加参数
 */
- (void)pay:(id<GHomeAPIPayDelegate>)delegate
  productId:(NSString*)productId
     areaId:(NSString*)areaId
gameOrderId:(NSString*)gameOrderId
 extendInfo:(NSString*)extendInfo;


/**
 * 扩展接口
 * @param delegate       委托对象
 *       type           接口类型
 *       parameter      接口参数，不同的type对应不同接口。
 *                      所有key和value都是NSString类型。
 *                      具体内容见文档。
 *
 */
- (NSInteger)extendFunction:(id<GHomeAPIExtendDelegate>)delegate
                       type:(GHomeExtendFunctionType)type
                  parameter:(NSDictionary*)param;
@end

// delegate定义
@protocol GHomeAPIInitializeDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 */
- (void)initializeResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg;
@end

@protocol GHomeAPILoginDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 *       ticket         验证使用的ticket，resultCode为0时返回。
 *       userId         用户账号，resultCode为0时返回。
 */
- (void)loginResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg ticket:(NSString*)ticket userId:(NSString*)userId;
@end

@protocol GHomeAPIGetTicketDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 *       ticket         验证使用的ticket，resultCode为0时返回。
 */
- (void)getTicketResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg ticket:(NSString*)ticket;
@end

@protocol GHomeAPILogoutDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 */
- (void)logoutResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg;
@end

@protocol GHomeAPIGetProductConfigrationDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 *       info           产品信息，resultCode为0时返回。
 */
- (void)getProductConfigurationResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg info:(NSDictionary*)info;
@end

@protocol GHomeAPIGetAreaConfigrationDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 *       info           区信息，resultCode为0时返回。
 */
- (void)getAreaConfigrationResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg info:(NSDictionary*)info;
@end

@protocol GHomeAPIPayDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 */
- (void)payResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg;
@end

@protocol GHomeAPIExtendDelegate <NSObject>
/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 *       type           调用时的type
 *       info           其他信息，resultCode为0时返回。
 */
- (void)extendFunctionResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg type:(GHomeExtendFunctionType)type info:(NSDictionary*)info;

/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 *       ticket         验证使用的ticket，resultCode为0时返回。
 *       userId         用户账号，resultCode为0时返回。
 */
- (void)extendLoginResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg ticket:(NSString*)ticket userId:(NSString*)userId;

/**
 * @param resultCode     返回码
 *       resultMsg      返回码描述
 */
- (void)extendLogoutResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg;
@end
