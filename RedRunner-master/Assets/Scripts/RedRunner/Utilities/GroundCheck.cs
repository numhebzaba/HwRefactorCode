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

		public bool IsGrounded { get { return Ism_IsGrounded; } }

		private bool Ism_IsGrounded = false;

		void Awake ()
		{
			Ism_IsGrounded = false;
		}

		void Update ()
		{
			Vector2 left = new Vector2 (m_Collider2D.bounds.max.x, m_Collider2D.bounds.center.y);
			Vector2 center = new Vector2 (m_Collider2D.bounds.center.x, m_Collider2D.bounds.center.y);
			Vector2 right = new Vector2 (m_Collider2D.bounds.min.x, m_Collider2D.bounds.center.y);
			bool grounded1 = false, grounded2 = false, grounded3 = false;
			var GroundCheckData = new GroundCheckData(left, center, right, grounded1, grounded2, grounded3);
			DebugAllRaycast(GroundCheckData);
		}
		public void DebugAllRaycast(GroundCheckData GroundCheckData)
		{
			
			DebugEachRaycast(GroundCheckData);
			bool grounded = GroundCheckData.grounded1 || GroundCheckData.grounded2 || GroundCheckData.grounded3;
			checkGrounded(grounded);
		}
		public void DebugEachRaycast(GroundCheckData GroundCheckData)
		{
			DebugOneRaycast(GroundCheckData.left, GroundCheckData.grounded1);
			DebugOneRaycast(GroundCheckData.center, GroundCheckData.grounded2);
			DebugOneRaycast(GroundCheckData.right, GroundCheckData.grounded3);
		}
		public void DebugOneRaycast(Vector2 direction,bool groundedIndex)
		{
			RaycastHit2D hit1 = Physics2D.Raycast (direction, new Vector2 (0f, -1f), m_RayDistance, LayerMask.GetMask (GROUND_LAYER_NAME));
			Debug.DrawRay (direction, new Vector2 (0f, -m_RayDistance));
			groundedIndex = hit1 != null && hit1.collider != null && hit1.collider.CompareTag (GROUND_TAG);
		}
		public void checkGrounded(bool grounded)
		{
			if (grounded && !Ism_IsGrounded) {
				if (OnGrounded != null)
					OnGrounded ();
			}
			Ism_IsGrounded = grounded;
		}

	}

}