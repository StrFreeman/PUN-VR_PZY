using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class CmdTry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string cmd = Application.dataPath + "/try.py";
        UnityEngine.Debug.Log(cmd);
        run_cmd(cmd, "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void run_cmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = "C:/Users/Freeman/AppData/Local/Programs/Python/Python311/python.exe";
        start.Arguments = string.Format("{0} {1}", cmd, args);
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd();
                UnityEngine.Debug.Log("result");
                UnityEngine.Debug.Log(result);
            }
        }
    }
}
