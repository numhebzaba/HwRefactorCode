using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;

namespace RedRunner.Utilities
{

	public class PathFollower : MonoBehaviour
	{

		[SerializeField]
		protected PathDefinition m_PathDefinition;
		[SerializeField]
		protected float m_Step = 0.1f;
		[SerializeField]
		protected bool m_FollowSpeeds = true;
		[SerializeField]
		protected float m_Speed = 1f;
		[SerializeField]
		protected bool m_FollowDelays = true;
		[SerializeField]
		protected float m_Delay = 0f;
		[SerializeField]
		protected bool m_Smart = false;
		[SerializeField]
		protected Vector3 m_RangeSize = new Vector3 (4f, 4f, 4f);
		[SerializeField]
		protected Vector3 m_RangeOffset = new Vector3 (0f, 0f, 0f);

		protected bool m_Stopped = false;
		protected IEnumerator<PathPoint> m_CurrentPoint;
		protected bool m_IsMovingNext = false;
		protected Vector3 m_LastPosition;
		protected Vector3 m_Velocity;
		protected Vector3 m_SmoothVelocity;
		protected float m_OverTimeSpeed = 0f;

		public virtual bool Stopped {
			get {
				return m_Stopped;
			}
			set {
				m_Stopped = value;
			}
		}

		public virtual Vector3 Velocity {
			get {
				return m_Velocity;
			}
		}

		void Awake ()
		{
			m_Stopped = m_Smart;
		}

		void Start ()
		{
			Ism_PathDefinitionNull();
			setm_CurrentPoint();
			Ism_CurrentPointNull();
			transform.position = m_CurrentPoint.Current.transform.position;
			StartCoroutine (CalcVelocity ());
		}
		public void Ism_PathDefinitionNull()
		{
			if (m_PathDefinition == null) {
				return;
			}
		}
		public void Ism_CurrentPointNull()
		{
			if (m_CurrentPoint.Current == null)
				return;
		}
		public void setm_CurrentPoint()
        {
			m_CurrentPoint = m_PathDefinition.GetPathEnumerator();
			m_CurrentPoint.MoveNext();
		}
		void OnDrawGizmos ()
		{
			if (m_Smart) {
				Gizmos.DrawWireCube (transform.position + m_RangeOffset, m_RangeSize);
			}
		}

		void Update ()
		{
			Ifm_Smart();
			if (m_CurrentPoint == null || m_CurrentPoint.Current == null || m_Stopped || !GameManager.Singleton.gameRunning) {
				return;
			}
			float speed = Time.deltaTime * m_CurrentPoint.Current.speed;
			IfMoveType(speed);

			CheckdistanceSquared();
		}
		public void Ifm_Smart()
		{
			if (m_Smart) {
				Collider2D[] colliders = Physics2D.OverlapBoxAll (transform.position + m_RangeOffset, m_RangeSize, 0f, LayerMask.GetMask ("Characters"));
				LoopColliderArrayCharacter(colliders);
				return;
			}
			m_Stopped = false;
		}
		public void LoopColliderArrayCharacter(Collider2D[] colliders)
        {
			for (int index = 0; index < colliders.Length; index++)
			{
				Character character = colliders[index].GetComponent<Character>();
				if (character != null)
					m_Stopped = false;
			}
		}
		public void IfMoveType(float speed)
		{
			MoveType_MoveTowards(speed);
			MoveType_Lerp(speed);
			MoveType_SmoothDamp(speed);
			MoveType_Acceleration(speed);
		}
		public void MoveType_MoveTowards(float speed)
        {
			if (m_CurrentPoint.Current.moveType == PathPoint.MoveType.MoveTowards)
			{
				transform.position = Vector3.MoveTowards(transform.position, m_CurrentPoint.Current.transform.position, speed);
				return;
			}
		}
		public void MoveType_Lerp(float speed)
        {
			if (m_CurrentPoint.Current.moveType == PathPoint.MoveType.Lerp)
			{
				transform.position = Vector3.Lerp(transform.position, m_CurrentPoint.Current.transform.position, speed);
				return;
			}
		}
		public void MoveType_SmoothDamp(float speed)
        {
			if (m_CurrentPoint.Current.moveType == PathPoint.MoveType.SmoothDamp)
			{
				transform.position = Vector3.SmoothDamp(transform.position, m_CurrentPoint.Current.transform.position,
					ref m_SmoothVelocity, m_CurrentPoint.Current.smoothTime);
				return;
			}
		}
		public void MoveType_Acceleration(float speed)
        {
			if (m_CurrentPoint.Current.moveType == PathPoint.MoveType.Acceleration)
			{
				Vector3 direction = (m_CurrentPoint.Current.transform.position - transform.position).normalized;
				transform.position = Vector3.MoveTowards(transform.position, m_CurrentPoint.Current.transform.position, m_OverTimeSpeed);
				m_OverTimeSpeed += m_CurrentPoint.Current.acceleration;
				m_OverTimeSpeedMorethanMaxSpeed();
			}
		}
		public void m_OverTimeSpeedMorethanMaxSpeed()
        {
			if (m_OverTimeSpeed > m_CurrentPoint.Current.maxSpeed)
				m_OverTimeSpeed = m_CurrentPoint.Current.maxSpeed;
		}
		public void CheckdistanceSquared()
        {
			var distanceSquared = (transform.position - m_CurrentPoint.Current.transform.position).sqrMagnitude;
			distanceSquaredMorethanAream_step(distanceSquared);
		}
		public void distanceSquaredMorethanAream_step(float distanceSquared)
        {
			if (distanceSquared < m_Step * m_Step)
			{
				m_Stopped = true;
				StopMove();
			}
		}
		public void StopMove()
        {
			if (!m_IsMovingNext)
			{
				StartCoroutine(MoveNext());
			}
		}

		IEnumerator CalcVelocity ()
		{
			while (Application.isPlaying) {
				m_LastPosition = transform.position;
				yield return new WaitForEndOfFrame ();
				m_Velocity = (m_LastPosition - transform.position) / Time.deltaTime;
			}
		}

		IEnumerator MoveNext ()
		{
			m_IsMovingNext = true;
			float delay = m_CurrentPoint.Current.delay;
			Ism_FollowDelaysEqualUseGlobalDelay(delay);
			yield return new WaitForSeconds (delay);
			m_OverTimeSpeed = 0f;
			m_CurrentPoint.MoveNext ();
			m_IsMovingNext = false;
		}
		public void Ism_FollowDelaysEqualUseGlobalDelay(float delay)
		{
			if (m_FollowDelays && m_PathDefinition.UseGlobalDelay) {
				delay = m_PathDefinition.GlobalDelay;
				return;
			}
			FollowDelay(delay);
		}
		public void FollowDelay(float delay)
        {
			if (!m_FollowDelays)
			{
				delay = m_Delay;
				return;
			}
		}

	}

}