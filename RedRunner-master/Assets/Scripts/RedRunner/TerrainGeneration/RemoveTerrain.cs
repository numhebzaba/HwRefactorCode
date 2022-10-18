using System;
using UnityEngine;
using RedRunner.Characters;
using RedRunner.TerrainGenerationVariables;
using RedRunner.TerrainGeneration;

public class RemoveTerrain : TerrainGeneratorVariables
{
	protected virtual void Reset()
	{
		m_Reset = true;
		RemoveAll();
		m_CurrentX = 0f;
		m_LastBlock = null;
		m_LastBackgroundBlock = null;

		for (int i = 0; i < m_BackgroundLayers.Length; i++)
		{
			m_BackgroundLayers[i].Reset();
		}

		SetBlockToZero();
	}

	protected void SetBlockToZero()
	{
		m_FathestBackgroundX = 0f;
		m_Blocks.Clear();
		m_BackgroundBlocks.Clear();
		m_GeneratedStartBlocksCount = 0;
		m_GeneratedMiddleBlocksCount = 0;
		m_GeneratedEndBlocksCount = 0;
		m_Reset = false;
	}

	public virtual void RemoveAll()
	{
		List<Block> blocksToRemove = new List<Block>();
		foreach (KeyValuePair<Vector3, Block> block in m_Blocks)
		{
			blocksToRemove.Add(block.Value);
		}
		List<BackgroundBlock> backgroundBlocksToRemove = new List<BackgroundBlock>();
		foreach (KeyValuePair<Vector3, BackgroundBlock> block in m_BackgroundBlocks)
		{
			backgroundBlocksToRemove.Add(block.Value);
		}
		for (int i = 0; i < blocksToRemove.Count; i++)
		{
			RemoveBlock(blocksToRemove[i]);
		}
		for (int i = 0; i < backgroundBlocksToRemove.Count; i++)
		{
			RemoveBackgroundBlock(backgroundBlocksToRemove[i]);
		}
	}

	public virtual void RemoveBlockAt(Vector3 position)
	{
		RemoveBlock(m_Blocks[position]);
	}

	public virtual void RemoveBlock(Block block)
	{
		block.OnRemove(this);
		Destroy(m_Blocks[block.transform.position].gameObject);
		m_Blocks.Remove(block.transform.position);
	}

	public virtual void RemoveBackgroundBlock(BackgroundBlock block)
	{
		block.OnRemove(this);
		Destroy(m_BackgroundBlocks[block.transform.position].gameObject);
		m_BackgroundBlocks.Remove(block.transform.position);
	}
}
