namespace Clay_cs;

public struct ClayElement : IDisposable
{
	public static ClayElement Open()
	{
		Clay.OpenElement();
		return new ClayElement();
	}
	
	public static ClayElement OpenAndSubmit(ClayConfig config)
	{
		return Open().SetConfig(config).Submit();
	}

	public ClayElement SetConfig(ClayConfig config)
	{
		if (config.Id.HasValue)
		{
			var id = config.Id.Value;
			Clay.AttachId(id.Text, id.Offset, id.Seed);
		}

		if (config.Layout.HasValue) Clay.Layout(config.Layout.Value);
		if (config.Rectangle.HasValue) Clay.Rectangle(config.Rectangle.Value);
		if (config.Text.HasValue) Clay.Text(config.Text.Value);
		if (config.Image.HasValue) Clay.Image(config.Image.Value);
		if (config.Border.HasValue) Clay.Border(config.Border.Value);
		if (config.Floating.HasValue) Clay.Floating(config.Floating.Value);
		if (config.Scroll.HasValue) Clay.Scroll(config.Scroll.Value);
		return this;
	}

	public ClayElement Submit()
	{
		Clay.ElementPostConfiguration();
		return this;
	}

	public void Dispose()
	{
		Clay.CloseElement();
	}
}