// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos.EyeTracking
{
    public class EyeTest : MonoBehaviour
    {
       
        private void Update()
        {
            var eyeGazeProvider = CoreServices.InputSystem?.EyeGazeProvider;

            Debug.Log(eyeGazeProvider.GazeDirection.normalized);
            if (eyeGazeProvider != null)
            {
                Debug.Log(eyeGazeProvider.GazeDirection.normalized);

            }
        }
    }
}
