using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedRunner.Utilities
{

	public class CameraController : MonoBehaviour
	{

		public delegate void ParallaxCameraDelegate(Vector3 deltaMovement);
		
		public ParallaxCameraDelegate onCameraTranslate;

		private static CameraController m_Singleton;

		public static CameraController Singleton
		{
			get
			{
				return m_Singleton;
			}
		}

		[SerializeField]
		private Camera m_Camera;
		[SerializeField]
		private Transform m_Followee;
		[SerializeField]
		private float m_MinY = 0f;
		[SerializeField]
		private float m_MinX = 0f;
		[SerializeField]
		private CameraControl m_ShakeControl;
		[SerializeField]
		private float m_FastMoveSpeed = 10f;
		[SerializeField]
		private float m_Speed = 1f;
		private bool m_FastMove = false;
		private Vector3 m_OldPosition;

		public bool fastMove
		{
			get
			{
				return m_FastMove;
			}
			set
			{
				m_FastMove = value;
			}

		}
		void Awake()
		{
			m_Singleton = this;
			m_ShakeControl = GetComponent<CameraControl>();
		}

		void Start()
		{
			m_OldPosition = transform.position;
		}

		void Update()
		{
			//if (!m_ShakeControl.IsShaking) {
			Follow();
			//}
			if (transform.position != m_OldPosition)
			{
				onCameraTranslateIsNotNull();
				m_OldPosition = transform.position;
			}
		}
		public void onCameraTranslateIsNotNull()
		{
			if (onCameraTranslate != null)
			{
				Vector3 delta = m_OldPosition - transform.position;
				onCameraTranslate(delta);
			}
		}
		public void Follow()
		{
			float speed = m_Speed;
			Ism_FastMoveTrue(speed);
			Vector3 cameraPosition = transform.position;
			Vector3 targetPosition = m_Followee.position;

			CheckPosition(cameraPosition, targetPosition, speed);

		}
		public void Ism_FastMoveTrue(float speed)
		{
			if (m_FastMove)
				speed = m_FastMoveSpeed;
		}
		public void CheckPosition(Vector3 cameraPosition, Vector3 targetPosition, float speed)
		{
			IsTargetPositionX_Morethan_m_MinX(cameraPosition, targetPosition);
			IsTargetPositionY_Morethan_m_MinY(cameraPosition, targetPosition);
			transform.position = Vector3.MoveTowards(transform.position, cameraPosition, speed);
			IsTranformPositionEqualTargetPosition(targetPosition);
		}
		public void IsTargetPositionX_Morethan_m_MinX(Vector3 cameraPosition, Vector3 targetPosition)
		{
			if (targetPosition.x - m_Camera.orthographicSize * m_Camera.aspect > m_MinX)
				cameraPosition.x = targetPosition.x;
			else
				cameraPosition.x = m_MinX + m_Camera.orthographicSize * m_Camera.aspect;
		}
		public void IsTargetPositionY_Morethan_m_MinY(Vector3 cameraPosition, Vector3 targetPosition)
		{
			if (targetPosition.y - m_Camera.orthographicSize > m_MinY)
				cameraPosition.y = targetPosition.y;
			else
				cameraPosition.y = m_MinY + m_Camera.orthographicSize;
		}
		public void IsTranformPositionEqualTargetPosition(Vector3 targetPosition)
		{
			if (transform.position == targetPosition && m_FastMove)
				m_FastMove = false;
		}
	}

	
	
}
