using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;

public abstract class EyesVariables
{
	[SerializeField]
	protected float m_Radius = 1f;
	[SerializeField]
	protected Transform m_Pupil;
	[SerializeField]
	protected Transform m_Eyelid;
	[SerializeField]
	protected float m_MaximumDistance = 5f;
	[SerializeField]
	protected Character m_LatestCharacter;
	[SerializeField]
	protected Vector3 m_InitialPosition;
	[SerializeField]
	protected float m_Speed = 0.01f;
	[SerializeField]
	protected float m_DeadSpeed = 0.005f;
	[SerializeField]
	protected Vector3 m_DeadPosition;
	protected Vector3 m_PupilDestination;
}
