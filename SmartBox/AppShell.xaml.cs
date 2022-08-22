namespace SmartBox;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(PopUp), typeof(PopUp));
	}
}
