/**
 * Coding Jar Studios Inc.
 * Author: Jodon Karlik
 * 
 * Copyright 2012 Coding Jar Studios Inc. All rights reserved.  http://www.codingjar.com/
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 *
 * 1.  Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 * 2.  Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
 * 
 * THIS SOFTWARE IS PROVIDED BY CODING JAR STUDIOS INC. ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL CODING JAR STUDIOS INC. OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 * Note: This code does not compile into Unity games (it is an editor-only script).  As such, the above notice is not needed in your games.
 */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#pragma warning disable 612, 618

/**
 * Animation Inspector is used to remap existing animation curves inside an AnimClip.
 * This is useful for when you've animated a scene, but have then re-named scene objects, thus 'losing' the curve and getting
 * the "Cleanup Leftover Curves" message in the Animation window.
 */
public class AnimationInspector : EditorWindow
{
	private AnimationClipCurveData[]	_AnimClipCurveData;
	private AnimationClip				_AnimClip;
	private Vector2						_ScrollPosition;
	private AnimCurvePathNode			_RootPathNode = new AnimCurvePathNode();
	
	/**
	 * Create the animation inspector window
	 */
	[MenuItem("Window/Animation Inspector")]
	static void CreateWindow()
	{
		GetWindow<AnimationInspector>( true, "Coding Jar Studios Animation Inspector" ).Show();
	}
	
	/**
	 * Cache the curve data from the animation clip
	 */
	void PopulateCurves()
	{
		_AnimClipCurveData = AnimationUtility.GetAllCurves( _AnimClip, true );
		PopulatePathNodes();
	}
	
	/**
	 * Store the modifications made back to the animation clip
	 */
	void SaveCurveChanges()
	{
		_AnimClip.ClearCurves();
		
		foreach( AnimationClipCurveData curve in _AnimClipCurveData )
		{
			AnimationUtility.SetEditorCurve( _AnimClip, curve.path, curve.type, curve.propertyName, curve.curve );
		}
		
		PopulateCurves();
	}
	
	/**
	 * The drawing function
	 */
	void OnGUI()
	{
		// Display the field for which animation clip we should inspect
		_AnimClip = EditorGUILayout.ObjectField( _AnimClip, typeof(AnimationClip), false ) as AnimationClip;
		if ( _AnimClip == null )
		{
			EditorGUILayout.HelpBox( "Drag an Animation Clip onto the slot above", MessageType.Info, true );
			return;
		}
		
		// If we've changed animation clips... recache our data
		if ( GUI.changed )
		{
			PopulateCurves();
			GUI.changed = false;
			
			Repaint();
		}
		
		// Draw the path tree for all curves in a scrollable area
		_ScrollPosition = EditorGUILayout.BeginScrollView( _ScrollPosition );
		DrawPathTree( _RootPathNode );
		EditorGUILayout.EndScrollView();
		
		// Save the changes if we're done
		if ( GUILayout.Button("Save Changes") )
		{
			SaveCurveChanges();
			Repaint();
		}
	}
	
	/**
	 * Draw the tree (populated from the animation curves) rooted at the passed-in 'root' variable
	 */
	void DrawPathTree( AnimCurvePathNode root )
	{
		++EditorGUI.indentLevel;
		
		// Do we allow folding out to display the curves?
		GUI.enabled = (root.animClipCurves.Count > 0);
		root.foldOut = EditorGUILayout.Foldout( GUI.enabled && root.foldOut, root.relativePath );
		GUI.enabled = true;
		
		// If we folded out to display all the curves...
		if ( root.foldOut )
		{
			// Display all the curves contained in this node
			foreach( AnimationClipCurveData curve in root.animClipCurves )
			{
				GUIContent pathContent = EditorGUIUtility.ObjectContent( null, curve.type );
				pathContent.tooltip = pathContent.text;
				pathContent.text = "";
				
				// Begin a row
				Rect area = EditorGUILayout.BeginHorizontal( GUILayout.ExpandWidth(true) );
				
				// Stop indenting, since we're drawing horizontally
				int oldIndentLevel = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;
				
				// Draw the little icon infront of the row
				float indentPixels = 10.0f * oldIndentLevel;
				GUILayout.Space( indentPixels + pathContent.image.width );
				GUI.DrawTexture( new Rect(area.x + indentPixels, area.y, pathContent.image.width, pathContent.image.height), pathContent.image );
				
				// Draw path, property name, then the curve preview
				curve.path = EditorGUILayout.TextField( curve.path );
				curve.propertyName = EditorGUILayout.TextField( curve.propertyName, GUILayout.MaxWidth(200.0f) );
				curve.curve = EditorGUILayout.CurveField( curve.curve, Color.red, GetCurveRect(curve.curve), GUILayout.Width(200.0f) );
				
				// Stop drawing horizontally
				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel = oldIndentLevel;
			}
		}
		
		// Now display all of the children of this path node
		foreach( AnimCurvePathNode node in root.children )
		{
			DrawPathTree( node );
		}
		
		--EditorGUI.indentLevel;
	}
	
	#region Path Node Implementation
	class AnimCurvePathNode
	{
		public List<AnimationClipCurveData>	animClipCurves = new List<AnimationClipCurveData>();
		public List<AnimCurvePathNode>		children = new List<AnimCurvePathNode>();
		public string						relativePath;
		public bool							foldOut = false;
	}
	
	AnimCurvePathNode	FindOrCreateChildPath( string relativePath, AnimCurvePathNode parent )
	{
		// First see if the path object already exists
		foreach( AnimCurvePathNode child in parent.children )
		{
			if ( child.relativePath.CompareTo(relativePath) == 0 )
				return child;
		}
		
		// Create the path since it didn't exist
		var newPath = new AnimCurvePathNode();
		newPath.relativePath = relativePath;
		
		parent.children.Add( newPath );
		
		return newPath;
	}
	
	void PopulatePathNodes()
	{
		// Reset the root...
		_RootPathNode.children.Clear();
		_RootPathNode.animClipCurves.Clear();
		
		foreach( var animCurve in _AnimClipCurveData )
		{
			string[] paths = animCurve.path.Split( '/' );
			
			AnimCurvePathNode parentPath = _RootPathNode;
			AnimCurvePathNode curPath = null;
			foreach( string path in paths )
			{
				curPath = FindOrCreateChildPath(path, parentPath);
				parentPath = curPath;
			}
			
			// Add this curve under this path.
			curPath.animClipCurves.Add ( animCurve );
		}
	}
	#endregion	
	
	/**
	 * Returns a suitable rect for displaying an existing curve (just slightly bigger than the curves values)
	 */
	Rect GetCurveRect( AnimationCurve curve )
	{
		float maxTime = 1.0f;
		float maxValue = 1.0f;
		foreach( Keyframe key in curve.keys )
		{
			maxTime = Mathf.Max( key.time, maxTime );
			maxValue = Mathf.Max ( key.value, maxValue );
		}
		
		return new Rect(0.0f, 0.0f, maxTime * 1.1f, maxValue * 1.1f);
	}
}

#pragma warning restore 612, 618