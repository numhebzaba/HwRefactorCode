using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedRunner.UI
{

	public class UIShareButtons : MonoBehaviour
	{

		[SerializeField]
		protected Animator m_ShareBackground;
		[SerializeField]
		protected Animator[] m_ShareButtons;
		protected bool Ism_IsOpen = false;

		void Start ()
		{
			
		}

		public void Toggle ()
		{
			if ( Ism_IsOpen)
            {
				Setm_IsOpenBooltoFalse();
				return;
			}
			Setm_IsOpenBooltoTrue();
		}
		public void Setm_IsOpenBooltoFalse()
        {
			Ism_IsOpen = false;
			SetTrigger("Close");
		}
		public void Setm_IsOpenBooltoTrue()
        {
			Ism_IsOpen = true;
			SetTrigger("Open");
		}

		public void SetTrigger ( string trigger )
		{
			m_ShareBackground.SetTrigger ( trigger );
			for ( int index = 0; index < m_ShareButtons.Length; index++ )
			{
				m_ShareButtons [index].SetTrigger ( trigger );
			}
		}

	}

}