#ifdef __cplusplus
extern "C"
{
#endif
	void GPUSH_U3D_Init(const char* appid,const char* appkey);
	void GPUSH_U3D_SetUserInfo(const char* area,const char* username);
	void GPUSH_U3D_Vibrate(long millisecond);
	void GPUSH_U3D_NewNotification(int id,const char* title,const char* content,int day,int hour,int minute,int second);
	void GPUSH_U3D_NewNotificationRepeat(int id,const char* title,const char* content,int day,int hour,int minute,int second);
	void GPUSH_U3D_ClearNotification(int id);
	void GPUSH_U3D_ClearAllNotification(int userdata);
	void GPUSH_U3D_StartNotification(int userdata);
#ifdef __cplusplus
}
#endif 