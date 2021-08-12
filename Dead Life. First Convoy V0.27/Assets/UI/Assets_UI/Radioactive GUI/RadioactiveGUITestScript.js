/*
Radioactive GUI Demo Script
Author: Ewa Aguero Padilla, based on Necromancer script by Jason Wentzel
avee@muhagames.com
*/

var doWindow0 = true;
var doWindow1 = true;
var doWindow2 = true;


var mySkin : GUISkin;

private var TestBackground = Rect (0, 0, 1400, 790);
private var windowRect0 = Rect (490, 80, 400, 550);		//Standard Components
private var windowRect1 = Rect (940, 40, 400, 550);		//Sliders and Scrollbars
private var windowRect2 = Rect (40, 40, 400, 550);		//Main Window

private var scrollPosition : Vector2;
private var HorizSliderValue = 0.5;
private var VertSliderValue = 0.5;
private var ToggleBTN = false;
private var selGridInt : int = 0;
private var selStrings : String[] = ["Radio Button 1", "Radio Button 2"];

private var TestText ="Lorem Ipsum is simply dummy text of the \nprinting and typesetting industry. \nLorem Ipsum has been the industry's standard dummy text ever since the 1500s, \nwhen an unknown printer took a galley of type \nand scrambled it to make a type specimen book. \nIt has survived not only five centuries, but also the leap into electronic typesetting, \nremaining essentially unchanged. \nIt was popularised in the 1960s with the release of \nLetraset sheets containing Lorem Ipsum passages, \nand more recently with desktop publishing software like Aldus PageMaker \nincluding versions of Lorem Ipsum.";


function DoMyWindow0 (windowID : int) 
{

		GUILayout.BeginVertical();
		GUILayout.Label("Standard Label");
		GUILayout.Button("Button");
		ToggleBTN = GUILayout.Toggle(ToggleBTN, "This is a Toggle Button");
		GUILayout.TextArea("This is a textarea.\nIt can be expanded\nby using \\n");
		GUILayout.TextField("Score: 1234567890");
		GUI.Label(new Rect(70, 390, 256, 32), "", "ProgressBar");//-------------------------------- custom
		GUI.Label(new Rect(70, 440, 256, 64), "", "Deco2");//-------------------------------- custom
		GUILayout.EndVertical();
		
		// Make the windows be draggable.
		GUI.DragWindow (Rect (0,0,10000,10000));
}


function DoMyWindow1 (windowID : int) 
{

		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);
		GUILayout.Label (TestText, "ScrollText");//-------------------------------- custom
        GUILayout.EndScrollView();
		GUILayout.EndHorizontal();
		GUILayout.Space(25);
		HorizSliderValue = GUILayout.HorizontalSlider(HorizSliderValue, 0.0, 1.1,GUILayout.Width(180));
		GUILayout.BeginHorizontal();
        VertSliderValue = GUILayout.VerticalSlider(VertSliderValue, 0.0, 1.1, GUILayout.Height(80));
        GUILayout.EndHorizontal();
        GUI.Label(new Rect(80, 300, 256, 256), "", "MinimapSample");//-------------------------------- custom
		GUI.Label(new Rect(80, 300, 256, 256), "", "Minimap");//-------------------------------- custom
        GUILayout.Space(120);
		GUILayout.EndVertical();
		GUI.DragWindow (Rect (0,0,10000,10000));
}

//bringing it all together
function DoMyWindow2 (windowID : int) 
{
		
		GUILayout.BeginVertical();
		GUILayout.Label("Radioactive GUI");
		GUILayout.TextArea ("Radioactive is a GUI skin for Unity. Rusty, metallic backgrounds mixed with electronic elements - perfect for any game in a post - apocalyptic setting.");
		doWindow0 = GUILayout.Toggle(doWindow0, "Standard Components");
		doWindow1 = GUILayout.Toggle(doWindow1, "Sliders and Scrollbars");
		GUI.Label(new Rect(70, 420, 256, 64), "", "Deco1");//-------------------------------- custom
        GUILayout.EndVertical();
			
		GUI.DragWindow (Rect (0,0,10000,10000));
}

function OnGUI ()
{
GUI.skin = mySkin;

GUI.Label(TestBackground, "", "Background");//-------------------------------- custom

if (doWindow0)
	windowRect0 = GUI.Window (0, windowRect0, DoMyWindow0, "");
	//now adjust to the group. (0,0) is the topleft corner of the group.
	GUI.BeginGroup (Rect (0,0,100,100));
	// End the group we started above. This is very important to remember!
	GUI.EndGroup ();
	

if (doWindow1)
	windowRect1 = GUI.Window (1, windowRect1, DoMyWindow1, "");
	//now adjust to the group. (0,0) is the topleft corner of the group.
	GUI.BeginGroup (Rect (0,0,100,100));
	// End the group we started above. This is very important to remember!
	GUI.EndGroup ();
	
if (doWindow2)
	windowRect2 = GUI.Window (2, windowRect2, DoMyWindow2, "");
	//now adjust to the group. (0,0) is the topleft corner of the group.
	GUI.BeginGroup (Rect (0,0,100,100));
	// End the group we started above. This is very important to remember!
	GUI.EndGroup ();
}