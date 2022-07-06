
using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class CsvManager
{
    private static string reportDirectoryName = "Report";
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[]
    {
        "time",
        "milli_seconds",
        "Probe_Position_x",
        "Probe_Position_y",
        "Probe_Position_z",
        "Probe_Rotation_x",
        "Probe_Rotation_y",
        "Probe_Rotation_z",
        "EyeGaze_HitPos_x",
        "EyeGaze_HitPos_y",
        "EyeGaze_HitPos_z",
        "EyeGaze_Hit_GameObject_Name",
        "Hand_Palm_Position_x",
        "Hand_Palm_Position_y",
        "Hand_Palm_Position_z",
        "Hand_Wrist_Position_x",
        "Hand_Wrist_Position_y",
        "Hand_Wrist_Position_z",
        "Head_Position_x",
        "Head_Position_y",
        "Head_Position_z",
        "Head_Rotation_x",
        "Head_Rotation_y",
        "Head_Rotation_z",
    };

    private static string timeStampHeader = "time stamp";

    public static void VerifyDirectory()
    {
        string directory = GetDirectoryPath();

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public static void AppendToReport(List<UserInteractionData> userInteractionData, string reportName)
    {
        VerifyDirectory();
        
        using (StreamWriter streamWriter = File.AppendText(GetFilePath(reportName)))
        {
            VerifyDirectory();
            VerifyFile(reportName);
           
            streamWriter.WriteLine(reportHeaders[0]
                + reportSeparator + reportHeaders[1]
                + reportSeparator + reportHeaders[2]
                + reportSeparator + reportHeaders[3]
                + reportSeparator + reportHeaders[4]
                + reportSeparator + reportHeaders[5]
                + reportSeparator + reportHeaders[6]
                + reportSeparator + reportHeaders[7]
                + reportSeparator + reportHeaders[8]
                + reportSeparator + reportHeaders[9]
                + reportSeparator + reportHeaders[10]
                + reportSeparator + reportHeaders[11]
                + reportSeparator + reportHeaders[12]
                + reportSeparator + reportHeaders[13]
                + reportSeparator + reportHeaders[14]
                + reportSeparator + reportHeaders[15]
                + reportSeparator + reportHeaders[16]
                + reportSeparator + reportHeaders[17]
                + reportSeparator + reportHeaders[18]
                + reportSeparator + reportHeaders[19]
                + reportSeparator + reportHeaders[20]
                + reportSeparator + reportHeaders[21]
                + reportSeparator + reportHeaders[22]
                + reportSeparator + reportHeaders[23]);

            foreach (var currentUserInteractionData in userInteractionData)
            {
                var finalString = currentUserInteractionData.Time + reportSeparator
                    + currentUserInteractionData.Milliseconds + reportSeparator
                    + currentUserInteractionData.ClariusPosX + reportSeparator
                    + currentUserInteractionData.ClariusPosY + reportSeparator
                    + currentUserInteractionData.ClariusPosZ + reportSeparator
                    + currentUserInteractionData.ClariusRotationX + reportSeparator
                    + currentUserInteractionData.ClariusRotationY + reportSeparator
                    + currentUserInteractionData.ClariusRotationZ + reportSeparator
                    + currentUserInteractionData.EyeGazeHitPosUSPlaneX + reportSeparator
                    + currentUserInteractionData.EyeGazeHitPosUSPlaneY + reportSeparator
                    + currentUserInteractionData.EyeGazeHitPosUSPlaneZ + reportSeparator
                    + currentUserInteractionData.EyeGazeHitGameObject + reportSeparator
                    + currentUserInteractionData.PalmPosition.x + reportSeparator
                    + currentUserInteractionData.PalmPosition.y + reportSeparator
                    + currentUserInteractionData.PalmPosition.z + reportSeparator
                    + currentUserInteractionData.WristPosition.x + reportSeparator
                    + currentUserInteractionData.WristPosition.y + reportSeparator
                    + currentUserInteractionData.WristPosition.z + reportSeparator
                    + currentUserInteractionData.HeadPos.x + reportSeparator
                    + currentUserInteractionData.HeadPos.y + reportSeparator
                    + currentUserInteractionData.HeadPos.z + reportSeparator
                    + currentUserInteractionData.HeadRotation.x + reportSeparator
                    + currentUserInteractionData.HeadRotation.y + reportSeparator
                    + currentUserInteractionData.HeadRotation.z;

                streamWriter.WriteLine(finalString);
            }
        }
    }

    public static void CreateReport(string reportName)
    {
        VerifyDirectory();

        using (StreamWriter streamWriter = File.CreateText(GetFilePath(reportName)))
        {
            string finalString = "";

            for (int i = 0; i < reportHeaders.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportHeaders[i];
                }

                streamWriter.WriteLine(finalString);
            }
        }
    }
    public static void VerifyFile(string reportName)
    {
        string file = GetFilePath(reportName);

        if (!File.Exists(file))
        {
            CreateReport(reportName);
        }
    }

    private static string GetDirectoryPath()
    {
        return Application.persistentDataPath + "/" + reportDirectoryName;
    }
  
    private static string GetFilePath(string reportName)
    {
        return GetDirectoryPath() + "/" + reportName;
    }

    private static string GetTimeStamp()
    {
        return System.DateTime.UtcNow.ToString();
    }
}
