using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;
using RedRunner.Utilities;

namespace RedRunner.Enemies
{

	public abstract class Enemy : MonoBehaviour
	{

		public abstract Collider2D Collider2D { get; }

		public abstract void Kill ( Character target );

		//////////////////////Saw Variables//////////////////////

		
		[SerializeField]
		protected Transform targetRotation;
		[SerializeField]
		protected float m_Speed = 1f;
		[SerializeField]
		protected bool m_RotateClockwise = false;
		[SerializeField]
		protected AudioClip m_DefaultSound;
		[SerializeField]
		protected AudioClip m_SawingSound;
		[SerializeField]
		protected AudioSource m_AudioSource;

		////////////////////Mace Variables//////////////////

		[SerializeField]
		protected Animator m_Animator;
		[SerializeField]
		protected PathFollower m_PathFollower;
		[SerializeField]
		protected float m_MaulSpeed = 0.5f;
		[SerializeField]
		protected float m_MaulScale = 0.8f;
		[SerializeField]
		protected ParticleSystem m_ParticleSystem;



	}

}