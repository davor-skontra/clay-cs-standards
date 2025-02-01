namespace Clay_cs;

public struct ClayElement : IDisposable
{
	public ClayElement(ClayConfig config)
	{
		Clay.OpenElement();

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
		Clay.ElementPostConfiguration();
	}

	public void Dispose()
	{
		Clay.CloseElement();
	}
}