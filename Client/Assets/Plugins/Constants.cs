using System;
using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class Constants
{
	public const int ERROR_CODE_SUCCESS = 0;
	public const int ERROR_CODE_LOGIN_CANCEL = -100;
	public const int ERROR_NETWORK_TIMEOUT = -10869601;
	public const int ERROR_SERVER_RETURN = -10869602;
	public const int ERROR_CHANNEL_RETURN = -10869603;
	public const int ERROR_PAY_FAILED = -10869604;
	public const int ERROR_PAY_RESULT_NOT_GET = -10869605;
	public const int ERROR_PAY_SUCCESS_NOT_SEND_GOODS = -10869606;
	public const int ERROR_USER_NOT_LOGIN = -10869607;
	public const int ERROR_LOGIN_FAILED = -10869608;
	public const int ERROR_PAY_CANCEL = -10869609;
	public const int ERROR_INIT_FAILED = -10869610;
	public const int ERROR_INIT_NOT_FINISHED = -10869611;
	public const int ERROR_PARAMS_ERROR = -10869612;

	public const int EVENT_ACCOUNT_LOGOUT = 1;
	
	
	public const string MESSAGE_RECEIVER_NAME = "ghome_unity_message_receiver";
	public const int ERROR_CODE_SDK_ALREADY_INITIALIZED = -1023001;
	public const int ERROR_CODE_SDK_NEED_INITIALIZE = -1023002;
	public const string MESSAGE_SDK_ALREADY_INITIALIIZED = "SDK has already been initialized.";
	public const string MESSAGE_SDK_NEED_INITIALIZE = "SDK not initialized, please do init first.";
}
