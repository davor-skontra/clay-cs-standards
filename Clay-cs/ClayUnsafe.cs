using System.Runtime.InteropServices;

namespace Clay_cs;

public static class ClayUnsafe
{
	public static uint MinMemorySize() => ClayInterop.Clay_MinMemorySize();

	public static unsafe (Clay_Arena arena, nint arenaPtr) CreateArena(uint memorySize)
	{
		var ptr = Marshal.AllocHGlobal((int)memorySize);
		var arena = ClayInterop.Clay_CreateArenaWithCapacityAndMemory(memorySize, (void*)ptr);
		return (arena, ptr);
	}

	public static void FreeArena((Clay_Arena arena, nint arenaPtr) arena)
	{
		Marshal.FreeHGlobal(arena.arenaPtr);
	}

	public unsafe delegate Clay_Dimensions ClayMeasureTextDelegate(Clay_String* text, Clay_TextElementConfig* config);


	public static unsafe void SetMeasureTextFunction(ClayMeasureTextDelegate measureText)
	{
		// TODO: do we need to dealloc the ptr?
		var ptr = Marshal.GetFunctionPointerForDelegate(measureText);
		var castPtr = (delegate* unmanaged[Cdecl]<Clay_String*, Clay_TextElementConfig*, Clay_Dimensions>)ptr;
		ClayInterop.Clay_SetMeasureTextFunction(castPtr);
	}

	public delegate void ClayErrorDelegate(Clay_ErrorData data);

	public static unsafe void Initialize(Clay_Arena arena, Clay_Dimensions dimensions, ClayErrorDelegate errorHandler)
	{
		// TODO: handle userdata
		// TODO: do we need to dealloc the ptr?
		var ptr = Marshal.GetFunctionPointerForDelegate(errorHandler);
		var castPtr = (delegate* unmanaged[Cdecl]<Clay_ErrorData, void>)ptr;

		ClayInterop.Clay_Initialize(arena, dimensions, new Clay_ErrorHandler
		{
			errorHandlerFunction = castPtr
		});
	}

	private static byte ToByte(bool b) => b ? (byte)1 : (byte)0;
	private static bool FromByte(byte b) => b != 0;

	public static void SetDebugModeEnabled(bool state)
	{
		ClayInterop.Clay_SetDebugModeEnabled(ToByte(state));
	}

	public static bool IsDebugModeEnabled()
	{
		return FromByte(ClayInterop.Clay_IsDebugModeEnabled());
	}

	public static void BeginLayout()
	{
		ClayInterop.Clay_BeginLayout();
	}

	public static Clay_RenderCommandArray EndLayout()
	{
		return ClayInterop.Clay_EndLayout();
	}

	public static unsafe Clay_RenderCommand* RenderCommandArrayGet(Clay_RenderCommandArray arr, int index)
	{
		return ClayInterop.Clay_RenderCommandArray_Get(&arr, index);
	}

	public static void SetPointerState(Clay_Vector2 position, bool isMouseDown)
	{
		ClayInterop.Clay_SetPointerState(position, ToByte(isMouseDown));
	}

	public static void UpdateScrollContainers(bool enableDragScrolling, Clay_Vector2 moveDelta, float timeDelta)
	{
		ClayInterop.Clay_UpdateScrollContainers(ToByte(enableDragScrolling), moveDelta, timeDelta);
	}

	public static void SetLayoutDimensions(Clay_Dimensions dimensions)
	{
		ClayInterop.Clay_SetLayoutDimensions(dimensions);
	}

	public static void OpenElement()
	{
		ClayInterop.Clay__OpenElement();
	}

	public static void ElementPostConfiguration()
	{
		ClayInterop.Clay__ElementPostConfiguration();
	}

	public static void CloseElement()
	{
		ClayInterop.Clay__CloseElement();
	}

	public static unsafe void Layout(Clay_LayoutConfig c)
	{
		ClayInterop.Clay__AttachLayoutConfig(ClayInterop.Clay__StoreLayoutConfig(c));
	}

	public static unsafe void Rectangle(Clay_RectangleElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			rectangleElementConfig = ClayInterop.Clay__StoreRectangleElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_RECTANGLE);
	}

	public static unsafe void TextElement(Clay_TextElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			textElementConfig = ClayInterop.Clay__StoreTextElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_TEXT);
	}
	
	public static unsafe void Image(Clay_ImageElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			imageElementConfig = ClayInterop.Clay__StoreImageElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_IMAGE);
	}
	
	public static unsafe void Floating(Clay_FloatingElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			floatingElementConfig = ClayInterop.Clay__StoreFloatingElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_FLOATING_CONTAINER);
	}
	
	public static unsafe void Custom(Clay_CustomElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			customElementConfig = ClayInterop.Clay__StoreCustomElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_CUSTOM);
	}
	
	public static unsafe void Scroll(Clay_ScrollElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			scrollElementConfig = ClayInterop.Clay__StoreScrollElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_SCROLL_CONTAINER);
	}
	
	public static unsafe void Border(Clay_BorderElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			borderElementConfig = ClayInterop.Clay__StoreBorderElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_BORDER_CONTAINER);
	}
}