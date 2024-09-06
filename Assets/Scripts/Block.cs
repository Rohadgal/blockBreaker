using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Configuration parameter
    [SerializeField] AudioClip explosionSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //Cached Reference
    Level level;

    //state variables
    [SerializeField] int timesHit; // TODO only serialize for bugging purposes

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
        
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( tag == "Breakable")
        {
            HandleHit();
        }
        
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlocks();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlocks()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        TriggerSparkleVFX();
        level.BlockDestroyed();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
    }

    private void TriggerSparkleVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles , 1f);
    }

}
