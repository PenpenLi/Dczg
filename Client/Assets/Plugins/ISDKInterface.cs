using System;
using System.Collections.Specialized;

public interface ISDKInterface
{
    void Initialize(string gameId);
	void Login();
	void LoginArea(string areaId);
	void Logout();
	void Pay(string orderId, string areaId, string productId, string extend);
	void GetTicket (string appId, string areaId);
	void Pause();
	void Resume();
	void ShowFloatIcon(bool show, int position);
	void GetAreaConfig();
	void GetProductConfig();
	void DoExtend(int command, NameValueCollection param);
	void Destroy();
}
