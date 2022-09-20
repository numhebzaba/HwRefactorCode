using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;

namespace RedRunner.Enemies
{

	public abstract class Enemy : MonoBehaviour
	{

		public abstract Collider2D Collider2D { get; }

		public abstract void Kill ( Character target );

		[SerializeField]
		protected Collider2D m_Collider2D;
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