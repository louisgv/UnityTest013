import System.Collections.Generic;
import System.IO;

class UPaint extends EditorWindow
{
	static var window : EditorWindow;
	static var image : Texture2D;
	static var images : List.<Texture2D> = new List.<Texture2D>();
	static var palettes : List.<Color> = new List.<Color>();
	static var scrollPosition : Vector2 = Vector2.zero;
	static var editScrollPosition : Vector2 = Vector2.zero;
	static var selectedImage : int;
	static var currentColor : Color;
	static var resolution : int;
	static var splits : int;
	static var tool : String;
	static var zoom : float;
	
	@MenuItem("Window/UPaint")
	static function Init()
	{
		ShowWindow();
	}
	
	static function ShowWindow()
	{
		image = null;
		images = new List.<Texture2D>();
		if(palettes.Count == 0)
		{
			palettes = new List.<Color>();
			for(var i = 0; i < 13; i++)palettes.Add(Color(Random.value, Random.value, Random.value, 1.0));
		}
		currentColor = Color.red;
		currentColor.a = 255;
		selectedImage = 0;
		tool = "Pencil";
		window = GetWindow(UPaint);
		window.position = Rect(Screen.width/2, Screen.height/2, 0, 0);
		window.maxSize = Vector2(610, 454);
		window.minSize = window.maxSize;
		window.Show();
	}
	
	function OnGUI()
	{
		GUI.color = Color.black;
		GUI.Box(Rect(0, 0, 610, 454), "");
		
		GUI.color = Color.white;
		GUILayout.BeginArea(Rect(2, 2, 402, 24), "", "Box");
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("New Tileset"))
		{
			var newTilesetWindow = EditorWindow.GetWindow(ImageSettings);
			newTilesetWindow.state = "NewTileset";
			newTilesetWindow.ShowWindow();
		}
		if(GUILayout.Button("Import Tileset"))
		{
			var importTilesetWindow = EditorWindow.GetWindow(ImageSettings);
			importTilesetWindow.state = "ImportTileset";
			importTilesetWindow.ShowWindow();
		}
		if(GUILayout.Button("Export Tileset") && images.Count > 0)
		{
			SaveSheet();
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		
		GUILayout.BeginArea(Rect(406, 2, 202, 24), "", "Box");
		GUILayout.BeginHorizontal();
		if(images.Count > 0)
		{
			if(GUILayout.Button("Import Tile"))
			{
				var importTileWindow = EditorWindow.GetWindow(ImageSettings);
				importTileWindow.state = "ImportTile";
				importTileWindow.pix = resolution/splits;
				importTileWindow.ShowWindow();
			}
			if(GUILayout.Button("Export Tile"))
			{
				SaveTile();
			}	
		}
		if(images.Count == 0)GUILayout.Label(":No tile in preview:");
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		
		GUILayout.BeginArea(Rect(2, 28, 96, 290), "", "Box");
		if(images.Count > 0)
		{
			DrawButtons();
		}
		GUILayout.EndArea();
		
		GUILayout.BeginArea(Rect(502, 28, 106, 290), "", "Box");
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Custom Palettes", EditorStyles.miniBoldLabel);
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		DrawPalettes();
		GUILayout.EndArea();
		
		GUILayout.BeginArea(Rect(502, 320, 106, 132), "", "Box");
		GUILayout.Label("Current Color");
		currentColor = EditorGUILayout.ColorField(currentColor);
		if(GUILayout.Button("Color Picker"))
		{
			tool = "Color Picker";
			Repaint();
		}
		if(GUILayout.Button("Color Swap"))
		{
			tool = "Color Swap";
			Repaint();
		}
		GUILayout.EndArea();
		
		GUILayout.BeginArea(Rect(2, 320, 96, 132), "", "Box");
		if(GUILayout.Button("Pencil"))
		{
			tool = "Pencil";
			Repaint();
		}
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label("ZOOM");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("+") && image)
		{
			if(zoom < 50)
			{
				zoom += 100/(parseFloat(resolution)/parseFloat(splits));
			}
		}
		if(GUILayout.Button("-") && image)
		{
			if(zoom > 0)
			{
				zoom -= 100/(parseFloat(resolution)/parseFloat(splits));
			}
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		
		GUI.BeginGroup(Rect(100, 28, 400, 400), "", "Box");
		if(image && images.Count > 0)
		{
			DrawTexture();
		}
		GUI.EndGroup();
		
		GUI.color = Color.gray;
		GUILayout.BeginArea(Rect(100, 430, 400, 22), "", "Box");
		GUILayout.BeginHorizontal();
		if(image)
		{
			GUILayout.Label("Current Tool: " + tool);
			GUILayout.FlexibleSpace();
			GUILayout.Label("Preview: Image" + selectedImage.ToString());
		}
		if(!image)GUILayout.Label(":No Image Selected:");
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
	
	function CreateNewSheet(r : int, s : int)
	{
		images.Clear();
		resolution = r;
		splits = s;
		image = new Texture2D(resolution/splits, resolution/splits);
		images.Add(image);
		var size = (splits * splits) - 1;
		for(var a = 0; a < size; a++)
		{
			var i = new Texture2D(resolution/splits, resolution/splits);
			images.Add(i);
		}
	}
	
	function ImportSheet(r : int, s : int, t : Texture2D)
	{
		images.Clear();
		resolution = r;
		splits = s;
		var path = AssetDatabase.GetAssetPath(t);
		var ti : TextureImporter = AssetImporter.GetAtPath(path);
		var p : Color[];
		var i : Texture2D;
		var a : int;
		var b : int;
		if(!ti.isReadable)
		{
			if(EditorUtility.DisplayDialog("Texture Not Readable", "The selected texture is not set to read/write. Would you like it to be set for you?", "Set", "Cancel"))
			{
				for(a = 0; a < splits; a++)
				{
					for(b = 0; b < splits; b++)
					{
						ti.isReadable = true;
						AssetDatabase.ImportAsset(path);
						
						p = t.GetPixels(a * (resolution/splits), b * (resolution/splits), resolution/splits, resolution/splits);
						i = new Texture2D(resolution/splits, resolution/splits);
						i.SetPixels(0, 0, resolution/splits, resolution/splits, p);
						i.Apply();
						if(a == 0 && b == 0)image = i;
						images.Add(i);
					}
				}
			}
		}
		else
		{
			for(a = 0; a < splits; a++)
			{
				for(b = 0; b < splits; b++)
				{
					ti.isReadable = true;
					AssetDatabase.ImportAsset(path);
					
					p = t.GetPixels(a * (resolution/splits), b * (resolution/splits), resolution/splits, resolution/splits);
					i = new Texture2D(resolution/splits, resolution/splits);
					i.SetPixels(0, 0, resolution/splits, resolution/splits, p);
					i.Apply();
					if(a == 0 && b == 0)image = i;
					images.Add(i);
				}
			} 
		}
	}
	
	function ImportTile(t : Texture2D)
	{
		var path = AssetDatabase.GetAssetPath(t);
		var ti : TextureImporter = AssetImporter.GetAtPath(path);
		var p : Color[];
		var i : Texture2D;
		if(!ti.isReadable)
		{
			if(EditorUtility.DisplayDialog("Texture Not Readable", "The selected texture is not set to read/write. Would you like it to be set for you?", "Set", "Cancel"))
			{
				ti.isReadable = true;
				AssetDatabase.ImportAsset(path);
				
				p = t.GetPixels(0, 0, t.width, t.height);
				i = new Texture2D(t.width, t.height);
				i.SetPixels(0, 0, t.width, t.height, p);
				i.Apply();
				images[selectedImage] = i;
				image = i;
			}
		}
		else
		{
			p = t.GetPixels(0, 0, t.width, t.height);
			i = new Texture2D(t.width, t.height);
			i.SetPixels(0, 0, t.width, t.height, p);
			i.Apply();
			images[selectedImage] = i;
			image = i;
		}
	}
	
	function SaveTile()
	{
		var path = EditorUtility.SaveFilePanelInProject("Save Tile as PNG", "NewTile.png", "png", "Please enter a file name");
		if(path.Length != 0)
		{
			var bytes = images[selectedImage].EncodeToPNG();
			File.WriteAllBytes(path, bytes);
			AssetDatabase.Refresh();
			if(System.IO.File.Exists(path))
			{
				var ti : TextureImporter = AssetImporter.GetAtPath(path);
				ti.maxTextureSize = resolution;
				ti.isReadable = true;
				ti.mipmapEnabled = false;
				ti.linearTexture = true;
				ti.alphaIsTransparency = true;
				ti.wrapMode = TextureWrapMode.Clamp;
				ti.filterMode = FilterMode.Point;
				ti.textureFormat = TextureImporterFormat.AutomaticTruecolor;
				AssetDatabase.ImportAsset(path);
			}
		}
	}
	
	function SaveSheet()
	{
		var path = EditorUtility.SaveFilePanelInProject("Save Tileset as PNG", "NewTileset.png", "png", "Please enter a file name");
		if(path.Length != 0)
		{
			var sheet : Texture2D = new Texture2D(resolution, resolution);
			for(var a = 0; a < splits; a++)
			{
				for(var b = 0; b < splits; b++)
				{
					sheet.SetPixels(a * (resolution/splits), b * (resolution/splits), resolution/splits, resolution/splits, images[a * splits + b].GetPixels(0, 0, resolution/splits, resolution/splits));
				}
			}
			var bytes = sheet.EncodeToPNG();
			File.WriteAllBytes(path, bytes);
			AssetDatabase.Refresh();
			if(System.IO.File.Exists(path))
			{
				var ti : TextureImporter = AssetImporter.GetAtPath(path);
				ti.maxTextureSize = resolution;
				ti.isReadable = true;
				ti.mipmapEnabled = false;
				ti.linearTexture = true;
				ti.alphaIsTransparency = true;
				ti.wrapMode = TextureWrapMode.Clamp;
				ti.filterMode = FilterMode.Point;
				ti.textureFormat = TextureImporterFormat.AutomaticTruecolor;
				AssetDatabase.ImportAsset(path);
			}
		}
	}
	
	function DrawTexture()
	{
		var size : float = 400/(resolution/splits) + zoom;
		editScrollPosition = GUI.BeginScrollView(Rect(0, 0, 400, 400), editScrollPosition, Rect(0, 0, 400 + (resolution/splits * zoom), 400 + (resolution/splits * zoom)));
		for(var a = 0; a < resolution/splits; a++)
		{
			for(var b = 0; b < resolution/splits; b++)
			{
				var pixelRect : Rect = Rect(a * size, (((resolution/splits) - 1) - b) * size, size, size);
				var thisPixel : Rect = Rect(a, b, 1, 1);
				GUI.color = images[selectedImage].GetPixel(thisPixel.x, thisPixel.y);
				GUI.Box(pixelRect, "", "Box");
				if(pixelRect.Contains(Event.current.mousePosition))
				{
					if(Event.current.button == 1 && Event.current.type == EventType.MouseDrag || Event.current.button == 1 && Event.current.type == EventType.MouseDown)
					{
						images[selectedImage].SetPixel(thisPixel.x, thisPixel.y, Color(0.0, 0.0, 0.0, 0.0));
						images[selectedImage].Apply();
						Repaint();
					}
					if(Event.current.button == 0 && Event.current.type == EventType.MouseDrag || Event.current.button == 0 && Event.current.type == EventType.MouseDown)
					{
						if(tool == "Pencil")
						{
							images[selectedImage].SetPixel(thisPixel.x, thisPixel.y, currentColor);
							images[selectedImage].Apply();
						}
						if(tool == "Color Swap")
						{
							SwapColor(images[selectedImage].GetPixel(thisPixel.x, thisPixel.y));
							tool = "Pencil";
						}
						if(tool == "Color Picker")
						{
							currentColor = images[selectedImage].GetPixel(thisPixel.x, thisPixel.y);
							tool = "Pencil";
						}
						Repaint();
					}
				}
			}
		}
		GUI.EndScrollView();
	}
	
	function FillCanvas(size : int)
	{
		var colors : Color[] = new Color[size * size];
		for(var a = 0; a < size * size; a++)
		{
			colors[a] = Color.white;
		}
		return colors;
	}
	
	function SwapColor(c : Color)
	{
		for(var a = 0; a < resolution/2; a++)
		{
			for(var b = 0; b < resolution/2; b++)
			{
				if(images[selectedImage].GetPixel(a, b) == c)
				{
					images[selectedImage].SetPixel(a, b, currentColor);
				}
			}
		}
		images[selectedImage].Apply();
	}
	
	function DrawButtons()
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, false, GUILayout.Width(92), GUILayout.Height(286));
		for(var a = 0; a < images.Count; a++)
		{
			if(GUILayout.Button(GUIContent("Image " + a.ToString(), images[a])))
			{
				selectedImage = a;
				zoom = 0;
				Repaint();
			}
		}
		GUILayout.EndScrollView();
	}
	
	function DrawPalettes()
	{
		for(var a = 0; a < palettes.Count; a++)
		{
			GUILayout.BeginHorizontal();
			if(GUILayout.Button("Use"))currentColor = palettes[a];
			palettes[a] = EditorGUILayout.ColorField(palettes[a]);
			GUILayout.EndHorizontal();
		}
	}
}