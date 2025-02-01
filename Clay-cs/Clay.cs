using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance;

[assembly: DisableRuntimeMarshalling]
namespace Clay_cs;

public unsafe delegate Clay_Dimensions ClayMeasureTextDelegate(Clay_String* text, Clay_TextElementConfig* config);

public delegate void ClayErrorDelegate(Clay_ErrorData data);

public delegate void ClayOnHoverDelegate(Clay_ElementId id, Clay_PointerData data, nint userData);

public delegate Clay_Vector2 ClayQueryScrollOffsetDelegate(uint elementId);

public static class Clay
{
	internal static readonly ClayStringCollection ClayStrings = new();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetMaxElementCount()
	{
		return ClayInterop.Clay_GetMaxElementCount();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetMaxElementCount(int maxElementCount)
	{
		ClayInterop.Clay_SetMaxElementCount(maxElementCount);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetMaxMeasureTextCacheWordCount()
	{
		return ClayInterop.Clay_GetMaxMeasureTextCacheWordCount();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetMaxMeasureTextCacheWordCount(int maxMeasureTextCacheWordCount)
	{
		ClayInterop.Clay_SetMaxMeasureTextCacheWordCount(maxMeasureTextCacheWordCount);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ResetMeasureTextCache()
	{
		ClayInterop.Clay_ResetMeasureTextCache();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint MinMemorySize() => ClayInterop.Clay_MinMemorySize();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe ClayArenaHandle CreateArena(uint memorySize)
	{
		var ptr = Marshal.AllocHGlobal((int)memorySize);
		var arena = ClayInterop.Clay_CreateArenaWithCapacityAndMemory(memorySize, (void*)ptr);
		return new ClayArenaHandle { Arena = arena, Memory = ptr };
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe Clay_Context* GetCurrentContext()
	{
		return ClayInterop.Clay_GetCurrentContext();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void SetCurrentContext(Clay_Context* context)
	{
		ClayInterop.Clay_SetCurrentContext(context);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void SetMeasureTextFunction(ClayMeasureTextDelegate measureText)
	{
		// TODO: do we need to dealloc the ptr?
		var ptr = Marshal.GetFunctionPointerForDelegate(measureText);
		var castPtr = (delegate* unmanaged[Cdecl]<Clay_String*, Clay_TextElementConfig*, Clay_Dimensions>)ptr;
		ClayInterop.Clay_SetMeasureTextFunction(castPtr);
	}

	public static unsafe void Initialize(
		ClayArenaHandle handle,
		Clay_Dimensions dimensions,
		ClayErrorDelegate errorHandler)
	{
		// TODO: handle userdata
		var ptr = Marshal.GetFunctionPointerForDelegate(errorHandler);
		var castPtr = (delegate* unmanaged[Cdecl]<Clay_ErrorData, void>)ptr;

		ClayInterop.Clay_Initialize(handle.Arena, dimensions, new Clay_ErrorHandler
		{
			errorHandlerFunction = castPtr
		});
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetCullingEnabled(bool state)
	{
		ClayInterop.Clay_SetCullingEnabled(state);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetDebugModeEnabled(bool state)
	{
		ClayInterop.Clay_SetDebugModeEnabled(state);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsDebugModeEnabled()
	{
		return ClayInterop.Clay_IsDebugModeEnabled();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void BeginLayout()
	{
		ClayInterop.Clay_BeginLayout();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Clay_RenderCommandArray EndLayout()
	{
		return ClayInterop.Clay_EndLayout();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe Clay_RenderCommand* RenderCommandArrayGet(Clay_RenderCommandArray arr, int index)
	{
		return ClayInterop.Clay_RenderCommandArray_Get(&arr, index);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetPointerState(Vector2 position, bool isMouseDown)
	{
		ClayInterop.Clay_SetPointerState(position, isMouseDown);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPointerOver(Clay_ElementId elementId)
	{
		return ClayInterop.Clay_PointerOver(elementId);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsHovered()
	{
		return ClayInterop.Clay_Hovered();
	}

	public static unsafe void OnHover(ClayOnHoverDelegate onHover, nint userData = 0)
	{
		var ptr = Marshal.GetFunctionPointerForDelegate(onHover);
		var castPtr = (delegate* unmanaged[Cdecl]<Clay_ElementId, Clay_PointerData, nint, void>)ptr;
		ClayInterop.Clay_OnHover(castPtr, userData);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Clay_ScrollContainerData GetScrollContainerData(Clay_ElementId id)
	{
		return ClayInterop.Clay_GetScrollContainerData(id);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void UpdateScrollContainers(bool enableDragScrolling, Vector2 moveDelta, float timeDelta)
	{
		ClayInterop.Clay_UpdateScrollContainers(enableDragScrolling, moveDelta, timeDelta);
	}

	public static unsafe void SetQueryScrollOffsetFunction(ClayQueryScrollOffsetDelegate queryScrollOffsetFunction)
	{
		var ptr = Marshal.GetFunctionPointerForDelegate(queryScrollOffsetFunction);
		var castPtr = (delegate* unmanaged[Cdecl]<uint, Clay_Vector2>)ptr;
		ClayInterop.Clay_SetQueryScrollOffsetFunction(castPtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetLayoutDimensions(Clay_Dimensions dimensions)
	{
		ClayInterop.Clay_SetLayoutDimensions(dimensions);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Clay_ElementId GetElementId(Clay_String idString)
	{
		return ClayInterop.Clay_GetElementId(idString);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Clay_ElementId GetElementId(Clay_String idString, uint index)
	{
		return ClayInterop.Clay_GetElementIdWithIndex(idString, index);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Clay_ElementData GetElementData(Clay_ElementId id)
	{
		return ClayInterop.Clay_GetElementData(id);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void OpenElement()
	{
		ClayInterop.Clay__OpenElement();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void OpenTextElement(string text, Clay_TextElementConfig c)
	{
		OpenTextElement(ClayStrings.Get(text), c);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void OpenTextElement(Clay_String text, Clay_TextElementConfig c)
	{
		ClayInterop.Clay__OpenTextElement(text, ClayInterop.Clay__StoreTextElementConfig(c));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ElementPostConfiguration()
	{
		ClayInterop.Clay__ElementPostConfiguration();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void CloseElement()
	{
		ClayInterop.Clay__CloseElement();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint GetParentElementId()
	{
		return ClayInterop.Clay__GetParentElementId();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Id(string text)
	{
		Id(ClayStrings.Get(text));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Id(Clay_String text)
	{
		AttachId(text, 0, 0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Id(string text, int offset)
	{
		Id(ClayStrings.Get(text), offset);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Id(Clay_String text, int offset)
	{
		AttachId(text, (uint)offset, 0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IdLocal(string text)
	{
		IdLocal(ClayStrings.Get(text));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IdLocal(Clay_String text)
	{
		AttachId(text, 0, GetParentElementId());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IdLocal(string text, int offset)
	{
		IdLocal(ClayStrings.Get(text), offset);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void IdLocal(Clay_String text, int offset)
	{
		AttachId(text, (uint)offset, GetParentElementId());
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void AttachId(Clay_String text, uint offset, uint seed)
	{
		ClayInterop.Clay__AttachId(ClayInterop.Clay__HashString(text, offset, seed));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Layout(Clay_LayoutConfig c)
	{
		ClayInterop.Clay__AttachLayoutConfig(ClayInterop.Clay__StoreLayoutConfig(c));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Rectangle(Clay_RectangleElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			rectangleElementConfig = ClayInterop.Clay__StoreRectangleElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_RECTANGLE);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Text(Clay_TextElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			textElementConfig = ClayInterop.Clay__StoreTextElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_TEXT);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Image(Clay_ImageElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			imageElementConfig = ClayInterop.Clay__StoreImageElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_IMAGE);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Floating(Clay_FloatingElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			floatingElementConfig = ClayInterop.Clay__StoreFloatingElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_FLOATING_CONTAINER);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Custom(Clay_CustomElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			customElementConfig = ClayInterop.Clay__StoreCustomElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_CUSTOM);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Scroll(Clay_ScrollElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			scrollElementConfig = ClayInterop.Clay__StoreScrollElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_SCROLL_CONTAINER);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void Border(Clay_BorderElementConfig c)
	{
		ClayInterop.Clay__AttachElementConfig(new Clay_ElementConfigUnion
		{
			borderElementConfig = ClayInterop.Clay__StoreBorderElementConfig(c)
		}, Clay__ElementConfigType.CLAY__ELEMENT_CONFIG_TYPE_BORDER_CONTAINER);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte AsByte(this bool b) => b.ToByte();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool AsBool(this byte b) => b != 0;
}