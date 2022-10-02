using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedRunner.Utilities
{

	[ExecuteInEditMode]
	public class PathDefinition : MonoBehaviour
	{

		[SerializeField]
		protected List<PathPoint> m_Points;
		[SerializeField]
		protected bool Ism_UseGlobalDelay = false;
		[SerializeField]
		protected float m_GlobalDelay = 0f;
		[SerializeField]
		protected bool Ism_ContinueToStart = false;

		protected int m_CurrentPointIndex = 0;

		public virtual List<PathPoint> Points {
			get {
				return m_Points;
			}
		}

		public virtual bool UseGlobalDelay {
			get {
				return Ism_UseGlobalDelay;
			}
		}

		public virtual float GlobalDelay {
			get {
				return m_GlobalDelay;
			}
		}

		public virtual bool ContinueToStart {
			get {
				return Ism_ContinueToStart;
			}
		}

		public virtual int CurrentPointIndex {
			get {
				return m_CurrentPointIndex;
			}
		}

		#if UNITY_EDITOR
		void OnEnable ()
		{
			if (m_Points != null)
				return;
			m_Points = new List<PathPoint>();
		}
		#endif

		#if UNITY_EDITOR
		void Update ()
		{
			if (transform.childCount != m_Points.Count)
				return;
			m_Points.Clear();
			ForLoopSetChildAndPoint();
		}
		#endif
		public void ForLoopSetChildAndPoint()
		{
			for ( int index = 0; index < transform.childCount; index++ )
			{
				Transform child = transform.GetChild ( index );
				PathPoint point = child.GetComponent<PathPoint> ();
				TryPointNull(point);
			}
		}
		public void TryPointNull(PathPoint point)
		{
			if (point == null)
				return;
			m_Points.Add ( point );
		}
		public IEnumerator<PathPoint> GetPathEnumeratorRoutine ()
		{
			// Exit when points count is smaller one
			if (IsOneOfThisTrue_m_PointsNull_m_PointsCountLessthan1())
				yield break;
			var DirectionData = new DirectionData(-1,0);
			m_CurrentPointIndex = DirectionData.index;
			whileLoopm_CurrentPointIndexRoutine(DirectionData);
		}
		bool IsOneOfThisTrue_m_PointsNull_m_PointsCountLessthan1()
        {
			return (m_Points == null || m_Points.Count < 1);
		}
		public IEnumerable whileLoopm_CurrentPointIndexRoutine(DirectionData DirectionData)
		{
			while ( true )
			{
				yield return m_Points [DirectionData.index];
				if ( m_Points.Count == 1)
					continue;
				IsIndexMorethanZero(DirectionData);
				IsIndexEqual_m_PointsDeleteOneAndZero(DirectionData);
				m_CurrentPointIndex = DirectionData.index;
			}
		}
		
		public void IsIndexMorethanZero(DirectionData DirectionData)
		{
			DirectionPlus(DirectionData);
			DirectionMinus(DirectionData);
		}
		public void DirectionPlus(DirectionData DirectionData)
        {
			if (DirectionData.index <= 0)
			{
				DirectionData.direction = 1;
				return;
			}
		}
		public void DirectionMinus(DirectionData DirectionData)
        {
			if (DirectionData.index >= m_Points.Count - 1)
			{
				DirectionData.direction = -1;
				return;
			}
		}
		public void IsIndexEqual_m_PointsDeleteOneAndZero(DirectionData DirectionData)
		{
			if (DirectionData.index == m_Points.Count - 1 && Ism_ContinueToStart)
            {
				DirectionData.index = 0;
				return;
			}
			DirectionData.index = DirectionData.index + DirectionData.direction;
		}

		public void OnDrawGizmos ()
		{
			if ( m_Points == null || m_Points.Count < 2 )
				return;
			ForLoopDrawGizmos();
		}
		public void ForLoopDrawGizmos()
		{
			for ( var i = 1; i < m_Points.Count; i++ )
			{
				Gizmos.DrawLine ( m_Points [ i - 1 ].transform.position, m_Points [ i ].transform.position );
			}
		}

	}

}