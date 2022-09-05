using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedRunner.Utilities
{

	public class GroundCheck : MonoBehaviour
	{

		public delegate void GroundedHandler ();

		public event GroundedHandler OnGrounded;

		public const string GROUND_TAG = "Ground";
		public const string GROUND_LAYER_NAME = "Ground";

		[SerializeField]
		private Collider2D m_Collider2D;

		[SerializeField]
		private float m_RayDistance = 0.5f;

		public bool IsGrounded { get { return m_IsGrounded; } }

		private bool m_IsGrounded = false;

		void Awake ()
		{
			m_IsGrounded = false;
		}

		void Update ()
		{
			Vector2 left = new Vector2 (m_Collider2D.bounds.max.x, m_Collider2D.bounds.center.y);
			Vector2 center = new Vector2 (m_Collider2D.bounds.center.x, m_Collider2D.bounds.center.y);
			Vector2 right = new Vector2 (m_Collider2D.bounds.min.x, m_Collider2D.bounds.center.y);
		
			DebugAllRaycast(left, center, right);

			
		}
		public void DebugAllRaycast(Vector2 left,Vector2 center,Vector2 right)
		{
			bool grounded1 = false,grounded2 = false, grounded3 = false;
			DebugEachRaycast(left,center,right,grounded1,grounded2,grounded3);
			bool grounded = grounded1 || grounded2 || grounded3;
			checkGrounded(grounded);
		}
		public void DebugEachRaycast(Vector2 left,Vector2 center,Vector2 right,bool grounded1,bool grounded2,bool grounded3)
		{
			DebugOneRaycast(left,grounded1);
			DebugOneRaycast(center,grounded2);
			DebugOneRaycast(right,grounded3);
		}
		public void DebugOneRaycast(Vector2 direction,bool groundedIndex)
		{
			RaycastHit2D hit1 = Physics2D.Raycast (direction, new Vector2 (0f, -1f), m_RayDistance, LayerMask.GetMask (GROUND_LAYER_NAME));
			Debug.DrawRay (direction, new Vector2 (0f, -m_RayDistance));
			groundedIndex = hit1 != null && hit1.collider != null && hit1.collider.CompareTag (GROUND_TAG);
		}
		public void checkGrounded(bool grounded)
		{
			if (grounded && !m_IsGrounded) {
				if (OnGrounded != null) {
					OnGrounded ();
				}
			}
			m_IsGrounded = grounded;
		}

	}

}