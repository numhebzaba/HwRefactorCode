using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RedRunner.UI
{

	[RequireComponent ( typeof ( Animator ) ), RequireComponent ( typeof ( CanvasGroup ) )]
	public class UIWindow : MonoBehaviour
	{

		public delegate void OpenHandler ();

		public delegate void CloseHandler ();

		public event OpenHandler OnOpened;
		public event CloseHandler OnClosed;

		[SerializeField]
		protected Animator m_Animator;
		[SerializeField]
		protected CanvasGroup m_CanvasGroup;
		[SerializeField]
		protected OpenedEvent m_OnOpened;
		[SerializeField]
		protected ClosedEvent m_OnClosed;
		[SerializeField]
		protected OpenEvent m_OnOpen;
		[SerializeField]
		protected CloseEvent m_OnClose;
		[SerializeField]
		protected bool m_Open = false;

		public void Open ()
		{
			m_OnOpen.Invoke ( this );
			Setm_AnimatorBool( "Open", true );
		}

		public void Close ()
		{
			m_OnClose.Invoke ( this );
			Setm_AnimatorBool( "Open", false );
		}
		
		public void Setm_AnimatorBool(string parameterName , bool value)
        {
			m_Animator.SetBool(parameterName, value);
		}

		public void Opened ()
		{
			if ( OnOpened != null )
				OnOpened ();
			OnOpened = null;
			m_OnOpened.Invoke ( this );
			SetOpenChangedBool( true );
		}

		public void Closed ()
		{
			if ( OnClosed != null )
				OnClosed ();
			OnClosed = null;
			m_OnClosed.Invoke ( this );
			SetOpenChangedBool( false );
		}

		public void SetOpenChangedBool(bool value)
        {
			OpenChanged(value);
		}

		public void OpenChanged ( bool isOpen )
		{
			m_Open = isOpen;
			m_CanvasGroup.interactable = m_Open;
			m_CanvasGroup.blocksRaycasts = m_Open;
		}

		[System.Serializable]
		public class OpenedEvent : UnityEvent<UIWindow>
		{

		}

		[System.Serializable]
		public class ClosedEvent : UnityEvent<UIWindow>
		{

		}

		[System.Serializable]
		public class OpenEvent : UnityEvent<UIWindow>
		{

		}

		[System.Serializable]
		public class CloseEvent : UnityEvent<UIWindow>
		{
			
		}

	}

}