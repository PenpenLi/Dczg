using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

class DumpManager : Singleton<DumpManager>
{
    string VERSION_CODE = "";
    string UPLOAD_URL = "http://180.96.39.127:89/upload_file.php";

    private List<string> m_Lines = new List<string>();
    private List<string> m_LogTxt = new List<string>();
    private List<string> m_ErrorTxt = new List<string>();
    private string m_strLogPath;
    private string m_strErrorPath;
    private short m_nErrorCount = 0;
    
    public void Initialize()
    {
        if (VERSION_CODE.Length >= 0)
        {
            return;
        }
        if(Application.isEditor)
        {
            VERSION_CODE = "100";
        }
        else
        {
            VERSION_CODE = BundleGlobal.AppVersion()["versionName"].Split('.')[3];
            Debug.Log("BundleGlobal.AppVersion ");
        }

        m_strLogPath = BundleGlobal.LocalPath + "log.txt";
        m_strErrorPath = BundleGlobal.LocalPath + "error.txt";
        if (File.Exists(m_strLogPath))
        {
            File.Delete(m_strLogPath);
        }
        if (File.Exists(m_strErrorPath))
        {
            StartCoroutine(Upload());
        }

        Application.RegisterLogCallback(HandleLog);
    }

    public void Tick()
    {
        if (m_LogTxt.Count > 0)
        {
            using (StreamWriter writer = new StreamWriter(m_strLogPath, true, Encoding.UTF8))
            {
                foreach (string t in m_LogTxt)
                {
                    writer.WriteLine(t);
                }
            }
            m_LogTxt.Clear();
        }
        if (m_ErrorTxt.Count > 0)
        {
            using (StreamWriter writer = new StreamWriter(m_strErrorPath, true, Encoding.UTF8))
            {
                foreach (string t in m_ErrorTxt)
                {
                    writer.WriteLine(t);
                }
            }
            m_ErrorTxt.Clear();
        }
    }

    public void OnGUI()
    {
        GUI.color = Color.red;
        foreach (string line in m_Lines)
        {
            GUILayout.Label(line);
        }
    }

    private void GetSystemInfo()
    {
        string systemInfo = string.Format(
            "Device Model: {0}, " +
            "Device Name: {1},\n" +
            "Device Type: {2}, " +
            "Device Unique Identifier: {3},\n" +
            "Graphics Device ID: {4}, " +
            "Graphics Device Name: {5},\n" +
            "Graphics Device Vendor: {6}, " +
            "Graphics Device Vendor ID: {7},\n" +
            "Graphics Device Version: {8}, " +
            "Graphics Memory Size: {9},\n" +
            "Graphics Pixel Fillrate: {10}, " +
            "Graphics Shader Level: {11}, " +
            "NPot Support: {12},\n" +
            "Operating System: {13}, " +
            "Processor Count: {14},\n" +
            "Processor Type: {15}, " +
            "Supported Render Target Count: {16},\n" +
            "Supports 3D Textures: {17}, " +
            "Supports Accelerometer: {18}, " +
            "Supports Compute Shaders: {19},\n" +
            "Supports Gyroscope: {20}, " +
            "Supports Image Effects: {21}, " +
            "Supports Instancing: {22},\n" +
            "Supports Location Service: {23}, " +
            "Supports Render Textures: {24}, " +
            "Supports Render To Cubemap: {25},\n" +
            "Supports Shadows: {26}, " +
            "Supports Sparse Textures: {27}, " +
            "Supports Stencil: {28},\n" +
            "Supports Vertex Programs: {29}, " +
            "Supports Vibration: {30}, " +
            "System Memory Size: {31},",
            SystemInfo.deviceModel,
            SystemInfo.deviceName,
            SystemInfo.deviceType,
            SystemInfo.deviceUniqueIdentifier,
            SystemInfo.graphicsDeviceID,
            SystemInfo.graphicsDeviceName,
            SystemInfo.graphicsDeviceVendor,
            SystemInfo.graphicsDeviceVendorID,
            SystemInfo.graphicsDeviceVersion,
            SystemInfo.graphicsMemorySize,
            SystemInfo.graphicsPixelFillrate,
            SystemInfo.graphicsShaderLevel,
            SystemInfo.npotSupport,
            SystemInfo.operatingSystem,
            SystemInfo.processorCount,
            SystemInfo.processorType,
            SystemInfo.supportedRenderTargetCount,
            SystemInfo.supports3DTextures,
            SystemInfo.supportsAccelerometer,
            SystemInfo.supportsComputeShaders,
            SystemInfo.supportsGyroscope,
            SystemInfo.supportsImageEffects,
            SystemInfo.supportsInstancing,
            SystemInfo.supportsLocationService,
            SystemInfo.supportsRenderTextures,
            SystemInfo.supportsRenderToCubemap,
            SystemInfo.supportsShadows,
            SystemInfo.supportsSparseTextures,
            SystemInfo.supportsStencil,
            SystemInfo.supportsVertexPrograms,
            SystemInfo.supportsVibration,
            SystemInfo.systemMemorySize
        );
        HandleLog(systemInfo, string.Empty, LogType.Error);
    }

    private void HandleLog(string condition, string stackTrace, LogType type)
    {
        string now = System.DateTime.Now.ToString();
        string prefix = string.Format("{0}[{1}]: ", now, type.ToString());
        int prefixLength = prefix.Length;
        string blankPrefix = string.Empty;
        for (int i = 0; i < prefixLength; i++)
        {
            blankPrefix += " ";
        }
        if (condition.LastIndexOf("\n") == condition.Length - 1)
        {
            condition = condition.Substring(0, condition.Length - 1);
        }
        condition = condition.Replace("\n", "\r\n" + blankPrefix);
        if (type == LogType.Error || type == LogType.Exception)
        {
            if (m_nErrorCount > 100)
            {
                if (File.Exists(m_strErrorPath))
                {
                    File.Delete(m_strErrorPath);
                }
                m_nErrorCount = 0;
            }
            bool bFirstError = m_nErrorCount == 0;
            m_nErrorCount++;
            if (bFirstError)
            {
                GetSystemInfo();
            }
            m_ErrorTxt.Add(prefix + condition);
            Log(condition);
            if (stackTrace != string.Empty)
            {
                if (stackTrace.LastIndexOf("\n") == stackTrace.Length - 1)
                {
                    stackTrace = stackTrace.Substring(0, stackTrace.Length - 1);
                }
                stackTrace = stackTrace.Replace("\n", "\r\n" + blankPrefix);
                m_ErrorTxt.Add(blankPrefix + stackTrace);
                Log(stackTrace);
            }
        }
        else
        {
            m_LogTxt.Add(prefix + condition);
        }
    }

    private void Log(params object[] objs)
    {
        string text = string.Empty;
        for (int i = 0; i < objs.Length; i++)
        {
            if (i > 0)
            {
                text += ", ";
            }
            text += objs[i].ToString();
        }
        if (Application.isPlaying)
        {
            if (m_Lines.Count > 20)
            {
                m_Lines.RemoveAt(0);
            }
            m_Lines.Add(text);
        }
    }

    IEnumerator Upload()
    {
        if (File.Exists(m_strErrorPath))
        {
            string totalFile = string.Empty;
            using (StreamReader reader = new StreamReader(m_strErrorPath, Encoding.UTF8, true))
            {
                totalFile = reader.ReadToEnd();
            }
            File.Delete(m_strErrorPath);
            byte[] buf = Encoding.UTF8.GetBytes(totalFile.ToCharArray());
            buf = CLZF2.Compress(buf);
            WWWForm form = new WWWForm();
            form.AddField("VERSION_CODE", VERSION_CODE);
            form.AddBinaryData("upload_file", buf, "error.txt", "text/plain");
            WWW www = new WWW(UPLOAD_URL, form);
            yield return www;
            if (www.text != null)
            {
                Debug.Log(www.text);
            }
            if (www.error != null)
            {
                Debug.LogError(www.error);
            }
        }
    }
}
