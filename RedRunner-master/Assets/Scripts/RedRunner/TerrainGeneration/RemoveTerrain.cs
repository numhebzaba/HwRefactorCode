using RedRunner.TerrainGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTerrain : TerrainGenerator
{
	//public virtual void Remove()
	//{
	//	List<Block> blocksToRemove = new List<Block>();
	//	foreach (KeyValuePair<Vector3, Block> block in m_Blocks)
	//	{
	//		if (block.Value.transform.position.x - m_CurrentX > m_DestroyRange)
	//		{
	//			blocksToRemove.Add(block.Value);
	//		}
	//	}
	//	List<BackgroundBlock> backgroundBlocksToRemove = new List<BackgroundBlock>();
	//	foreach (KeyValuePair<Vector3, BackgroundBlock> block in m_BackgroundBlocks)
	//	{
	//		if (block.Value.transform.position.x - m_FathestBackgroundX > m_DestroyRange)
	//		{
	//			backgroundBlocksToRemove.Add(block.Value);
	//		}
	//	}
	//	for (int i = 0; i < blocksToRemove.Count; i++)
	//	{
	//		RemoveBlock(blocksToRemove[i]);
	//	}
	//	for (int i = 0; i < backgroundBlocksToRemove.Count; i++)
	//	{
	//		RemoveBackgroundBlock(backgroundBlocksToRemove[i]);
	//	}
	//}

	//public virtual void RemoveAll()
	//{
	//	List<Block> blocksToRemove = new List<Block>();
	//	foreach (KeyValuePair<Vector3, Block> block in m_Blocks)
	//	{
	//		blocksToRemove.Add(block.Value);
	//	}
	//	List<BackgroundBlock> backgroundBlocksToRemove = new List<BackgroundBlock>();
	//	foreach (KeyValuePair<Vector3, BackgroundBlock> block in m_BackgroundBlocks)
	//	{
	//		backgroundBlocksToRemove.Add(block.Value);
	//	}
	//	for (int i = 0; i < blocksToRemove.Count; i++)
	//	{
	//		RemoveBlock(blocksToRemove[i]);
	//	}
	//	for (int i = 0; i < backgroundBlocksToRemove.Count; i++)
	//	{
	//		RemoveBackgroundBlock(backgroundBlocksToRemove[i]);
	//	}
	//}

	//public virtual void RemoveBlockAt(Vector3 position)
	//{
	//	RemoveBlock(m_Blocks[position]);
	//}

	//public virtual void RemoveBlock(Block block)
	//{
	//	block.OnRemove(this);
	//	Destroy(m_Blocks[block.transform.position].gameObject);
	//	m_Blocks.Remove(block.transform.position);
	//}

	//public virtual void RemoveBackgroundBlock(BackgroundBlock block)
	//{
	//	block.OnRemove(this);
	//	Destroy(m_BackgroundBlocks[block.transform.position].gameObject);
	//	m_BackgroundBlocks.Remove(block.transform.position);
	//}
}
