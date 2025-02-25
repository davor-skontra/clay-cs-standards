namespace Clay_cs;

/// <summary>
/// stores managed c# data so it doesn't get collected by the GC before we want it.
/// </summary>
internal class ClayManagedContext
{
	public nint ArenaMemory;
	public ClayErrorDelegate ErrorHandler;
	public ClayQueryScrollOffsetDelegate QueryScrollOffset;
	public List<ClayOnHoverDelegate> OnHover = [];
}
