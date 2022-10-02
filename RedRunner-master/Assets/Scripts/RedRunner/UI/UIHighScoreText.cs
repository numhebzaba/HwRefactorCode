using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RedRunner.Utilities;

namespace RedRunner.UI
{

	public class UIHighScoreText : Text
	{

		protected override void Awake ()
		{
			GameManager.OnScoreChanged += GameManager_OnScoreChanged;
			base.Awake ();
		}

		void GameManager_OnScoreChanged ( ScoreData scoreData )
		{
			text = scoreData.highScore.ToLength ();
		}

	}

}