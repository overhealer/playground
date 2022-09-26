using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Game Config", fileName = "Game Config")]
public class GameConfigSO : ScriptableObject{
    [Header("Global")]
    public float Gravity;
}
