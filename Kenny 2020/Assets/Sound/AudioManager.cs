using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour{

	public Sound[] soundClip;

	void Awake(){
		foreach (Sound s in soundClip) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.spatialBlend = s.spatial_blend;

			s.source.playOnAwake = s.PlayOnAwake;
			s.source.loop = s.loop;
		}
	}

	void Start(){
		//Play ("Zombie_Walk");
	}

	public void Play(string name){
		Sound s = Array.Find (soundClip, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("Sound " + name + " Not Found");
			return;
		}
			
		s.source.Play ();
	}

	public void PlayOnceAtATime(string name) {
		Sound s = Array.Find(soundClip, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound " + name + " Not Found");
			return;
		}
		else {
			if (s.source.isPlaying == false)
			{
				s.source.Play();
			}
		}
		
		
	}
		
	public void Stop(string name){
		Sound s = Array.Find (soundClip, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("Sound " + name + " Not Found");
			return;
		}

		s.source.Stop ();
	}

	public void PlayDialogue(string name){
		Sound s = Array.Find (soundClip, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning ("Sound " + name + " Not Found");
			return;
		}

		s.source.Play ();
		StartCoroutine (showDialogue (s.clip.length, s.dialogue));
	}

	IEnumerator showDialogue(float time, string dialog){
		GameObject.FindGameObjectWithTag ("DialogText").GetComponent<Text> ().text = dialog;
		GameObject.FindGameObjectWithTag ("DialogText").GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds (time);
		GameObject.FindGameObjectWithTag ("DialogText").GetComponent<Text> ().text = "";
		GameObject.FindGameObjectWithTag ("DialogText").GetComponent<Text>().enabled = false;
	}
}
