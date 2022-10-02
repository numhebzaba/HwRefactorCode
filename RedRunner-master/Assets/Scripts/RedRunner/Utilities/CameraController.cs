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
		private bool Ism_FastMove = false;
		private Vector3 m_OldPosition;

		public bool fastMove
		{
			get
			{
				return Ism_FastMove;
			}
			set
			{
				Ism_FastMove = value;
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
			if (transform.position == m_OldPosition)
				return;
			TryonCameraTranslateIsNotNull();
			m_OldPosition = transform.position;
		}
		public void TryonCameraTranslateIsNotNull()
		{
			if (onCameraTranslate == null)
				return;
			Vector3 delta = m_OldPosition - transform.position;
			onCameraTranslate(delta);
		}
		public void Follow()
		{
			float speed = m_Speed;
			Ism_FastMoveTrue(speed);
			Vector3 cameraPosition = transform.position, targetPosition = m_Followee.position;
			var CameraControllData = new CameraControllData(cameraPosition, targetPosition, speed);
			CheckPosition(CameraControllData);
		}
		public void Ism_FastMoveTrue(float speed)
		{
			if (Ism_FastMove)
				speed = m_FastMoveSpeed;
		}
		public void CheckPosition(CameraControllData CameraControllData)
		{
			IsTargetPositionX_Morethan_m_MinX(CameraControllData);
			IsTargetPositionY_Morethan_m_MinY(CameraControllData);
			transform.position = Vector3.MoveTowards(transform.position, CameraControllData.cameraPosition, CameraControllData.speed);
			IsTranformPositionEqualTargetPosition(CameraControllData.targetPosition);
		}
		public void IsTargetPositionX_Morethan_m_MinX(CameraControllData CameraControllData)
		{
			if (CameraControllData.targetPosition.x - m_Camera.orthographicSize * m_Camera.aspect > m_MinX)
				CameraControllData.cameraPosition.x = CameraControllData.targetPosition.x;
			else
				CameraControllData.cameraPosition.x = m_MinX + m_Camera.orthographicSize * m_Camera.aspect;
		}
		public void IsTargetPositionY_Morethan_m_MinY(CameraControllData CameraControllData)
		{
			if (CameraControllData.targetPosition.y - m_Camera.orthographicSize > m_MinY)
				CameraControllData.cameraPosition.y = CameraControllData.targetPosition.y;
			else
				CameraControllData.cameraPosition.y = m_MinY + m_Camera.orthographicSize;
		}
		public void IsTranformPositionEqualTargetPosition(Vector3 targetPosition)
		{
			if (transform.position == targetPosition && Ism_FastMove)
				Ism_FastMove = false;
		}
	}

	
	
}
