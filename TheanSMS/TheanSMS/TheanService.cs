using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Util;
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
            _TimerData = new Timer((o)=> { sent(); },null,0, 30000);
        }

        public void sent()
        {
            SmsManager.Default.SendTextMessage("9539536943", null, "hai", null, null);
           // Toast.MakeText(this, "**** Started *****", ToastLength.Long).Show();
        }

        public override void OnDestroy()
        {
            Toast.MakeText(this, "Stopped !!!", ToastLength.Long).Show();

        }
        public override bool StopService(Intent name)
        {
            return base.StopService(name);  
        }
    }
}