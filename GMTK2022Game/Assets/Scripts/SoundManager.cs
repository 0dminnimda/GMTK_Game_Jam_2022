// using System.Collections;
// using System.Collections.Generic;
// using System.Reflection;
// using V_AnimationSystem;
// using UnityEngine;


// public static class SoundManager {
//     public enum Sound {

//     }

//     public static void PlaySound(Sound sound) {
//         GameObject soundGameObject = new GameObject("Sound");
//         AudioSource audioSource = soundGameObject.AddComponent<audioSource>();
//         audioSource.PlayOneShot(GetAudioClip(sound));
//     }

//     private static AudioClip GetAudioClip(Sound sound) {
//         foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray) {
//             if (soundAudioClip.sound == sound)
//                 return soundAudioClip.audioClip;
//         }
//         Debug.LogError("Sound " + sound + " not found!");
//         return null;
//     }
// }