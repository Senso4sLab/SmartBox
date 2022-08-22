using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using Microsoft.Maui.Platform;
using SmartBox.SmartConfig;

namespace SmartBox;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Arial.ttf", "ArialRegular");				
			})
			.Services
			.AddSingleton(typeof(IEspTouchConfigService), typeof(EspTouchConfigService))
			.AddSingleton(typeof(IWifiInfoService), typeof(WifiInfoService))
			.AddTransient(typeof(MainPage));


		ModifyEntry();
        return builder.Build();
	}


    public static void ModifyEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
#			if ANDROID
            //GradientDrawable gd = new GradientDrawable();
            //gd.SetColor(global::Android.Graphics.Color.Black);
			//handler.PlatformView.SetBackgroundColor(global::Android.Graphics.Color.Black);
			handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(global::Android.Graphics.Color.Black);
			#endif
        });
    }
}
