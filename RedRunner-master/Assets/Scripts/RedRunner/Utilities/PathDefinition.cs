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
		protected bool m_UseGlobalDelay = false;
		[SerializeField]
		protected float m_GlobalDelay = 0f;
		[SerializeField]
		protected bool m_ContinueToStart = false;

		protected int m_CurrentPointIndex = 0;

		public virtual List<PathPoint> Points {
			get {
				return m_Points;
			}
		}

		public virtual bool UseGlobalDelay {
			get {
				return m_UseGlobalDelay;
			}
		}

		public virtual float GlobalDelay {
			get {
				return m_GlobalDelay;
			}
		}

		public virtual bool ContinueToStart {
			get {
				return m_ContinueToStart;
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
			if ( m_Points == null )
			{
				m_Points = new List<PathPoint> ();
			}
		}
		#endif

		#if UNITY_EDITOR
		void Update ()
		{
			if ( transform.childCount != m_Points.Count )
			{
				m_Points.Clear ();
				ForLoopSetChildAndPoint();
			}
		}
		#endif
		public void ForLoopSetChildAndPoint()
		{
			for ( int i = 0; i < transform.childCount; i++ )
				{
					Transform child = transform.GetChild ( i );
					PathPoint point = child.GetComponent<PathPoint> ();
					IsPointNull(point);
				}
		}
		public void IsPointNull(PathPoint point)
		{
			if ( point != null )
				m_Points.Add ( point );
		}
		public IEnumerator<PathPoint> GetPathEnumerator ()
		{
			// Exit when points count is smaller one
			if ( m_Points == null || m_Points.Count < 1 )
				yield break;
			var direction = 1;
			var index = 0;
			m_CurrentPointIndex = index;
			whileLoopm_CurrentPointIndex(direction,index);
		}
		public void whileLoopm_CurrentPointIndex(var direction,var index)
		{
			while ( true )
			{
				yield return m_Points [ index ];
				Ism_PointsCountEqualOne();
				IsIndexMorethanZero(direction,index);
				IsIndexEqual_m_PointsDeleteOneAndZero(direction,index);
				m_CurrentPointIndex = index;
			}
		}
		public void Ism_PointsCountEqualOne()
		{
			if ( m_Points.Count == 1 )
				continue;
		}
		public void IsIndexMorethanZero(var direction,var index)
		{
			if ( index <= 0 )
				direction = 1;
			else if ( index >= m_Points.Count - 1 )
				direction = -1;	
		}
		public void IsIndexEqual_m_PointsDeleteOneAndZero(var direction,var index)
		{
			if ( index == m_Points.Count - 1 && m_ContinueToStart )
				index = 0;
			else
				index = index + direction;
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