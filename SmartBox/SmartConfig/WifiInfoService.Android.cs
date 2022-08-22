using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Telephony;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartBox.SmartConfig
{
    [BroadcastReceiver(Enabled = true)]
    public class WifiBroadcastReceiver : BroadcastReceiver
    {
        public event EventHandler<WifiDeviceInfo> WifiStateChanged;

        private WifiManager wifi;
        private WifiManager Wifi => wifi ??= 
                    (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

        public WifiBroadcastReceiver()
        {

        }
        public WifiBroadcastReceiver(EventHandler<WifiDeviceInfo> wifiStateChanged)
        {
            this.WifiStateChanged = wifiStateChanged;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.Action.Equals(WifiManager.NetworkStateChangedAction))               
                WifiStateChanged?.Invoke(this, Wifi.ConnectionInfo.ToWifiDeviceInfo());           
        }
    }


    public class WifiInfoService : IWifiInfoService
    {
        private ConnectivityManager connectivityManager = null;
        public ConnectivityManager ConnectivityManager => connectivityManager ??=
                 (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);

        
        private WifiCallback WifiCallback { get; set; }
        private WifiBroadcastReceiver WifiBroadcastReceiver { get; set; }

        public event EventHandler<WifiDeviceInfo> WifiStateChanged;
        public void DeactivateWifiService()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.S && WifiCallback != null)
                ConnectivityManager.UnregisterNetworkCallback(WifiCallback);
            else
            {
                if(WifiBroadcastReceiver != null)
                    Android.App.Application.Context.UnregisterReceiver(WifiBroadcastReceiver);                   
            }
        }
        public void ActivateWifiService()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            {
                NetworkRequest Request = new NetworkRequest.Builder()
                                   .AddTransportType(TransportType.Wifi)
                                   .AddCapability(NetCapability.NotMetered)
                                   .Build();

                WifiCallback = new WifiCallback(WifiStateChanged);            
                ConnectivityManager.RegisterNetworkCallback(Request, WifiCallback);
            }
            else
            {
                IntentFilter intentFilter = new IntentFilter();
                intentFilter.AddAction(WifiManager.NetworkStateChangedAction);
                WifiBroadcastReceiver = new WifiBroadcastReceiver(WifiStateChanged);    
                Android.App.Application.Context.RegisterReceiver(WifiBroadcastReceiver, intentFilter);               
            }
        }



        



    }

    public class WifiCallback : ConnectivityManager.NetworkCallback
    {        
        private WifiDeviceInfo WifiDeviceInfo { get; set; }

        private event EventHandler<WifiDeviceInfo> WifiStateChanged;
        public WifiCallback() : base((int)NetworkCallbackFlags.IncludeLocationInfo)
        {
        }
        public WifiCallback(EventHandler<WifiDeviceInfo> wifiStateChanged) : this()
        {
            WifiStateChanged = wifiStateChanged;
            WifiDeviceInfo = new WifiDeviceInfo();
        }

        public override void OnLost(Network network)
        {
            base.OnLost(network);
            WifiDeviceInfo = WifiDeviceInfo.Empty();
            WifiStateChanged?.Invoke(this, WifiDeviceInfo);            
        }

        public override void OnCapabilitiesChanged(Network network,
                                                   NetworkCapabilities networkCapabilities)
        {          
            base.OnCapabilitiesChanged(network, networkCapabilities);
            var wifiInfo = (WifiInfo)networkCapabilities.TransportInfo;

            if (WifiDeviceInfo.IsEqualBssid(wifiInfo.BSSID))
                return;

            WifiDeviceInfo = wifiInfo.ToWifiDeviceInfo();
            WifiStateChanged?.Invoke(this, WifiDeviceInfo);
        }
    }

    public static class WifiInfoExtension
    {
        public static WifiDeviceInfo ToWifiDeviceInfo(this WifiInfo wifi) =>    
            new WifiDeviceInfo(wifi.SSID.Substring(1, wifi.SSID.Length - 2), wifi.BSSID);           
        
    }

   
}
