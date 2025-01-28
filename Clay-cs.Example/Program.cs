// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Clay_cs;
using Clay_cs.Example;
using CommunityToolkit.HighPerformance;
using ZeroElectric.Vinculum;


void ErrorHandler(Clay_ErrorData data)
{
	Console.WriteLine($"{data.errorType}: {data.errorText.ToCSharpString()}");
}

unsafe
{
	Console.WriteLine("Hello, World!");


	var memorySize = ClayUnsafe.MinMemorySize();
	var arena = ClayUnsafe.CreateArena(memorySize);
	ClayUnsafe.SetMeasureTextFunction(RaylibClay.MeasureText);

	var dimensions = new Clay_Dimensions
	{
		width = 600,
		height = 600,
	};
	ClayUnsafe.Initialize(arena.arena, dimensions, ErrorHandler);
	ClayUnsafe.SetDebugModeEnabled(true);

	Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
	Raylib.InitWindow((int)dimensions.width, (int)dimensions.height, "C# Clay Raylib Demo");
	RaylibClay.Fonts[0] = Raylib.LoadFont("resources/Roboto-Regular.ttf");
	Raylib.SetTextureFilter(RaylibClay.Fonts[0].texture, TextureFilter.TEXTURE_FILTER_BILINEAR);

	while (Raylib.WindowShouldClose() == false)
	{
		dimensions = new Clay_Dimensions
		{
			width = Raylib.GetScreenWidth(),
			height = Raylib.GetScreenHeight(),
		};

		ClayUnsafe.BeginLayout();
		ClayUnsafe.SetPointerState(new Clay_Vector2
		{
			x = Raylib.GetMouseX(),
			y = Raylib.GetMouseY()
		}, Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT));

		var delta = Raylib.GetMouseDelta();
		ClayUnsafe.UpdateScrollContainers(true,
			new Clay_Vector2
			{
				y = delta.Y,
				x = delta.X
			}, Raylib.GetFrameTime()
		);
		ClayUnsafe.SetLayoutDimensions(dimensions);

		for (var i = 0; i < 25; i++)
		{
			ClayUnsafe.OpenElement();


			// string id = String.Intern("Test");
			// var bytes = Utils.ToSpanOwner(id);
			// fixed (byte* idp = bytes.Span)
			// {
				// var length = Encoding.UTF8.GetByteCount(id);
				// ClayInterop.Clay__AttachId(
					// ClayInterop.Clay__HashString(new Clay_String { chars = (sbyte*)idp, length = bytes.Length }, (uint)i, 0));
			// }

			ClayUnsafe.Layout(new Clay_LayoutConfig
			{
				padding = new Clay_Padding
				{
					top = 10,
					left = 10,
					right = 10,
					bottom = 10,
				},
				childGap = 10,
				childAlignment = new Clay_ChildAlignment
				{
					x = Clay_LayoutAlignmentX.CLAY_ALIGN_X_LEFT,
					y = Clay_LayoutAlignmentY.CLAY_ALIGN_Y_TOP,
				},
				sizing = new Clay_Sizing
				{
					width = new Clay_SizingAxis
					{
						type = Clay__SizingType.CLAY__SIZING_TYPE_FIXED, size = new Clay_SizingAxis._size_e__Union
						{
							minMax = new Clay_SizingMinMax
							{
								min = 100,
								max = 100,
							}
						}
					},
					height = new Clay_SizingAxis
					{
						type = Clay__SizingType.CLAY__SIZING_TYPE_FIXED, size = new Clay_SizingAxis._size_e__Union
						{
							minMax = new Clay_SizingMinMax
							{
								min = 100,
								max = 100,
							}
						}
					},
				}
			});
			ClayUnsafe.Rectangle(new Clay_RectangleElementConfig
			{
				color = new Clay_Color { r = 255, a = 255 },
			});
			ClayUnsafe.ElementPostConfiguration();

			ClayUnsafe.CloseElement();
		}


		var commands = ClayUnsafe.EndLayout();

		Raylib.BeginDrawing();
		Raylib.ClearBackground(Raylib.LIME);
		RaylibClay.RenderCommands(commands);
		Raylib.DrawTextEx(RaylibClay.Fonts[0], "This text is rendered using direct raylib", new Vector2(), 16, 0,
			Raylib.WHITE);
		Raylib.EndDrawing();
	}

	ClayUnsafe.FreeArena(arena);
	Console.WriteLine("~ fin ~");
}
