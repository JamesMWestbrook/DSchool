using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.BA;
using UnityEngine.Rendering;
using System.IO.Compression;
using System.IO;

[CreateAssetMenu(fileName = "Project Build Zip & Copy", menuName = "Build Automator/Build Scripts/ Project Build Zip & Copy")]
public class CopyDirectoryScript : BuildScript
{
    public override int ArgumentCount => 2;

    public override void Execute(string[] args)
    {
        string src = args[0];
        string dst = args[1];

        if(File.Exists(dst))
        {
            File.Delete(dst);
        }
        ZipFile.CreateFromDirectory(src, dst);
    }

    public override string GetArgumentName(int argIndex)
    {
        switch ((argIndex))
        {
            case 0:
                return "Source";
            case 1:
                return "Destination";
        }
        return base.GetArgumentName(argIndex);
    }
}