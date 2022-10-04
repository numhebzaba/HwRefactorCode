using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Characters;

namespace RedRunner.TerrainGeneration
{

	public class TerrainGenerator : TerrainGenerationVariables
	{

		private static TerrainGenerator m_Singleton;

		public static TerrainGenerator Singleton
		{
			get
			{
				return m_Singleton;
			}
		}

		public float PreviousX
		{
			get
			{
				return m_PreviousX;
			}
		}

		public float CurrentX
		{
			get
			{
				return m_CurrentX;
			}
		}

		public TerrainGenerationSettings Settings
		{
			get
			{
				return m_Settings;
			}
		}

		protected virtual void Awake ()
		{
			if ( m_Singleton != null )
			{
				Destroy ( gameObject );
				return;
			}
			m_Singleton = this;
			m_Blocks = new Dictionary<Vector3, Block> ();
			m_BackgroundBlocks = new Dictionary<Vector3, BackgroundBlock> ();
			m_BackgroundLayers = new BackgroundLayer[m_Settings.BackgroundLayers.Length];
			
			SetBG();

			GameManager.OnReset += Reset;
		}

		void SetBG()
        {
			for (int i = 0; i < m_Settings.BackgroundLayers.Length; i++)
			{
				m_BackgroundLayers[i] = m_Settings.BackgroundLayers[i];
			}
		}

		protected virtual void OnDestroy ()
		{
			m_Singleton = null;
		}

		protected virtual void Update ()
		{
			if ( m_Reset )
			{
				return;
			}
			
			if ( m_RemoveTime < Time.time )
			{
				m_RemoveTime = Time.time + 5f;
				Remove ();
			}
			Generate ();
		}

		public virtual void Generate ()
		{
			if ( m_CurrentX < m_Settings.LevelLength || m_Settings.LevelLength <= 0 )
			{
				bool isEnd = false, isStart = false, isMiddle = false;
				Block block = null;
				Vector3 current = new Vector3 ( m_CurrentX, 0f, 0f );
				float newX = 0f;
				GenerateBlockFromStart();

				CheckLastBlock();

				if ( block != null && ( m_LastBlock == null || newX < m_Character.transform.position.x + m_GenerateRange ) )
				{
					CheckWhereToGenerate();

					CreateBlock ( block, current );
				}
			}

			GenerateBackground();
		}

		public void GenerateBackground()
        {
			for (int i = 0; i < m_BackgroundLayers.Length; i++)
			{
				int random = Random.Range(0, 2);
				bool generate = random == 1 ? true : false;
				if (!generate)
				{
					continue;
				}
				Vector3 current = new Vector3(m_BackgroundLayers[i].CurrentX, 0f, 0f);
				BackgroundBlock block = (BackgroundBlock)ChooseFrom(m_BackgroundLayers[i].Blocks);
				float newX = 0f;

				CheckBackgroundLastBlock();
			}
		}

		public void CheckBackgroundLastBlock()
        {
			if (m_BackgroundLayers[i].LastBlock != null)
			{
				newX = m_BackgroundLayers[i].CurrentX + m_BackgroundLayers[i].LastBlock.Width;
			}
			BackgroundLastBlockIsNull();

			if (block != null && (m_BackgroundLayers[i].LastBlock == null || newX < m_Character.transform.position.x + m_BackgroundGenerateRange))
			{
				CreateBackgroundBlock(block, current, m_BackgroundLayers[i], i);
			}
		}

		public void BackgroundLastBlockIsNull()
        {
			if (m_BackgroundLayers[i].LastBlock == null)
            {
				newX = 0f;
			}

		}

		public void GenerateBlockFromStart()
        {
			if (m_GeneratedStartBlocksCount < m_Settings.StartBlocksCount || m_Settings.StartBlocksCount <= 0)
			{
				isStart = true;
				block = ChooseFrom(m_Settings.StartBlocks);
			}
			GenerateMiddleBlockFromStart();
			GenerateEndBlockFromStart();
		}
		
		public void GenerateMiddleBlockFromStart()
        {
			if (m_GeneratedMiddleBlocksCount < m_Settings.MiddleBlocksCount || m_Settings.MiddleBlocksCount <= 0)
			{
				isMiddle = true;
				block = ChooseFrom(m_Settings.MiddleBlocks);
			}
		}

		public void GenerateEndBlockFromStart()
        {
			if (m_GeneratedEndBlocksCount < m_Settings.EndBlocksCount || m_Settings.EndBlocksCount <= 0)
			{
				isEnd = true;
				block = ChooseFrom(m_Settings.EndBlocks);
			}
		}

		public void CheckLastBlock()
        {
			if (m_LastBlock != null)
			{
				newX = m_CurrentX + m_LastBlock.Width;
			}
			else
			{
				newX = 0f;
			}
		}

		public void CheckWhereToGenerate()
        {
			if (isStart)
			{
				if (m_Settings.StartBlocksCount > 0)
				{
					m_GeneratedStartBlocksCount++;
				}
			}
			else if (isMiddle)
			{
				if (m_Settings.MiddleBlocksCount > 0)
				{
					m_GeneratedMiddleBlocksCount++;
				}
			}
			else if (isEnd)
			{
				if (m_Settings.EndBlocksCount > 0)
				{
					m_GeneratedEndBlocksCount++;
				}
			}
		}

		public virtual bool CreateBlock ( Block blockPrefab, Vector3 position )
		{
			if ( blockPrefab == null )
			{
				return false;
			}
			SetBlock();
			return true;
		}

		public void SetBlock()
        {
			blockPrefab.PreGenerate(this);
			Block block = Instantiate<Block>(blockPrefab, position, Quaternion.identity);
			m_PreviousX = m_CurrentX;
			m_CurrentX += block.Width;
			m_Blocks.Add(position, block);
			blockPrefab.PostGenerate(this);
			m_LastBlock = block;
		}

		public virtual bool CreateBackgroundBlock ( BackgroundBlock blockPrefab, Vector3 position, BackgroundLayer layer, int layerIndex )
		{
			if ( blockPrefab == null )
			{
				return false;
			}
			SetBackgroundBlock();
			if ( m_BackgroundLayers [ layerIndex ].CurrentX > m_FathestBackgroundX )
			{
				m_FathestBackgroundX = m_BackgroundLayers [ layerIndex ].CurrentX;
			}
			return true;
		}

		public void SetBackgroundBlock()
        {
			blockPrefab.PreGenerate(this);
			position.z = blockPrefab.transform.position.z;
			position.y = blockPrefab.transform.position.y;
			BackgroundBlock block = Instantiate<BackgroundBlock>(blockPrefab, position, Quaternion.identity);
			float width = Random.Range(block.MinWidth, block.MaxWidth);
			m_BackgroundLayers[layerIndex].PreviousX = m_BackgroundLayers[layerIndex].CurrentX;
			m_BackgroundLayers[layerIndex].CurrentX += width;
			block.Width = width;
			m_BackgroundLayers[layerIndex].LastBlock = block;
			m_BackgroundBlocks.Add(position, block);
			blockPrefab.PostGenerate(this);
		}

		public Block GetCharacterBlock ()
		{
			Block characterBlock = null;
			foreach ( KeyValuePair<Vector3, Block> block in m_Blocks )
			{
				if ( block.Key.x <= m_Character.transform.position.x && block.Key.x + block.Value.Width > m_Character.transform.position.x )
				{
					characterBlock = block.Value;
					break;
				}
			}
			return characterBlock;
		}

		public static Block ChooseFrom ( Block[] blocks )
		{
			if ( blocks.Length <= 0 )
			{
				return null;
			}
			float total = 0;
			for ( int i = 0; i < blocks.Length; i++ )
			{
				total += blocks [ i ].Probability;
			}
			float randomPoint = Random.value * total;
			for ( int i = 0; i < blocks.Length; i++ )
			{
				if ( randomPoint < blocks [ i ].Probability )
				{
					return blocks [ i ];
				}
				else
				{
					randomPoint -= blocks [ i ].Probability;
				}
			}
			return blocks [ blocks.Length - 1 ];
		}

	}

}