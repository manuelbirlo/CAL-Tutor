using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteractionData 
{
    public string Time { get; set; }
    
    public int Milliseconds { get; set; }
  
    public double ClariusPosX { get; set; }

    public double ClariusPosY { get; set; }

    public double ClariusPosZ { get; set; }

    public double ClariusRotationX { get; set; }

    public double ClariusRotationY { get; set; }

    public double ClariusRotationZ { get; set; }

    public double EyeGazeHitPosUSPlaneX { get; set; }
   
    public double EyeGazeHitPosUSPlaneY { get; set; }

    public double EyeGazeHitPosUSPlaneZ { get; set; }

    public string EyeGazeHitGameObject { get; set; }

    public Vector3 HeadPos { get; set; }

    public Quaternion HeadRotation { get; set; }

    #region Hand joints

    public Vector3 ThumbTipPosition { get; set; }

    public Vector3 IndexTipPosition { get; set; }

    public Vector3 MiddleTipPosition { get; set; }

    public Vector3 RingTipPosition { get; set; }

    public Vector3 PinkyTipPosition { get; set; }

    public Vector3 PalmPosition { get; set; }

    public Vector3 WristPosition { get; set; }

    #endregion
}
