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

	public static void SetDebugModeEnabled(bool state)
	{
		ClayInterop.Clay_SetDebugModeEnabled(ToByte(state));
	}

	public static void BeginLayout() => ClayInterop.Clay_BeginLayout();
	public static Clay_RenderCommandArray EndLayout() => ClayInterop.Clay_EndLayout();

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

	public static unsafe void Layout(Clay_LayoutConfig c)
	{
		ClayInterop.Clay__AttachLayoutConfig(ClayInterop.Clay__StoreLayoutConfig(c));
	}

	// public static unsafe void Rectangle(Clay_LayoutConfig c)
	// {
	// ClayInterop.Clay__AttachElementConfig(ClayUnsafe);
	// }
}