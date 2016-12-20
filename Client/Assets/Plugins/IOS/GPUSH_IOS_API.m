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
    //chuagjianһ����������
    UILocalNotification *noti = [[[UILocalNotification alloc] init] autorelease];
    if (noti) {
        //��������ʱ��
        noti.fireDate = date;
        //����ʱ��
        noti.timeZone = [NSTimeZone defaultTimeZone];
        //�����ظ����
        noti.repeatInterval = 0;//NSWeekCalendarUnit;
        //��������
        noti.soundName = UILocalNotificationDefaultSoundName;
        noti.alertAction=   NS_title;
        //����
        noti.alertBody = NS_content;
        //��ʾ��icon�ϵĺ�ɫȦ�е�����
        //noti.applicationIconBadgeNumber = 1;
        //����userinfo ������֮����Ҫ������ʱ��ʹ��
        //NSDictionary *infoDic = [NSDictionary dictionaryWithObject:@"name" forKey:@"key"];
        //noti.userInfo = infoDic;
        //������͵�uiapplication
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
    //chuagjianһ����������
    UILocalNotification *noti = [[[UILocalNotification alloc] init] autorelease];
    if (noti) {
        //��������ʱ��
        noti.fireDate = date;
        //����ʱ��
        noti.timeZone = [NSTimeZone defaultTimeZone];
        //noti.repeatCalendar =   NSCalendarCalendarUnit
        //�����ظ����
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
        //��������
        noti.soundName = UILocalNotificationDefaultSoundName;
        noti.alertAction=   NS_title;
        //����
        noti.alertBody = NS_content;
        //��ʾ��icon�ϵĺ�ɫȦ�е�����
        //noti.applicationIconBadgeNumber = 1;
        //����userinfo ������֮����Ҫ������ʱ��ʹ��
        //NSDictionary *infoDic = [NSDictionary dictionaryWithObject:id forKey:@"key"];
        //infoDic.//[@"id"]=id;
        //noti.userInfo = infoDic;
        //������͵�uiapplication
        UIApplication *app = [UIApplication sharedApplication];
        [app scheduleLocalNotification:noti];
    }
}
void GPUSH_U3D_ClearNotification(int id)
{
    UIApplication *app = [UIApplication sharedApplication];
    //��ȡ������������
    NSArray *localArr = [app scheduledLocalNotifications];
    
    //��������֪ͨ����
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
        
        //�ж��Ƿ��ҵ��Ѿ����ڵ���ͬkey������
        if (!localNoti) {
            //������ ��ʼ��
            localNoti = [[UILocalNotification alloc] init];
        }
        
        if (localNoti ) {
            //������ ȡ������
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