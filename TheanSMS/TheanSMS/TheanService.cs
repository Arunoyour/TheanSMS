using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace TheanSMS
{
    [Service]
    class TheanService : Service
    {
        private Timer _TimerData;
        public override IBinder OnBind(Intent intent)
        {

            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Debug("Thean", "Stating....");
            SentSMS();
            return StartCommandResult.NotSticky;
            /*
            Toast.MakeText(this, "**** Started *****", ToastLength.Long).Show();
            bool flag = false;
            while (!flag)
            {
                SmsManager.Default.SendTextMessage("9539536943", null, "hai", null, null);
                System.Threading.Thread.Sleep(30000);
            }
            return StartCommandResult.NotSticky;
            */
        }

        public void SentSMS()
        {
            _TimerData = new Timer((o) => { sent(); }, null, 0, 60000);
        }

        public void sent()
        {
            var txt = Convert.ToString(RefreshDataAsync().Result);
            foreach (var item in RefreshDataAsync().Result.Data)
            {
                SmsManager.Default.SendTextMessage(item.PhoneNo, null, item.Message, null, null);
                UpdatingAck(item.PhoneNo);
            }

            // Toast.MakeText(this, "**** Started *****", ToastLength.Long).Show();
        }

        private void UpdatingAck(string phoneNo)
        {
            var WebAPIUrl = "https://www.thean.in/api/User/SMSAck"; // Set your REST API URL here.
            var uri = new Uri(WebAPIUrl);
            var client = new HttpClient();
            try
            {
                StringContent content = new StringContent("{\"MobileNumber\": \"" + phoneNo + "\"}", Encoding.UTF8, "application/json");
                var response = client.PostAsync(uri, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    JsonConvert.DeserializeObject<GetallSMSDto>(contents);
                }
            }
            catch (Exception ex)
            { }
        }

        public override void OnDestroy()
        {
            Toast.MakeText(this, "Stopped !!!", ToastLength.Long).Show();

        }
        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }

        public async System.Threading.Tasks.Task<GetallSMSDto> RefreshDataAsync()
        {
            var WebAPIUrl = "https://www.thean.in/api/User/GetAllSMS"; // Set your REST API URL here.
            var uri = new Uri(WebAPIUrl);
            var client = new HttpClient();
            try
            {
                StringContent content = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<GetallSMSDto>(contents);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}