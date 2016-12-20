//
//  GHomeAPI_Unity3D.h
//

#ifdef __cplusplus
extern "C"
{
#endif
    void GP_U3D_Init(const char* gameId,const char* unityObj);
    void GP_U3D_Login(const char* unityObj);
    void GP_U3D_Logout(const char* unityObj);
    void GP_U3D_SetGameArea(const char* areaId);
    void GP_U3D_Pay(const char* orderId, const char* areaId, const char* productId, const char* extend, const char* unityObj);
    void GP_U3D_GetAreaConfiguration(const char* unityObj);
    void GP_U3D_GetProductConfiguration(const char* unityObj);
    void GP_U3D_GetTicket(const char* appId, const char* areaId, const char* unityObj);
    void GP_U3D_DoExtend(const char* unityObj, int command, const char* param);
    void GP_U3D_GetChannelCode(const char* unityObj);
#ifdef __cplusplus
}
#endif
