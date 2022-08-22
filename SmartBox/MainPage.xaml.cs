using Acr.UserDialogs;
using AndroidHUD;
using SmartBox.SmartConfig;
using System.Linq;

namespace SmartBox;

public partial class MainPage : ContentPage
{	
	private IEspTouchConfigService EspTouchService { get; }
    private IWifiInfoService WifiService { get; }

	private WifiDeviceInfo wifiDeviceInfo;
    public WifiDeviceInfo WifiDeviceInfo
	{
		get => wifiDeviceInfo ??= new WifiDeviceInfo();    
		set
		{
			wifiDeviceInfo = value;
			OnPropertyChanged();
		}
	}
	private bool isVisibleIndicator = false;
    public bool IsVisibleIndicator
	{
		get => isVisibleIndicator;

        set
		{
			isVisibleIndicator = value;
			OnPropertyChanged();
		}
	}

    public MainPage(IEspTouchConfigService espTouchService, IWifiInfoService wifiService)
	{
		InitializeComponent();

        this.WifiService = wifiService;
        this.EspTouchService = espTouchService;
		this.BindingContext = this;
	}

	protected async override void OnAppearing()
	{
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
            await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

		WifiService.WifiStateChanged += WifiStateChanged;
		WifiService.ActivateWifiService();
       
        base.OnAppearing();
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
        WifiService.WifiStateChanged -= WifiStateChanged;
        WifiService.DeactivateWifiService();
    }

	private void WifiStateChanged(object sender, WifiDeviceInfo e) =>
		WifiDeviceInfo = e;
	

	private async void OnCounterClicked(object sender, EventArgs e)
	{

        await Shell.Current.GoToAsync(nameof(PopUp));




        //userDialog.Toast()

        //this.IsVisibleIndicator = true;
        //      var espTouchResult = (await EspTouchService.ExecuteForResultsAsync(WifiDeviceInfo, 1)).FirstOrDefault();

        //this.EspTouchService.Stop();

        //this.IsVisibleIndicator = false;

        //if(espTouchResult.IsSucceed)		
        //	await App.Current.MainPage.DisplayAlert("Smart Config", "SmartBox is succesfully set up", "OK");		
        //else
        //          await App.Current.MainPage.DisplayAlert("Smart Config", "SmartBox is not set up", "OK");
    }
}

