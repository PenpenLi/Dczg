#import "GPUSH_IOS_API.h"
#import "GHomePushAPI.h"
void GPUSH_U3D_Init(const char* gameId,const char* appkey)
{
   // GHome Push init
    [GHomePushAPI registerForRemoteNotificationTypes:UIRemoteNotificationTypeBadge | UIRemoteNotificationTypeSound | UIRemoteNotificationTypeAlert];
    NSString *NS_gameId = [[NSString alloc] initWithCString:(const char*)gameId encoding:NSASCIIStringEncoding];
    NSString *NS_appkey = [[NSString alloc] initWithCString:(const char*)appkey encoding:NSASCIIStringEncoding];
    [GHomePushAPI setAppId:NS_gameId appKey:NS_appkey];

}

void GPUSH_U3D_SetUserInfo(const char* area,const char* username)
{
    NSString *NS_area = [[NSString alloc] initWithCString:(const char*)area encoding:NSASCIIStringEncoding];
    NSString *NS_username = [[NSString alloc] initWithCString:(const char*)username encoding:NSASCIIStringEncoding];
    [GHomePushAPI setAreaId:NS_area roleName:NS_username];
}
void GPUSH_U3D_Vibrate(long millisecond)
{
    //AudioServicesPlaySystemSound(kSystemSoundID_Vibrate);
}
void GPUSH_U3D_NewNotification(int id,const char* title,const char* content,int day,int hour,int minute,int second)
{
    NSString *NS_title = [[NSString alloc] initWithCString:(const char*)title encoding:NSUTF8StringEncoding];
    NSString *NS_content = [[NSString alloc] initWithCString:(const char*)content encoding:NSUTF8StringEncoding];
    
    long time = (((day*24+hour)*60+minute)*60+second);
    
    NSDate *date = [NSDate dateWithTimeIntervalSinceNow:time];
    //chuagjian一个本地推送
    UILocalNotification *noti = [[[UILocalNotification alloc] init] autorelease];
    if (noti) {
        //设置推送时间
        noti.fireDate = date;
        //设置时区
        noti.timeZone = [NSTimeZone defaultTimeZone];
        //设置重复间隔
        noti.repeatInterval = 0;//NSWeekCalendarUnit;
        //推送声音
        noti.soundName = UILocalNotificationDefaultSoundName;
        noti.alertAction=   NS_title;
        //内容
        noti.alertBody = NS_content;
        //显示在icon上的红色圈中的数子
        //noti.applicationIconBadgeNumber = 1;
        //设置userinfo 方便在之后需要撤销的时候使用
        //NSDictionary *infoDic = [NSDictionary dictionaryWithObject:@"name" forKey:@"key"];
        //noti.userInfo = infoDic;
        //添加推送到uiapplication
        UIApplication *app = [UIApplication sharedApplication];
        [app scheduleLocalNotification:noti];
    }
}
void GPUSH_U3D_NewNotificationRepeat(int id,const char* title,const char* content,int day,int hour,int minute,int second)
{
    NSString *NS_title = [[NSString alloc] initWithCString:(const char*)title encoding:NSUTF8StringEncoding];
    NSString *NS_content = [[NSString alloc] initWithCString:(const char*)content encoding:NSUTF8StringEncoding];
    
    long time = (((day*24+hour)*60+minute)*60+second);
    
    NSDate *date = [NSDate dateWithTimeIntervalSinceNow:time];
    //chuagjian一个本地推送
    UILocalNotification *noti = [[[UILocalNotification alloc] init] autorelease];
    if (noti) {
        //设置推送时间
        noti.fireDate = date;
        //设置时区
        noti.timeZone = [NSTimeZone defaultTimeZone];
        //noti.repeatCalendar =   NSCalendarCalendarUnit
        //设置重复间隔
        if(day==1)
        {
            noti.repeatInterval = NSCalendarUnitDay;
        }
        else if(day==7)
        {
            noti.repeatInterval = kCFCalendarUnitWeek;
        }
        else
        {
            noti.repeatInterval = NSCalendarUnitDay;

        }
        //推送声音
        noti.soundName = UILocalNotificationDefaultSoundName;
        noti.alertAction=   NS_title;
        //内容
        noti.alertBody = NS_content;
        //显示在icon上的红色圈中的数子
        //noti.applicationIconBadgeNumber = 1;
        //设置userinfo 方便在之后需要撤销的时候使用
        //NSDictionary *infoDic = [NSDictionary dictionaryWithObject:id forKey:@"key"];
        //infoDic.//[@"id"]=id;
        //noti.userInfo = infoDic;
        //添加推送到uiapplication
        UIApplication *app = [UIApplication sharedApplication];
        [app scheduleLocalNotification:noti];
    }
}
void GPUSH_U3D_ClearNotification(int id)
{
    UIApplication *app = [UIApplication sharedApplication];
    //获取本地推送数组
    NSArray *localArr = [app scheduledLocalNotifications];
    
    //声明本地通知对象
    UILocalNotification *localNoti;
    
    if (localArr) {
        for (UILocalNotification *noti in localArr) {
            NSDictionary *dict = noti.userInfo;
            if (dict) {
                int oldid = [dict objectForKey:@"key"];
                if (id == oldid) {
                    if (localNoti){
                        [localNoti release];
                        localNoti = nil;
                    }
                    localNoti = [noti retain];
                    break;
                }
            }
        }
        
        //判断是否找到已经存在的相同key的推送
        if (!localNoti) {
            //不存在 初始化
            localNoti = [[UILocalNotification alloc] init];
        }
        
        if (localNoti ) {
            //不推送 取消推送
            [app cancelLocalNotification:localNoti];
            [localNoti release];
            return;
        }
    }
}
void GPUSH_U3D_ClearAllNotification(int userdata)
{
    UIApplication *app = [UIApplication sharedApplication];
    [app cancelAllLocalNotifications];

}
void GPUSH_U3D_StartNotification(int userdata)
{
    
}