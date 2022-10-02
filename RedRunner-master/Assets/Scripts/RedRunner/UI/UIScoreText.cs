using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RedRunner.Utilities;

namespace RedRunner.UI
{

	public class UIScoreText : Text
	{

		protected bool Ism_Collected = false;

		protected override void Awake ()
		{
			GameManager.OnScoreChanged += GameManager_OnScoreChanged;
			GameManager.OnReset += GameManager_OnReset;
			base.Awake ();
		}

		void GameManager_OnReset ()
		{
			Ism_Collected = false;
		}

		void GameManager_OnScoreChanged ( ScoreData scoreData )
		{
			text = scoreData.newScore.ToLength ();
			if (IsNewScoreMorethanHighScoreAndm_CollectedIsFalse(scoreData))
				Setm_CollectedBoolAndSetTriggerValue();
		}
		public bool IsNewScoreMorethanHighScoreAndm_CollectedIsFalse(ScoreData scoreData)
		{
			return (scoreData.newScore > scoreData.highScore && !Ism_Collected);
		}
		public void Setm_CollectedBoolAndSetTriggerValue()
        {
			Ism_Collected = true;
			GetComponent<Animator>().SetTrigger("Collect");
		}

	}

}