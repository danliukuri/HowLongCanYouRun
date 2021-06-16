using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
	public class AudioManager : MonoBehaviour
	{
		#region Fields
		static AudioManager instance;

		static List<AudioSource> audioSources;
		static GameObject tempAudioSourcesParent;
		#endregion

		#region Methods
		void Awake()
		{
			if (instance)
			{
				Destroy(gameObject);
			}
			else
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
				audioSources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
			}
		}

		public static void Play(string audioName)
		{
			AudioSource audioSource = FindAudioSourceOrCloneWhen(audioName, (audio) => audio.isPlaying || audio.IsPaused());
			audioSource.Play();
		}
		public static void PlayInPosition(string audioName, Vector3 position)
		{
			AudioSource audioSource = FindAudioSourceOrCloneWhen(audioName, (audio) => audio.isPlaying || audio.IsPaused());
			audioSource.transform.position = position;
			audioSource.Play();
		}
		public static void PlayFollowingTheTarget(string audioName, Transform target)
		{
			if (target is null)
				throw new ArgumentNullException(nameof(target));

			AudioSource audioSource = FindAudioSourceOrCloneWhen(audioName, (audio) => audio.isPlaying || audio.IsPaused());
			instance.StartCoroutine(StaticFunctions.DoWhile<WaitForFixedUpdate>(() =>
				audioSource.transform.position = target.position, () => audioSource.isPlaying));
			audioSource.Play();
		}
		public static void FadeInAndPlay(string audioName, float speed)
		{
			AudioSource audioSource = FindAudioSourceOrCloneWhen(audioName, (audio) => audio.isPlaying || audio.IsPaused());
			instance.StartCoroutine(FadeIn(audioSource, () => audioSource.Play(), speed)); 
		}

		public static void FadeOutAndPause(string audioName, float speed)
		{
			AudioSource audioSource = FindPlayingAudioSource(audioName);
			instance.StartCoroutine(FadeOut(audioSource, () => audioSource.Pause(), speed));
		}
		public static void FadeOutAndStop(string audioName, float speed)
		{
			AudioSource audioSource = FindPlayingAudioSource(audioName);
			instance.StartCoroutine(FadeOut(audioSource, () => audioSource.Stop(), speed));
		}
		public static void FadeInAndUnPause(string audioName, float speed)
		{
			AudioSource audioSource = FindPausedAudioSource(audioName);
			instance.StartCoroutine(FadeIn(audioSource, () => audioSource.UnPause(), speed));
		}

		static IEnumerator FadeOut(AudioSource audioSource, Action action, float speed)
		{
			FadeFunctionParametersValidate(audioSource, action, speed);

			float requiredAudioSourceVolume = audioSource.volume;
			while (audioSource.volume != 0f)
			{
				float newVolume = audioSource.volume - (speed * requiredAudioSourceVolume * Time.deltaTime);
				if (newVolume < 0f)
					newVolume = 0f;
				audioSource.volume = newVolume;
				if(audioSource.volume == 0f)
				{
					action.Invoke();
				}
				yield return new WaitForEndOfFrame();
			}
			audioSource.volume = requiredAudioSourceVolume;
		}
		static IEnumerator FadeIn(AudioSource audioSource, Action action, float speed)
		{
			FadeFunctionParametersValidate(audioSource, action, speed);

			float requiredAudioSourceVolume = audioSource.volume;
			audioSource.volume = 0f;
			action.Invoke();
			while (audioSource.volume != requiredAudioSourceVolume)
			{
				float newVolume = audioSource.volume + (speed * requiredAudioSourceVolume * Time.deltaTime);
				if (newVolume > requiredAudioSourceVolume)
					newVolume = requiredAudioSourceVolume;
				audioSource.volume = newVolume;

				yield return new WaitForEndOfFrame();
			}
		}
		static void FadeFunctionParametersValidate(AudioSource audioSource, Action action, float speed)
		{
			if (audioSource is null)
				throw new ArgumentNullException(nameof(audioSource));
			if (action is null)
				throw new ArgumentNullException(nameof(action));
			if (speed <= 0f)
				throw new ArgumentException("Value must be greater than zero!", nameof(speed));
			if (speed > 1f)
				throw new ArgumentException("The value must be less than or equal to one!", nameof(speed));
		}

		static AudioSource FindAudioSourceOrCloneWhen(string audioName, Predicate<AudioSource> cloneWhen)
		{
			if (audioName is null)
				throw new ArgumentNullException(nameof(audioName));
			if (cloneWhen is null)
				throw new ArgumentNullException(nameof(cloneWhen));

			AudioSource audioSource = audioSources.Find(audio => audio.name == audioName);
			if (audioSource is null)
				throw new ArgumentException("Audio source \"" + audioName + "\" not found!", nameof(audioSource));

			if (cloneWhen.Invoke(audioSource))
			{
				audioSource = audioSources.FindLast(audio => audio.name == audioName && !cloneWhen.Invoke(audioSource)) 
					?? Clone(audioSource);
			}
			return audioSource;
		}
		static AudioSource FindPlayingAudioSource(string audioName)
		{
			if (audioName is null)
				throw new ArgumentNullException(nameof(audioName));
			AudioSource audioSource = audioSources.Find(audio => audio.name == audioName && audio.isPlaying);
			if (audioSource is null)
				throw new ArgumentException("Playing audio source \"" + audioName + "\" not found!", nameof(audioSource));
			return audioSource;
		}
		static AudioSource FindPausedAudioSource(string audioName)
		{
			if (audioName is null)
				throw new ArgumentNullException(nameof(audioName));
			AudioSource audioSource = audioSources.Find(audio => audio.name == audioName && audio.IsPaused());
			if (audioSource is null)
				throw new ArgumentException("Paused audio source \"" + audioName + "\" not found!", nameof(audioSource));
			return audioSource;
		}

		public static void Invoke(Action action, float delayTime) =>
			instance.StartCoroutine(StaticFunctions.Invoke(() => action.Invoke(), delayTime));
		static AudioSource Clone(AudioSource audioSource)
		{
			if (tempAudioSourcesParent is null)
			{
				tempAudioSourcesParent = new GameObject("TempAudioSources");
				tempAudioSourcesParent.transform.SetParent(instance.transform);
			}
			GameObject gameObject = Instantiate(audioSource.gameObject, tempAudioSourcesParent.transform);
			gameObject.name = audioSource.name;

			AudioSource audioSourceClone = gameObject.GetComponent<AudioSource>();
			audioSources.Add(audioSourceClone);
			return audioSourceClone;
		}
		#endregion
	}
}