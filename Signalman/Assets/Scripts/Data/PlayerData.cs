using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField, Range(1, 10)] public float MaxJumpHeight { get; private set; }
    [field: SerializeField, Range(1, 10)] public float MaxJumpTime { get; private set; }
    [field: SerializeField, Range(0, 40)] public float Speed { get; private set; }
    [field: SerializeField, Range(0, 1)] public float RotateSpeed { get; private set; }
    [field: SerializeField] public List<AudioClip> StepSounds { get; private set; }
    [field: SerializeField] public AudioClip JumpSounds { get; private set; }
    [field: SerializeField] public AudioClip LandSounds { get; private set; }
}
