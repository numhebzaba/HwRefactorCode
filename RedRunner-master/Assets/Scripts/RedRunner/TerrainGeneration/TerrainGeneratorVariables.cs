using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;

namespace RedRunner.TerrainGenerationVariables
{
    public abstract class TerrainGeneratorVariables : MonoBehaviour
    {
		protected Dictionary<Vector3, Block> m_Blocks;
		protected Dictionary<Vector3, BackgroundBlock> m_BackgroundBlocks;
		protected BackgroundLayer[] m_BackgroundLayers;
		protected float m_PreviousX;
		protected float m_CurrentX;
		protected float m_FathestBackgroundX;
		[SerializeField]
		protected TerrainGenerationSettings m_Settings;
		protected int m_GeneratedStartBlocksCount;
		protected int m_GeneratedMiddleBlocksCount;
		protected int m_GeneratedEndBlocksCount;
		[SerializeField]
		protected float m_DestroyRange = 100f;
		[SerializeField]
		protected float m_GenerateRange = 100f;
		[SerializeField]
		protected float m_BackgroundGenerateRange = 200f;
		[SerializeField]
		protected Character m_Character;
		protected Block m_LastBlock;
		protected BackgroundBlock m_LastBackgroundBlock;
		protected float m_RemoveTime = 0f;
		protected bool m_Reset = false;
	}
}

