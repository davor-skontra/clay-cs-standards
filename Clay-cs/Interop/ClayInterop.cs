using System.Runtime.InteropServices;

namespace Clay_cs;

public unsafe partial struct Clay_String
{
    [NativeTypeName("int32_t")]
    public int length;

    [NativeTypeName("const char *")]
    public sbyte* chars;
}

    public partial struct Clay_Context
    {
    }

    public unsafe partial struct Clay_Arena
    {
        [NativeTypeName("uintptr_t")]
        public nuint nextAllocation;

        [NativeTypeName("size_t")]
        public nuint capacity;

        [NativeTypeName("char *")]
        public sbyte* memory;
    }

    public partial struct Clay_Dimensions
    {
        public float width;

        public float height;
    }

    public partial struct Clay_Vector2
    {
        public float x;

        public float y;
    }

    public partial struct Clay_Color
    {
        public float r;

        public float g;

        public float b;

        public float a;
    }

    public partial struct Clay_BoundingBox
    {
        public float x;

        public float y;

        public float width;

        public float height;
    }

    public partial struct Clay_ElementId
    {
        [NativeTypeName("uint32_t")]
        public uint id;

        [NativeTypeName("uint32_t")]
        public uint offset;

        [NativeTypeName("uint32_t")]
        public uint baseId;

        public Clay_String stringId;
    }

    public partial struct Clay_CornerRadius
    {
        public float topLeft;

        public float topRight;

        public float bottomLeft;

        public float bottomRight;
    }

    [NativeTypeName("uint8_t")]
    public enum Clay__ElementConfigType : byte
    {
        CLAY__ELEMENT_CONFIG_TYPE_NONE = 0,
        CLAY__ELEMENT_CONFIG_TYPE_RECTANGLE = 1,
        CLAY__ELEMENT_CONFIG_TYPE_BORDER_CONTAINER = 2,
        CLAY__ELEMENT_CONFIG_TYPE_FLOATING_CONTAINER = 4,
        CLAY__ELEMENT_CONFIG_TYPE_SCROLL_CONTAINER = 8,
        CLAY__ELEMENT_CONFIG_TYPE_IMAGE = 16,
        CLAY__ELEMENT_CONFIG_TYPE_TEXT = 32,
        CLAY__ELEMENT_CONFIG_TYPE_CUSTOM = 64,
    }

    [NativeTypeName("uint8_t")]
    public enum Clay_LayoutDirection : byte
    {
        CLAY_LEFT_TO_RIGHT,
        CLAY_TOP_TO_BOTTOM,
    }

    [NativeTypeName("uint8_t")]
    public enum Clay_LayoutAlignmentX : byte
    {
        CLAY_ALIGN_X_LEFT,
        CLAY_ALIGN_X_RIGHT,
        CLAY_ALIGN_X_CENTER,
    }

    [NativeTypeName("uint8_t")]
    public enum Clay_LayoutAlignmentY : byte
    {
        CLAY_ALIGN_Y_TOP,
        CLAY_ALIGN_Y_BOTTOM,
        CLAY_ALIGN_Y_CENTER,
    }

    [NativeTypeName("uint8_t")]
    public enum Clay__SizingType : byte
    {
        CLAY__SIZING_TYPE_FIT,
        CLAY__SIZING_TYPE_GROW,
        CLAY__SIZING_TYPE_PERCENT,
        CLAY__SIZING_TYPE_FIXED,
    }

    public partial struct Clay_ChildAlignment
    {
        public Clay_LayoutAlignmentX x;

        public Clay_LayoutAlignmentY y;
    }

    public partial struct Clay_SizingMinMax
    {
        public float min;

        public float max;
    }

    public partial struct Clay_SizingAxis
    {
        [NativeTypeName("__AnonymousRecord_clay_L287_C5")]
        public ClaySizingUnion size;

        public Clay__SizingType type;

        [StructLayout(LayoutKind.Explicit)]
        public partial struct ClaySizingUnion
        {
            [FieldOffset(0)]
            public Clay_SizingMinMax minMax;

            [FieldOffset(0)]
            public float percent;
        }
    }

    public partial struct Clay_Sizing
    {
        public Clay_SizingAxis width;

        public Clay_SizingAxis height;
    }

    public partial struct Clay_Padding
    {
        [NativeTypeName("uint16_t")]
        public ushort left;

        [NativeTypeName("uint16_t")]
        public ushort right;

        [NativeTypeName("uint16_t")]
        public ushort top;

        [NativeTypeName("uint16_t")]
        public ushort bottom;
    }

    public partial struct Clay_LayoutConfig
    {
        public Clay_Sizing sizing;

        public Clay_Padding padding;

        [NativeTypeName("uint16_t")]
        public ushort childGap;

        public Clay_ChildAlignment childAlignment;

        public Clay_LayoutDirection layoutDirection;
    }

    public partial struct Clay_RectangleElementConfig
    {
        public Clay_Color color;

        public Clay_CornerRadius cornerRadius;
    }

    public enum Clay_TextElementConfigWrapMode
    {
        CLAY_TEXT_WRAP_WORDS,
        CLAY_TEXT_WRAP_NEWLINES,
        CLAY_TEXT_WRAP_NONE,
    }

    public partial struct Clay_TextElementConfig
    {
        public Clay_Color textColor;

        [NativeTypeName("uint16_t")]
        public ushort fontId;

        [NativeTypeName("uint16_t")]
        public ushort fontSize;

        [NativeTypeName("uint16_t")]
        public ushort letterSpacing;

        [NativeTypeName("uint16_t")]
        public ushort lineHeight;

        public Clay_TextElementConfigWrapMode wrapMode;
    }

    public unsafe partial struct Clay_ImageElementConfig
    {
        public void* imageData;

        public Clay_Dimensions sourceDimensions;
    }

    [NativeTypeName("uint8_t")]
    public enum Clay_FloatingAttachPointType : byte
    {
        CLAY_ATTACH_POINT_LEFT_TOP,
        CLAY_ATTACH_POINT_LEFT_CENTER,
        CLAY_ATTACH_POINT_LEFT_BOTTOM,
        CLAY_ATTACH_POINT_CENTER_TOP,
        CLAY_ATTACH_POINT_CENTER_CENTER,
        CLAY_ATTACH_POINT_CENTER_BOTTOM,
        CLAY_ATTACH_POINT_RIGHT_TOP,
        CLAY_ATTACH_POINT_RIGHT_CENTER,
        CLAY_ATTACH_POINT_RIGHT_BOTTOM,
    }

    public partial struct Clay_FloatingAttachPoints
    {
        public Clay_FloatingAttachPointType element;

        public Clay_FloatingAttachPointType parent;
    }

    public enum Clay_PointerCaptureMode
    {
        CLAY_POINTER_CAPTURE_MODE_CAPTURE,
        CLAY_POINTER_CAPTURE_MODE_PASSTHROUGH,
    }

    public partial struct Clay_FloatingElementConfig
    {
        public Clay_Vector2 offset;

        public Clay_Dimensions expand;

        [NativeTypeName("uint16_t")]
        public ushort zIndex;

        [NativeTypeName("uint32_t")]
        public uint parentId;

        public Clay_FloatingAttachPoints attachment;

        public Clay_PointerCaptureMode pointerCaptureMode;
    }

    public unsafe partial struct Clay_CustomElementConfig
    {
        public void* customData;
    }

    public partial struct Clay_ScrollElementConfig
    {
        public bool horizontal;

        public bool vertical;
    }

    public partial struct Clay_Border
    {
        [NativeTypeName("uint32_t")]
        public uint width;

        public Clay_Color color;
    }

    public partial struct Clay_BorderElementConfig
    {
        public Clay_Border left;

        public Clay_Border right;

        public Clay_Border top;

        public Clay_Border bottom;

        public Clay_Border betweenChildren;

        public Clay_CornerRadius cornerRadius;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe partial struct Clay_ElementConfigUnion
    {
        [FieldOffset(0)]
        public Clay_RectangleElementConfig* rectangleElementConfig;

        [FieldOffset(0)]
        public Clay_TextElementConfig* textElementConfig;

        [FieldOffset(0)]
        public Clay_ImageElementConfig* imageElementConfig;

        [FieldOffset(0)]
        public Clay_FloatingElementConfig* floatingElementConfig;

        [FieldOffset(0)]
        public Clay_CustomElementConfig* customElementConfig;

        [FieldOffset(0)]
        public Clay_ScrollElementConfig* scrollElementConfig;

        [FieldOffset(0)]
        public Clay_BorderElementConfig* borderElementConfig;
    }

    public partial struct Clay_ElementConfig
    {
        public Clay__ElementConfigType type;

        public Clay_ElementConfigUnion config;
    }

    public unsafe partial struct Clay_ScrollContainerData
    {
        public Clay_Vector2* scrollPosition;

        public Clay_Dimensions scrollContainerDimensions;

        public Clay_Dimensions contentDimensions;

        public Clay_ScrollElementConfig config;

        public bool found;
    }

    public partial struct Clay_ElementData
    {
        public Clay_BoundingBox boundingBox;

        public bool found;
    }

    [NativeTypeName("uint8_t")]
    public enum Clay_RenderCommandType : byte
    {
        CLAY_RENDER_COMMAND_TYPE_NONE,
        CLAY_RENDER_COMMAND_TYPE_RECTANGLE,
        CLAY_RENDER_COMMAND_TYPE_BORDER,
        CLAY_RENDER_COMMAND_TYPE_TEXT,
        CLAY_RENDER_COMMAND_TYPE_IMAGE,
        CLAY_RENDER_COMMAND_TYPE_SCISSOR_START,
        CLAY_RENDER_COMMAND_TYPE_SCISSOR_END,
        CLAY_RENDER_COMMAND_TYPE_CUSTOM,
    }

    public partial struct Clay_RenderCommand
    {
        public Clay_BoundingBox boundingBox;

        public Clay_ElementConfigUnion config;

        public Clay_String text;

        [NativeTypeName("uint32_t")]
        public uint id;

        public Clay_RenderCommandType commandType;
    }

    public unsafe partial struct Clay_RenderCommandArray
    {
        [NativeTypeName("int32_t")]
        public int capacity;

        [NativeTypeName("int32_t")]
        public int length;

        public Clay_RenderCommand* internalArray;
    }

    public enum Clay_PointerDataInteractionState
    {
        CLAY_POINTER_DATA_PRESSED_THIS_FRAME,
        CLAY_POINTER_DATA_PRESSED,
        CLAY_POINTER_DATA_RELEASED_THIS_FRAME,
        CLAY_POINTER_DATA_RELEASED,
    }

    public partial struct Clay_PointerData
    {
        public Clay_Vector2 position;

        public Clay_PointerDataInteractionState state;
    }

    public enum Clay_ErrorType
    {
        CLAY_ERROR_TYPE_TEXT_MEASUREMENT_FUNCTION_NOT_PROVIDED,
        CLAY_ERROR_TYPE_ARENA_CAPACITY_EXCEEDED,
        CLAY_ERROR_TYPE_ELEMENTS_CAPACITY_EXCEEDED,
        CLAY_ERROR_TYPE_TEXT_MEASUREMENT_CAPACITY_EXCEEDED,
        CLAY_ERROR_TYPE_DUPLICATE_ID,
        CLAY_ERROR_TYPE_FLOATING_CONTAINER_PARENT_NOT_FOUND,
        CLAY_ERROR_TYPE_INTERNAL_ERROR,
    }

    public partial struct Clay_ErrorData
    {
        public Clay_ErrorType errorType;

        public Clay_String errorText;

        [NativeTypeName("uintptr_t")]
        public nuint userData;
    }

    public unsafe partial struct Clay_ErrorHandler
    {
        [NativeTypeName("void (*)(Clay_ErrorData)")]
        public delegate* unmanaged[Cdecl]<Clay_ErrorData, void> errorHandlerFunction;

        [NativeTypeName("uintptr_t")]
        public nuint userData;
    }

    internal static unsafe partial class ClayInterop
    {
        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint Clay_MinMemorySize();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_Arena Clay_CreateArenaWithCapacityAndMemory([NativeTypeName("uint32_t")] uint capacity, void* offset);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetPointerState(Clay_Vector2 position, bool pointerDown);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_Context* Clay_Initialize(Clay_Arena arena, Clay_Dimensions layoutDimensions, Clay_ErrorHandler errorHandler);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_Context* Clay_GetCurrentContext();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetCurrentContext(Clay_Context* context);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_UpdateScrollContainers(bool enableDragScrolling, Clay_Vector2 scrollDelta, float deltaTime);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetLayoutDimensions(Clay_Dimensions dimensions);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_BeginLayout();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_RenderCommandArray Clay_EndLayout();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_ElementId Clay_GetElementId(Clay_String idString);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_ElementId Clay_GetElementIdWithIndex(Clay_String idString, [NativeTypeName("uint32_t")] uint index);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_ElementData Clay_GetElementData(Clay_ElementId id);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool Clay_Hovered();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_OnHover([NativeTypeName("void (*)(Clay_ElementId, Clay_PointerData, intptr_t)")] delegate* unmanaged[Cdecl]<Clay_ElementId, Clay_PointerData, nint, void> onHoverFunction, [NativeTypeName("intptr_t")] nint userData);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool Clay_PointerOver(Clay_ElementId elementId);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_ScrollContainerData Clay_GetScrollContainerData(Clay_ElementId id);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetMeasureTextFunction([NativeTypeName("Clay_Dimensions (*)(Clay_String *, Clay_TextElementConfig *)")] delegate* unmanaged[Cdecl]<Clay_String*, Clay_TextElementConfig*, Clay_Dimensions> measureTextFunction);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetQueryScrollOffsetFunction([NativeTypeName("Clay_Vector2 (*)(uint32_t)")] delegate* unmanaged[Cdecl]<uint, Clay_Vector2> queryScrollOffsetFunction);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_RenderCommand* Clay_RenderCommandArray_Get(Clay_RenderCommandArray* array, [NativeTypeName("int32_t")] int index);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetDebugModeEnabled(bool enabled);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool Clay_IsDebugModeEnabled();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetCullingEnabled(bool enabled);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int Clay_GetMaxElementCount();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetMaxElementCount([NativeTypeName("int32_t")] int maxElementCount);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("int32_t")]
        public static extern int Clay_GetMaxMeasureTextCacheWordCount();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_SetMaxMeasureTextCacheWordCount([NativeTypeName("int32_t")] int maxMeasureTextCacheWordCount);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay_ResetMeasureTextCache();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay__OpenElement();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay__CloseElement();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_LayoutConfig* Clay__StoreLayoutConfig(Clay_LayoutConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay__ElementPostConfiguration();

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay__AttachId(Clay_ElementId id);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay__AttachLayoutConfig(Clay_LayoutConfig* config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay__AttachElementConfig(Clay_ElementConfigUnion config, Clay__ElementConfigType type);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_RectangleElementConfig* Clay__StoreRectangleElementConfig(Clay_RectangleElementConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_TextElementConfig* Clay__StoreTextElementConfig(Clay_TextElementConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_ImageElementConfig* Clay__StoreImageElementConfig(Clay_ImageElementConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_FloatingElementConfig* Clay__StoreFloatingElementConfig(Clay_FloatingElementConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_CustomElementConfig* Clay__StoreCustomElementConfig(Clay_CustomElementConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_ScrollElementConfig* Clay__StoreScrollElementConfig(Clay_ScrollElementConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_BorderElementConfig* Clay__StoreBorderElementConfig(Clay_BorderElementConfig config);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Clay_ElementId Clay__HashString(Clay_String key, [NativeTypeName("uint32_t")] uint offset, [NativeTypeName("uint32_t")] uint seed);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void Clay__OpenTextElement(Clay_String text, Clay_TextElementConfig* textConfig);

        [DllImport("Clay", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: NativeTypeName("uint32_t")]
        public static extern uint Clay__GetParentElementId();
    }
