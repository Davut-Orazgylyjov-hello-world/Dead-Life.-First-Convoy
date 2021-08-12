using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *Class for simple animation control on custom style background of normal state.
 * 
*/

public class RadioactiveAnimatedTexture : MonoBehaviour 
{
	public GUISkin m_skin;
	public string m_styleName = "ProgressBar";	
	public List<Texture2D> m_frames;
	public float m_fps = 10.0f;
	public bool m_autoAnimate = true;
	
	
	
	private int m_currentFrame=0;
	private float m_delayInSeconds = 100.0f;
	private float m_currentDelay = 0.0f;
	private GUIStyleState m_textureParent;
	private bool m_dirty = true;
	
	void Awake()
	{
		//find how often we will switch texture for automated process. 
		//NOTE! We do this only once!
		if (m_fps > 0)
		{
			m_delayInSeconds = 1.0f/m_fps	;
		}		
	}
	
	
	void OnGUI () 
	{
		//ensure we use proper skin for our animation
		GUI.skin = m_skin;
		
		//find texture parent. Later we will reuse it for direct access
		if (m_textureParent == null)
		{					
			for (int i =0; i< GUI.skin.customStyles.Length; i++)
			{				
				if (GUI.skin.customStyles[i].name ==m_styleName)
				{
					m_textureParent = 	GUI.skin.customStyles[i].normal;
					break;
				}
				
			}		
		}
		
		//we have texture so check if we should do automated animation progress
		else
		{		
			if (m_autoAnimate)
			{
				m_currentDelay += Time.deltaTime;
				if (m_delayInSeconds < m_currentDelay)
				{
					m_currentDelay -= m_delayInSeconds;
					m_currentFrame = ++m_currentFrame % m_frames.Count;
					
					m_dirty = true;
			 		
				}
			}
		}
		
		//update texture if it is required
		if (m_dirty)
		{
			m_dirty = false;
			m_currentFrame = m_currentFrame % m_frames.Count;
			m_textureParent.background = m_frames[m_currentFrame];
		}
	}
	
	//function for remote animation
	public void SetFrame(int frame)
	{
		m_dirty = true;
		m_currentFrame = frame;
	}
}
