using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Scripting.Python;
using UnityEditor;
using System.IO;


public class TestPy : MonoBehaviour
{
    static readonly string textFile = "Response.txt";
    void Start()
    {
        RunPy();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            RunPy();
        }
    }
    private static void RunPy()
    {
        PythonRunner.RunFile($"{Application.dataPath}/Scripts/Python/HelloWorldPythonScript.py");
        //PythonRunner.RunFile($"{Application.dataPath}/Scripts/Python/PyTest.py");
        //string text = System.IO.File.ReadAllText("Assets/Scripts/Python/Response.txt");
        //print(text);
    }

}
