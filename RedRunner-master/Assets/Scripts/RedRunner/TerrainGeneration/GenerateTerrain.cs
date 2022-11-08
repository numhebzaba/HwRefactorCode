using RedRunner.TerrainGeneration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : TerrainGenerator
{
    //public virtual void Generate()
    //{
    //    if (m_CurrentX < m_Settings.LevelLength || m_Settings.LevelLength <= 0)
    //    {
    //        bool isEnd = false, isStart = false, isMiddle = false;
    //        Block block = null;
    //        Vector3 current = new Vector3(m_CurrentX, 0f, 0f);
    //        float newX = 0f;
    //        if (m_GeneratedStartBlocksCount < m_Settings.StartBlocksCount || m_Settings.StartBlocksCount <= 0)
    //        {
    //            isStart = true;
    //            block = ChooseFrom(m_Settings.StartBlocks);
    //        }
    //        else if (m_GeneratedMiddleBlocksCount < m_Settings.MiddleBlocksCount || m_Settings.MiddleBlocksCount <= 0)
    //        {
    //            isMiddle = true;
    //            block = ChooseFrom(m_Settings.MiddleBlocks);
    //        }
    //        else if (m_GeneratedEndBlocksCount < m_Settings.EndBlocksCount || m_Settings.EndBlocksCount <= 0)
    //        {
    //            isEnd = true;
    //            block = ChooseFrom(m_Settings.EndBlocks);
    //        }
    //        if (m_LastBlock != null)
    //        {
    //            newX = m_CurrentX + m_LastBlock.Width;
    //        }
    //        else
    //        {
    //            newX = 0f;
    //        }
    //        if (block != null && (m_LastBlock == null || newX < m_Character.transform.position.x + m_GenerateRange))
    //        {
    //            if (isStart)
    //            {
    //                if (m_Settings.StartBlocksCount > 0)
    //                {
    //                    m_GeneratedStartBlocksCount++;
    //                }
    //            }
    //            else if (isMiddle)
    //            {
    //                if (m_Settings.MiddleBlocksCount > 0)
    //                {
    //                    m_GeneratedMiddleBlocksCount++;
    //                }
    //            }
    //            else if (isEnd)
    //            {
    //                if (m_Settings.EndBlocksCount > 0)
    //                {
    //                    m_GeneratedEndBlocksCount++;
    //                }
    //            }
    //            CreateBlock(block, current);
    //        }
    //    }
    //    GenerateBGWhilePlaying();
    //}

    //void GenerateBGWhilePlaying()
    //{
    //    for (int i = 0; i < m_BackgroundLayers.Length; i++)
    //    {
    //        int random = Random.Range(0, 2);
    //        bool generate = random == 1 ? true : false;
    //        if (!generate)
    //        {
    //            continue;
    //        }
    //        Vector3 current = new Vector3(m_BackgroundLayers[i].CurrentX, 0f, 0f);
    //        BackgroundBlock block = (BackgroundBlock)ChooseFrom(m_BackgroundLayers[i].Blocks);
    //        float newX = 0f;
    //        if (m_BackgroundLayers[i].LastBlock != null)
    //        {
    //            newX = m_BackgroundLayers[i].CurrentX + m_BackgroundLayers[i].LastBlock.Width;
    //        }
    //        else
    //        {
    //            newX = 0f;
    //        }
    //        if (block != null && (m_BackgroundLayers[i].LastBlock == null || newX < m_Character.transform.position.x + m_BackgroundGenerateRange))
    //        {
    //            CreateBackgroundBlock(block, current, m_BackgroundLayers[i], i);
    //        }
    //    }
    //}

    //public static Block ChooseFrom(Block[] blocks)
    //{
    //    if (blocks.Length <= 0)
    //    {
    //        return null;
    //    }
    //    float total = 0;
    //    for (int i = 0; i < blocks.Length; i++)
    //    {
    //        total += blocks[i].Probability;
    //    }
    //    float randomPoint = Random.value * total;
    //    for (int i = 0; i < blocks.Length; i++)
    //    {
    //        if (randomPoint < blocks[i].Probability)
    //        {
    //            return blocks[i];
    //        }
    //        else
    //        {
    //            randomPoint -= blocks[i].Probability;
    //        }
    //    }
    //    return blocks[blocks.Length - 1];
    //}
}
