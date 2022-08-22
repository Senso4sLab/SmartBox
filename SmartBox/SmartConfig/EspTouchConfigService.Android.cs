using Android.Media.Midi;
using Com.Espressif.Iot.Esptouch;
using Java.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartBox.SmartConfig
{
    public class EspTouchConfigService : IEspTouchConfigService
    {

        private bool SetBroadcast { get; set; }
        
        private EsptouchTask EspTouchTask = null;

        private event EventHandler<EspTouchEventArgs> espTouch;
        public event EventHandler<EspTouchEventArgs> EspTouch
        {
            add
            {
                if (espTouch == null)
                    espTouch += value;
            }
            remove
            {
                espTouch -= value;
            }
        }

        public EspTouchConfigService()
        {            
            this.SetBroadcast = true;
        }

        public void SetPackageBroadcast(bool packageBrodcast) =>
                 SetBroadcast = packageBrodcast;


        public Task<IEnumerable<EspTouchResult>> ExecuteForResultsAsync(WifiDeviceInfo wifiInfo, int expectedEspTouchResult) =>        
            Task.Run(() =>
            {
                EspTouchTask = new EsptouchTask(wifiInfo.Ssid.GetBytes(),
                                            wifiInfo.Bssid.GetBytesFromHex(),
                                            wifiInfo.Password.GetBytes(),
                                            Android.App.Application.Context);

                EspTouchTask.SetPackageBroadcast(SetBroadcast);
                EspTouchTask.Esptouch += Task_Esptouch;
                return EspTouchTask?.ExecuteForResults(expectedEspTouchResult)?.ToEspTouchResults();
            });             

        public void Stop()
        {
            EspTouchTask.Esptouch -= Task_Esptouch;
            EspTouchTask?.Interrupt();
        }

        private void Task_Esptouch(object sender, EsptouchEventArgs e) =>
            RaiseEspTouchEventHandler(e.P0.ToEspTouchResult());

        private void RaiseEspTouchEventHandler(EspTouchResult result) =>
             espTouch?.Invoke(this, new EspTouchEventArgs(result));
               
    }

    public static class StringExtension
    {
        public static byte[] GetBytes(this string str) =>
           Encoding.ASCII.GetBytes(str);

    }
    public static class EnumerableExtension
    {
        public static byte[] GetBytesFromHex(this string str) =>
           str.Split(':')
              .Select(item => Convert.ToByte(item, 16))
              .ToArray();

    }
    public static class IEsptouchResultExtension
    {
        public static IEnumerable<EspTouchResult> ToEspTouchResults(this IEnumerable<IEsptouchResult> results) =>
            results.Select(espResult => espResult.ToEspTouchResult());


        public static EspTouchResult ToEspTouchResult(this IEsptouchResult result) =>
            new EspTouchResult(result.Bssid, result.IsCancelled, result.IsSuc);
    }
}
