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
		protected bool Ism_FollowSpeeds = true;
		[SerializeField]
		protected float m_Speed = 1f;
		[SerializeField]
		protected bool Ism_FollowDelays = true;
		[SerializeField]
		protected float m_Delay = 0f;
		[SerializeField]
		protected bool Ism_Smart = false;
		[SerializeField]
		protected Vector3 m_RangeSize = new Vector3 (4f, 4f, 4f);
		[SerializeField]
		protected Vector3 m_RangeOffset = new Vector3 (0f, 0f, 0f);

		protected bool Ism_Stopped = false;
		protected IEnumerator<PathPoint> m_CurrentPoint;
		protected bool Ism_MovingNext = false;
		protected Vector3 m_LastPosition;
		protected Vector3 m_Velocity;
		protected Vector3 m_SmoothVelocity;
		protected float m_OverTimeSpeed = 0f;

		public virtual bool IsStopped {
			get {
				return Ism_Stopped;
			}
			set {
				Ism_Stopped = value;
			}
		}

		public virtual Vector3 Velocity {
			get {
				return m_Velocity;
			}
		}

		void Awake ()
		{
			Ism_Stopped = Ism_Smart;
		}

		void Start ()
		{
			TryCheckIsm_PathDefinitionNull();
			Setm_CurrentPoint();
			TryCheckIsm_CurrentPointNull();
			transform.position = m_CurrentPoint.Current.transform.position;
			StartCoroutine (CalcVelocityRoutine ());
		}
		public void TryCheckIsm_PathDefinitionNull()
		{
			if (m_PathDefinition == null) {
				return;
			}
		}
		public void Setm_CurrentPoint()
		{
			m_CurrentPoint = m_PathDefinition.GetPathEnumeratorRoutine();
			m_CurrentPoint.MoveNext();
		}
		public void TryCheckIsm_CurrentPointNull()
		{
			if (m_CurrentPoint.Current == null)
				return;
		}
		void OnDrawGizmos ()
		{
			if (Ism_Smart) {
				Gizmos.DrawWireCube (transform.position + m_RangeOffset, m_RangeSize);
			}
		}

		void Update ()
		{
			Ifm_Smart();
			if (IsOneOfthisTrue_CurrentPointNull_CurrentPointCurrentNull_Ism_Stopped_SingletonGameRunning()) 
				return;
			float speed = Time.deltaTime * m_CurrentPoint.Current.speed;
			_4MoveType(speed);
			CheckDistanceSquared();
		}
		public void Ifm_Smart()
		{
			if (Ism_Smart) {
				Collider2D[] colliders = Physics2D.OverlapBoxAll (transform.position + m_RangeOffset, m_RangeSize, 0f, LayerMask.GetMask ("Characters"));
				LoopColliderArrayCharacter(colliders);
				return;
			}
			Ism_Stopped = false;
		}
		bool IsOneOfthisTrue_CurrentPointNull_CurrentPointCurrentNull_Ism_Stopped_SingletonGameRunning()
        {
			return (m_CurrentPoint == null || m_CurrentPoint.Current == null || Ism_Stopped || !GameManager.Singleton.gameRunning);

		}
		public void LoopColliderArrayCharacter(Collider2D[] colliders)
        {
			for (int index = 0; index < colliders.Length; index++)
			{
				Character character = colliders[index].GetComponent<Character>();
				if (character == null)
					return;
				Ism_Stopped = false;
			}
		}
		public void _4MoveType(float speed)
		{
			TryMoveType_MoveTowards(speed);
			TryMoveType_Lerp(speed);
			TryMoveType_SmoothDamp(speed);
			TryMoveType_Acceleration(speed);
		}
		public void TryMoveType_MoveTowards(float speed)
        {
			if (IsMoveType_MoveTowards())
			{
				transform.position = Vector3.MoveTowards(transform.position, m_CurrentPoint.Current.transform.position, speed);
				return;
			}
		}
		bool IsMoveType_MoveTowards()
        {
			return m_CurrentPoint.Current.moveType == PathPoint.MoveType.MoveTowards;
		}
		public void TryMoveType_Lerp(float speed)
        {
			if (IsMoveType_Lerp())
			{
				transform.position = Vector3.Lerp(transform.position, m_CurrentPoint.Current.transform.position, speed);
				return;
			}
		}
		bool IsMoveType_Lerp()
		{
			return m_CurrentPoint.Current.moveType == PathPoint.MoveType.Lerp;
		}
		public void TryMoveType_SmoothDamp(float speed)
        {
			if (IsMoveType_SmoothDamp())
			{
				transform.position = Vector3.SmoothDamp(transform.position, m_CurrentPoint.Current.transform.position,
					ref m_SmoothVelocity, m_CurrentPoint.Current.smoothTime);
				return;
			}
		}
		bool IsMoveType_SmoothDamp()
		{
			return m_CurrentPoint.Current.moveType == PathPoint.MoveType.SmoothDamp;
		}
		public void TryMoveType_Acceleration(float speed)
        {
			if (IsMoveType_Acceleration())
			{
				Vector3 direction = (m_CurrentPoint.Current.transform.position - transform.position).normalized;
				transform.position = Vector3.MoveTowards(transform.position, m_CurrentPoint.Current.transform.position, m_OverTimeSpeed);
				m_OverTimeSpeed += m_CurrentPoint.Current.acceleration;
				m_OverTimeSpeedMorethanMaxSpeed();
			}
		}
		bool IsMoveType_Acceleration()
		{
			return m_CurrentPoint.Current.moveType == PathPoint.MoveType.Acceleration;
		}
		public void m_OverTimeSpeedMorethanMaxSpeed()
        {
			if (m_OverTimeSpeed > m_CurrentPoint.Current.maxSpeed)
				m_OverTimeSpeed = m_CurrentPoint.Current.maxSpeed;
		}
		public void CheckDistanceSquared()
        {
			var distanceSquared = (transform.position - m_CurrentPoint.Current.transform.position).sqrMagnitude;
			TryDistanceSquaredMorethanAream_step(distanceSquared);
		}
		public void TryDistanceSquaredMorethanAream_step(float distanceSquared)
        {
			if (distanceSquared < m_Step * m_Step)
			{
				Ism_Stopped = true;
				StopMove();
			}
		}
		public void StopMove()
        {
			if (Ism_MovingNext)
				return;
			StartCoroutine(MoveNextRoutine());
		}

		IEnumerator CalcVelocityRoutine ()
		{
			while (Application.isPlaying) {
				m_LastPosition = transform.position;
				yield return new WaitForEndOfFrame ();
				m_Velocity = (m_LastPosition - transform.position) / Time.deltaTime;
			}
		}

		IEnumerator MoveNextRoutine ()
		{
			Ism_MovingNext = true;
			float delay = m_CurrentPoint.Current.delay;
			Trym_IsFollowDelaysEqualUseGlobalDelay(delay);
			yield return new WaitForSeconds (delay);
			m_OverTimeSpeed = 0f;
			m_CurrentPoint.MoveNext ();
			Ism_MovingNext = false;
		}
		public void Trym_IsFollowDelaysEqualUseGlobalDelay(float delay)
		{
			if (Is_Ism_FollowDelaysEqualUseGlobalDelay()) {
				delay = m_PathDefinition.GlobalDelay;
				return;
			}
			FollowDelay(delay);
		}
		bool Is_Ism_FollowDelaysEqualUseGlobalDelay()
        {
			return Ism_FollowDelays && m_PathDefinition.UseGlobalDelay;
		}
		public void FollowDelay(float delay)
        {
			if (Ism_FollowDelays)
				return;
			delay = m_Delay;
		}

	}

}