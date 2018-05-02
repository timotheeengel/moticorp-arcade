 /**
 * PSMove API - A Unity5 plugin for the PSMove motion controller.
 *              Derived from the psmove-ue4 plugin by Chadwick Boulay
 *              and the UniMove plugin by the Copenhagen Game Collective
 * Copyright (C) 2016, Guido Sanchez (hipstersloth908@gmail.com)
 * 
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *    1. Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *
 *    2. Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 **/

using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class PSMoveSplash : MonoBehaviour
{
    // PSMove controller ID - 0-based
    public int PSMoveID = 0;

	// Pressed This Frame
    public event EventHandler OnButtonTrianglePressed;
    public event EventHandler OnButtonCirclePressed;
    public event EventHandler OnButtonCrossPressed;
    public event EventHandler OnButtonSquarePressed;
    public event EventHandler OnButtonSelectPressed;
    public event EventHandler OnButtonStartPressed;
    public event EventHandler OnButtonPSPressed;
    public event EventHandler OnButtonMovePressed;

    // Released This Frame
    public event EventHandler OnButtonTriangleReleased;
    public event EventHandler OnButtonCircleReleased;
    public event EventHandler OnButtonCrossReleased;
    public event EventHandler OnButtonSquareReleased;
    public event EventHandler OnButtonSelectReleased;
    public event EventHandler OnButtonStartReleased;
    public event EventHandler OnButtonPSReleased;
    public event EventHandler OnButtonMoveReleased;

    // Used to send and receive controller data from the PSMoveWorker thread
    //private PSMoveDataContext dataContext;

    #region Controller Properties
    
    // Debug
    public bool ShowTrackingDebug;
    public bool ShowHMDFrustumDebug;

    #endregion

    #region Controller Actions
    /// <summary>
    /// Converts the current orientation to the identity orientation
    /// </summary

    /// <summary>
    /// Sets the amount of rumble
    /// </summary>
    /// <param name="rumble">the rumble amount (0-1)</param>
    #endregion

    #region Unity Callbacks
    /// <summary>
    /// NOTE! This function does NOT pair the controller by Bluetooth.
    /// If the controller is not already paired, it can only be connected by USB.
    /// See README for more information.
    /// </summary>
    #endregion
}
