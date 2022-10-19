using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;

namespace RedRunner.Enemies
{
	public class Saw : Enemy
	{
		[SerializeField]
		private Collider2D m_Collider2D;

		public override Collider2D Collider2D {
			get {
				return m_Collider2D;
			}
		}

		void Start ()
		{
			if (targetRotation == null) {
				targetRotation = transform;
			}
		}

		void Update ()
		{
			Vector3 rotation = targetRotation.rotation.eulerAngles;
			Checkm_RotateWay(rotation);

			targetRotation.rotation = Quaternion.Euler (rotation);
		}

		void Checkm_RotateWay(Vector3 rotation)
        {
			if (!m_RotateClockwise)
			{
				rotation.z += m_Speed;
				return;
			}
			rotation.z -= m_Speed;
		}

		void OnCollisionEnter2D (Collision2D collision2D)
		{
			Character character = collision2D.collider.GetComponent<Character> ();
			if (character != null) {
				Kill (character);
			}
		}

		void OnCollisionStay2D (Collision2D collision2D)
		{
			if (collision2D.collider.CompareTag ("Player")) 
			{
				CheckPlayingSound();
			}
		}

		void CheckPlayingSound()
        {
			if (m_AudioSource.clip != m_SawingSound)
			{
				m_AudioSource.clip = m_SawingSound;
			}
			else if (!m_AudioSource.isPlaying)
			{
				m_AudioSource.Play();
			}
		}

		void OnCollisionExit2D (Collision2D collision2D)
		{
			if (collision2D.collider.CompareTag ("Player")) {
				CheckDefaultSoundIsPlaying();

				m_AudioSource.Play ();
			}
		}

		void CheckDefaultSoundIsPlaying()
        {
			if (m_AudioSource.clip != m_DefaultSound)
			{
				m_AudioSource.clip = m_DefaultSound;
			}
		}

		public override void Kill (Character target)
		{
			target.Die (true);
		}

	}

}