using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    [CreateAssetMenu(menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        public float CarSpeed;

        public float BulletProjectileSpeed;
        public float RocketProjectileSpeed;
       
        [Header("Camera")]
        public Vector3 CameraOffset;
        public Vector3 CameraRotation;
        public Vector3 StartCameraOffset;
        public Vector3 StartCameraRotation;
        public int CurrentCameraSetupID;
        public CameraSetup[] CameraSetups;
        public float CameraFollowSpeed = 8f;

        [Header("Monetization")]
        public float InterstitialAdsCooldown = 30f;
        public float FirstInterstitialAdsCooldown = 300f;
    }

    [Serializable]
    public struct CameraSetup
    {
        public Vector3 CameraOffset;
        public Vector3 CameraRotation;
    }

}
