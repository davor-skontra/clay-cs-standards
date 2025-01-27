using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using ZeroElectric.Vinculum;
using ZeroElectric.Vinculum.Extensions;

namespace Clay_cs.Example;

public static class RaylibClay
{
	public static Font[] Fonts = new Font[10];

	private static Color ToColor(Clay_Color color) => new Color
	{
		r = (byte)MathF.Round(color.r),
		g = (byte)MathF.Round(color.g),
		b = (byte)MathF.Round(color.b),
		a = (byte)MathF.Round(color.a),
	};

	public static unsafe Clay_Dimensions MeasureText(Clay_String* str, Clay_TextElementConfig* config)
	{
		var text = str->ToCSharpString();
		
		// valid font?
		if (config->fontId > Fonts.Length) return default;
		var font = Fonts[config->fontId];
		if (font.glyphs == null) return default;

		var factor = (float)config->fontSize / font.baseSize;


		float maxTextWidth = 0;
		float currentTextWidth = 0;

		for (int i = 0; i < str->length; i++)
		{
			var ch = str->chars[i];
			if (ch == '\n')
			{
				maxTextWidth = Math.Max(maxTextWidth, currentTextWidth);
				currentTextWidth = 0;
				continue;
			}

			int index = str->chars[i] - 32;
			if (font.glyphs[index].advanceX != 0) currentTextWidth += font.glyphs[index].advanceX;
			else currentTextWidth += font.recs[index].width + font.glyphs[index].offsetX;
		}

		maxTextWidth = Math.Max(maxTextWidth, currentTextWidth);
		return new Clay_Dimensions
		{
			width = maxTextWidth * factor,
			height = config->fontSize,
		};
	}

	public static unsafe void RenderCommands(Clay_RenderCommandArray array)
	{
		for (int i = 0; i < array.length; i++)
		{
			var renderCommand = ClayInterop.Clay_RenderCommandArray_Get(&array, i);
			Clay_BoundingBox boundingBox = renderCommand->boundingBox;

			switch (renderCommand->commandType)
			{
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_NONE:
					break;
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_RECTANGLE:
				{
					var config = renderCommand->config.rectangleElementConfig;

					var box = new Rectangle(boundingBox.x, boundingBox.y, boundingBox.width, boundingBox.height);
					var color = ToColor(config->color);

					if (config->cornerRadius.topLeft > 0)
					{
						float radius = (config->cornerRadius.topLeft * 2) /
						               (float)((boundingBox.width > boundingBox.height) ? boundingBox.height : boundingBox.width);
						Raylib.DrawRectangleRounded(box, radius, 8, color);
					}
					else
					{
						Raylib.DrawRectangleRec(box, color);
					}

					break;
				}
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_BORDER:
					// throw new NotImplementedException();
					break;
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_TEXT:
				{
					var asStr = renderCommand->text.ToCSharpString();
					var span = renderCommand->text.ToSpanOwner();
					var font = Fonts[renderCommand->config.textElementConfig->fontId];

					try
					{
						Raylib.DrawTextEx(
							font, span.AsPtr(),
							new Vector2(boundingBox.x, boundingBox.y),
							renderCommand->config.textElementConfig->fontSize,
							renderCommand->config.textElementConfig->letterSpacing,
							ToColor(renderCommand->config.textElementConfig->textColor)
						);
					}
					finally
					{
						span.Dispose();
					} 
					break;
				}
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_IMAGE:
				{
					Texture texture = *(Texture*)renderCommand->config.imageElementConfig->imageData;
					Raylib.DrawTextureEx(texture,
						new Vector2(boundingBox.x, boundingBox.y),
						0,
						boundingBox.width / boundingBox.width,
						Raylib.WHITE
					);
					break;
				}
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_SCISSOR_START:
				{
					Raylib.BeginScissorMode((int)boundingBox.x, (int)boundingBox.y, (int)boundingBox.width,
						(int)boundingBox.height);
					break;
				}
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_SCISSOR_END:
				{
					Raylib.EndScissorMode();
					break;
				}
				case Clay_RenderCommandType.CLAY_RENDER_COMMAND_TYPE_CUSTOM:
					// throw new NotImplementedException();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}