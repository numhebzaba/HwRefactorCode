using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;

namespace RedRunner.Collectables
{
	public class Coin : Collectable
	{
		

		public override SpriteRenderer SpriteRenderer {
			get {
				return m_SpriteRenderer;
			}
		}

		public override Animator Animator {
			get {
				return m_Animator;
			}
		}

		public override Collider2D Collider2D {
			get {
				return m_Collider2D;
			}
		}

		public override bool UseOnTriggerEnter2D {
			get {
				return m_UseOnTriggerEnter2D;
			}
			set {
				m_UseOnTriggerEnter2D = value;
			}
		}

		public override void OnTriggerEnter2D (Collider2D other)
		{
			Character character = other.GetComponent<Character> ();
			if (m_UseOnTriggerEnter2D && character != null) {
				Collect ();
			}
		}

		public override void OnCollisionEnter2D (Collision2D collision2D)
		{
			Character character = collision2D.collider.GetComponent<Character> ();
			if (!m_UseOnTriggerEnter2D && character != null) {
				Collect ();
			}
		}

		public override void Collect ()
		{
            GameManager.Singleton.m_Coin.Value++;
			m_Animator.SetTrigger (COLLECT_TRIGGER);
			m_ParticleSystem.Play ();
			m_SpriteRenderer.enabled = false;
			m_Collider2D.enabled = false;
			Destroy (gameObject, m_ParticleSystem.main.duration);
			AudioManager.Singleton.PlayCoinSound (transform.position);
		}
	}
}