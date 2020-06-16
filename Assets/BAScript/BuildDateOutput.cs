using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.BA;
using System.IO;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "Build Data Output", menuName = "Build Automator/Build Scripts/ Build Data Output")]
public class BuildDateOutput : BuildScript
{
    public override int ArgumentCount => 2;

    public override void Execute(string[] args)
    {
        string path = args[0];
        string info = args[1];

        Debug.Log(info);

        string line = null;
        List<string> lines = new List<string>();
        StreamReader reader = new StreamReader(path);
        

        while((line = reader.ReadLine()) != null)
        {
            lines.Add(line);
        }

        reader.Close();
        lines.Insert(0, info);

        StreamWriter writer = new StreamWriter(path);
        for(int i = 0; i < lines.Count; i++)
        {
            writer.WriteLine(lines[i]);
        }
        writer.Close();

    }

    public override string GetArgumentName(int argIndex)
    {
        switch ((argIndex))
        {
            case 0:
                return "Path.txt";
            case 1:
                return "Printed info ex #DATE#";
        }
        return base.GetArgumentName(argIndex);
    }
}
