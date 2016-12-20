//
//  GHomeAPI_Unity3D.h
//

#import "GHomeAPI_Unity3D.h"
#import "GHomeAPI.h"
#import <UIKit/UIKit.h>

extern NSString* __X_CHANNEL;

@interface GHomeInner : NSObject<
GHomeAPIInitializeDelegate,
GHomeAPILoginDelegate,
GHomeAPIGetTicketDelegate,
GHomeAPILogoutDelegate,
GHomeAPIPayDelegate,
GHomeAPIGetAreaConfigrationDelegate,
GHomeAPIGetProductConfigrationDelegate,
GHomeAPIExtendDelegate>

+(const char*)validStr:(NSString*)str;
+(NSString*)validNSStr:(const char*)str;
+ (id)toArrayOrNSDictionary:(NSString *)jsonString;
@end

static GHomeInner*       _gHome           = nil;
static UIViewController*    _mainController     = nil;

static char _init_unity_obj[128]               = {0};
static char _login_unity_obj[128]               = {0};
static char _logout_unity_obj[128]              = {0};
static char _pay_unity_obj[128]                 = {0};
static char _get_area_configuration_unity_obj[128]      = {0};
static char _get_product_configuration_unity_obj[128]   = {0};
static char _get_ticket_unity_obj[128]   = {0};
static char _do_extend_unity_obj[128]   = {0};
static char _get_channelcode_unity_obj[128]   = {0};


static const char* _init_unity_callback                = "GHInitializeCallback";
static const char* _login_unity_callback                = "GHLoginCallback";
static const char* _logout_unity_callback               = "GHLogoutCallback";
static const char* _pay_unity_callback                  = "GHPayCallback";
static const char* _get_area_configuration_unity_callback       = "GHGetAreaConfigCallback";
static const char* _get_product_configuration_unity_callback    = "GHGetProductConfigCallback";
static const char* _get_ticket_unity_callback           = "GHGetTicketCallback";
static const char* _do_extend_unity_callback           = "GHDoExtendCallback";
static const char* _do_extend_login_unity_callback           = "GHDoExtendLoginCallback";
static const char* _do_extend_logout_unity_callback           = "GHDoExtendLogoutCallback";
static const char* _get_channelcode_unity_callback           = "GHGetChannelCodeCallback";

void checkGHomeInner()
{
    if (!_gHome)
    {
        _gHome = [[GHomeInner alloc] init];
    }
    
    if (!_mainController)
    {
        if ( [[UIDevice currentDevice].systemVersion floatValue] < 6.0)
        {
            NSArray* array = [[UIApplication sharedApplication]windows];
            UIWindow* windows = [array objectAtIndex:0];
            UIView* ui = [[windows subviews] objectAtIndex:0];
            _mainController = (UIViewController*)[ui nextResponder];
        }
        else
        {
            _mainController = [UIApplication sharedApplication].keyWindow.rootViewController;
        }
    }
}

@implementation GHomeInner

+ (const char*)validStr:(NSString *)str
{
    return str && [str length] > 0 ? [str cStringUsingEncoding:NSUTF8StringEncoding] : "";
}

+ (NSString*) validNSStr:(const char*)str
{
    return [NSString stringWithCString:str encoding:NSUTF8StringEncoding];
}

// 将JSON串转化为字典或者数组
+ (id)toArrayOrNSDictionary:(NSString *)jsonString
{
    NSError *error = nil;
    id jsonObject = [NSJSONSerialization JSONObjectWithData:[jsonString dataUsingEncoding:NSUTF8StringEncoding]
                                                    options:NSJSONReadingAllowFragments
                                                      error:&error];
    
    if (jsonObject != nil && error == nil){
        return jsonObject;
    }else{
        // 解析错误
        return nil;
    }
    
}

#pragma mark GHomeAPIDelegate
- (void)initializeResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg
{
    NSDictionary* info = @{@"code" : [NSNumber numberWithInteger:resultCode],
                           @"msg" : (resultMsg ? resultMsg : @"")
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    
    UnitySendMessage(_init_unity_obj, _init_unity_callback, data);
}

- (void)loginResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg ticket:(NSString*)ticket userId:(NSString*)userId
{
    NSDictionary* info = @{@"code" : [NSNumber numberWithInteger:resultCode],
                           @"msg" : (resultMsg ? resultMsg : @""),
                           @"data" : @{@"ticket" : (ticket ? ticket : @""),@"userId" : (userId ? userId : @"")}
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    
    UnitySendMessage(_login_unity_obj, _login_unity_callback, data);
}

- (void)getTicketResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg ticket:(NSString*)ticket
{
    NSDictionary* info = @{@"code" : [NSNumber numberWithInteger:resultCode],
                           @"msg" : (resultMsg ? resultMsg : @""),
                           @"data" : @{@"ticket" : (ticket ? ticket : @"")}
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    UnitySendMessage(_get_ticket_unity_obj, _get_ticket_unity_callback, data);
}


- (void)logoutResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg
{
    NSDictionary* info = @{@"code" : [NSNumber numberWithInteger:resultCode],
                           @"msg" : (resultMsg ? resultMsg : @"")
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    UnitySendMessage(_logout_unity_obj, _logout_unity_callback, data);
}

- (void)getProductConfigurationResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg info:(NSDictionary*)info
{
    NSString* messageStr = @"";
    NSString* resultStr = @"";
    if([info objectForKey:@"message"])
    {
        NSData * messageData = [NSJSONSerialization dataWithJSONObject:[info objectForKey:@"message"] options:NSJSONWritingPrettyPrinted error:nil];
        messageStr = [[NSString alloc] initWithData:messageData encoding:NSUTF8StringEncoding];
    }
    if([info objectForKey:@"result"])
    {
        resultStr = [NSString stringWithFormat:@"%@",[info objectForKey:@"result"]];
    }
    
    NSDictionary* newInfo = @{@"code" : [NSNumber numberWithInteger:resultCode],
                              @"msg" : (resultMsg ? resultMsg : @""),
                              @"data" : @{@"message" : messageStr, @"result" : resultStr}
                              };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:newInfo options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    UnitySendMessage(_get_product_configuration_unity_obj, _get_product_configuration_unity_callback, data);
}

- (void)getAreaConfigrationResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg info:(NSDictionary*)info
{
    NSString* messageStr = @"";
    NSString* resultStr = @"";
    if([info objectForKey:@"message"])
    {
        NSData * messageData = [NSJSONSerialization dataWithJSONObject:[info objectForKey:@"message"] options:NSJSONWritingPrettyPrinted error:nil];
        messageStr = [[NSString alloc] initWithData:messageData encoding:NSUTF8StringEncoding];
    }
    if([info objectForKey:@"result"])
    {
        resultStr = [NSString stringWithFormat:@"%@",[info objectForKey:@"result"]];
    }
    
    NSDictionary* newInfo = @{@"code" : [NSNumber numberWithInteger:resultCode],
                              @"msg" : (resultMsg ? resultMsg : @""),
                              @"data" : @{@"message" : messageStr, @"result" : resultStr}
                              };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:newInfo options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    
    UnitySendMessage(_get_area_configuration_unity_obj, _get_area_configuration_unity_callback, data);
}

- (void)payResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg
{
    NSDictionary* info = @{@"code" : [NSNumber numberWithInteger:resultCode],
                           @"msg" : (resultMsg ? resultMsg : @"")
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    
    UnitySendMessage(_pay_unity_obj, _pay_unity_callback, data);
}

- (void)extendFunctionResult:(NSInteger)resultCode resultMsg:(NSString*)resultMsg type:(GHomeExtendFunctionType)type info:(NSDictionary*)info
{
    NSString* infoStr = @"";
    if(info)
    {
        NSData * infoData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
        infoStr = [[NSString alloc] initWithData:infoData encoding:NSUTF8StringEncoding];
    }
    
    NSDictionary* newInfo = @{@"code" : [NSNumber numberWithInteger:resultCode],
                              @"msg" : (resultMsg ? resultMsg : @""),
                              @"data" : @{@"doExtendRequestCommand" : [NSString stringWithFormat:@"%d",type], @"info" : infoStr}
                              };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:newInfo options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    UnitySendMessage(_do_extend_unity_obj, _do_extend_unity_callback, data);
}


- (void)extendLoginResult:(NSInteger)resultCode resultMsg:(NSString *)resultMsg ticket:(NSString *)ticket userId:(NSString *)userId
{
    NSDictionary* info = @{@"code" : [NSNumber numberWithInteger:resultCode],
                           @"msg" : (resultMsg ? resultMsg : @""),
                           @"data" : @{@"ticket" : (ticket ? ticket : @""),@"userId" : (userId ? userId : @"")}
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    
    UnitySendMessage(_do_extend_unity_obj, _do_extend_login_unity_callback, data);
}

- (void)extendLogoutResult:(NSInteger)resultCode resultMsg:(NSString *)resultMsg
{
    NSDictionary* info = @{@"code" : [NSNumber numberWithInteger:resultCode],
                           @"msg" : (resultMsg ? resultMsg : @"")
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    UnitySendMessage(_do_extend_unity_obj, _do_extend_logout_unity_callback, data);
}
@end

void GP_U3D_Init(const char* gameId,const char* unityObj)
{
    checkGHomeInner();
    strncpy(_init_unity_obj, unityObj, sizeof(_init_unity_obj));
    [[GHomeAPI sharedGHome] initialize:_gHome appId:[GHomeInner validNSStr:gameId]];
}

void GP_U3D_Login(const char* unityObj)
{
    checkGHomeInner();
    strncpy(_login_unity_obj, unityObj, sizeof(_login_unity_obj));
    
    [[GHomeAPI sharedGHome] login:_gHome];
}

void GP_U3D_Logout(const char* unityObj)
{
    checkGHomeInner();
    strncpy(_logout_unity_obj, unityObj, sizeof(_logout_unity_obj));
    
    [[GHomeAPI sharedGHome] logout:_gHome];
}

void GP_U3D_SetGameArea(const char* areaId)
{
    checkGHomeInner();
    [[GHomeAPI sharedGHome] loginArea:[GHomeInner validNSStr:areaId]];
}

void GP_U3D_Pay(const char* orderId, const char* areaId, const char* productId, const char* extend, const char* unityObj)
{
    checkGHomeInner();
    strncpy(_pay_unity_obj, unityObj, sizeof(_pay_unity_obj));
    
    [[GHomeAPI sharedGHome] pay:_gHome productId:[GHomeInner validNSStr:productId] areaId:[GHomeInner validNSStr:areaId] gameOrderId:[GHomeInner validNSStr:orderId] extendInfo:[GHomeInner validNSStr:extend]];
}

void GP_U3D_GetAreaConfiguration(const char* unityObj)
{
    checkGHomeInner();
    strncpy(_get_area_configuration_unity_obj, unityObj, sizeof(_get_area_configuration_unity_obj));
    [[GHomeAPI sharedGHome] getAreaConfiguration:_gHome];
}

void GP_U3D_GetProductConfiguration(const char* unityObj)
{
    checkGHomeInner();
    strncpy(_get_product_configuration_unity_obj, unityObj, sizeof(_get_product_configuration_unity_obj));
    [[GHomeAPI sharedGHome] getProductConfiguration:_gHome];
}

void GP_U3D_GetTicket(const char* appId, const char* areaId, const char* unityObj)
{
    checkGHomeInner();
    strncpy(_get_ticket_unity_obj, unityObj, sizeof(_get_ticket_unity_obj));
    [[GHomeAPI sharedGHome] getTicket:_gHome appId:[GHomeInner validNSStr:appId] areaId:[GHomeInner validNSStr:areaId]];
}

void GP_U3D_DoExtend(const char* unityObj, int command, const char* param)
{
    checkGHomeInner();
    strncpy(_do_extend_unity_obj, unityObj, sizeof(_do_extend_unity_obj));
    [[GHomeAPI sharedGHome] extendFunction:_gHome type:(GHomeExtendFunctionType)command parameter:[GHomeInner toArrayOrNSDictionary:[GHomeInner validNSStr:param]]];
}

void GP_U3D_GetChannelCode(const char* unityObj)
{
    checkGHomeInner();
    strncpy(_get_channelcode_unity_obj, unityObj, sizeof(_get_channelcode_unity_obj));
    
    NSDictionary* info = @{@"code" : @0,
                           @"msg" : @"ok",
                           @"data" : @{@"channelcode" : __X_CHANNEL}
                           };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:info options:NSJSONWritingPrettyPrinted error:nil];
    const char* data = "";
    if (jsonData && [jsonData length] > 0)
    {
        data = [GHomeInner validStr:[[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding]];
    }
    UnitySendMessage(_get_channelcode_unity_obj, _get_channelcode_unity_callback, data);
}


