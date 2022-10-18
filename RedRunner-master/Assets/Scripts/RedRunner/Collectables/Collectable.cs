using RedRunner.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedRunner.Collectables
{

	[RequireComponent (typeof(SpriteRenderer))]
	[RequireComponent (typeof(Collider2D))]
	[RequireComponent (typeof(Animator))]
	public abstract class Collectable : MonoBehaviour
	{

		public const string COLLECT_TRIGGER = "Collect";

		public abstract SpriteRenderer SpriteRenderer { get; }

		public abstract Collider2D Collider2D { get; }

		public abstract Animator Animator { get; }

		public abstract bool UseOnTriggerEnter2D { get; set; }

		public abstract void OnTriggerEnter2D (Collider2D other);

		public abstract void OnCollisionEnter2D (Collision2D collision2D);

		public abstract void Collect ();

		//-------------------For Chest Script------------------------//

		[SerializeField]
		protected Animator m_Animator;
		[SerializeField]
		protected Collider2D m_Collider2D;
		[SerializeField]
		protected SpriteRenderer m_SpriteRenderer;
		[SerializeField]
		protected bool m_UseOnTriggerEnter2D = true;
		[SerializeField]
		protected int m_MinimumCoins = 5;
		[SerializeField]
		protected int m_MaximumCoins = 10;
		[SerializeField]
		protected CoinRigidbody2D m_CoinRigidbody2D;
		[SerializeField]
		protected Transform m_SpawnPoint;
		[SerializeField]
		protected ParticleSystem m_ParticleSystem;
		[SerializeField]
		[Range(-100f, 100f)]
		protected float m_RandomForceYMinimum = -10f;
		[SerializeField]
		[Range(-100f, 100f)]
		protected float m_RandomForceYMaximum = 10f;
		[SerializeField]
		[Range(-100f, 100f)]
		protected float m_RandomForceXMinimum = -10f;
		[SerializeField]
		[Range(-100f, 100f)]
		protected float m_RandomForceXMaximum = 10f;

		protected Character m_CurrentCharacter;

	}

}