using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Scripting.Python;
using UnityEditor;


public class TestPy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PythonRunner.RunFile($"{Application.dataPath}/Scripts/Python/HelloWorldPythonScript.py");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
