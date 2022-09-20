using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedRunner.UI
{

	public class UIImageRandom : MonoBehaviour
	{

		//[System.Serializable]
		//public struct RandomImageItem
		//{
		//	public Color color;
		//	public Sprite sprite;
		//}

		[SerializeField]
		protected RandomImageItemClass[] m_RandomItems;
		[SerializeField]
		protected Image m_ColorImage;
		[SerializeField]
		protected Image m_PatternImage;

		protected virtual void Start ()
		{
			if ( m_RandomItems.Length > 0 )
			{
				int index = Random.Range ( 0, m_RandomItems.Length );
				m_PatternImage.sprite = m_RandomItems [ index ].sprite;
				SetnewColorValue(index);
			}
		}
		public void SetnewColorValue(int index)
        {
			Color newColor = m_RandomItems[index].color;
			newcolorSetting(newColor);
			newColor.a = m_RandomItems[index].color.a;
			Setm_PatternImageColor(newColor);
		}
		public void newcolorSetting(Color newColor)
        {
			newColor.a = Color.white.a;
			m_ColorImage.color = newColor;
			newColor = Color.white;
		}
		public void Setm_PatternImageColor(Color newColor)
        {
			m_PatternImage.color = newColor;
		}


	}

}