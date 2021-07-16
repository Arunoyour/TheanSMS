using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Telephony;
using Android.Widget;
using Android.Views;
using Android.Content;

namespace TheanSMS
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",Icon ="@drawable/Modified", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private static bool flag = false;
        Button StartBtn, Stopbtn;
        TextView txt;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            StartService(new Android.Content.Intent(this, typeof(TheanService)));
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            StartBtn =FindViewById<Button>(Resource.Id.startSelectingText);
            StartBtn.Click += delegate { StartBtnClick(); };
            Stopbtn = FindViewById<Button>(Resource.Id.stopSelectingText);
            Stopbtn.Click += delegate { StopBtnClick(); };
            txt = FindViewById<TextView>(Resource.Id.txt);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void StartBtnClick()
        {
            Intent intent = new Intent(this, typeof(TheanService));
            this.StartService(intent);
        }
        public void StopBtnClick()
        {
            Intent intent = new Intent(this, typeof(TheanService));
            this.StopService(intent);
        }
        public void SentSMS()
        {
            while(!flag)
            {
                //txt.Text = System.DateTime.Today.ToLongTimeString();
                //SmsManager.Default.SendTextMessage("9539536943", null, "hai", null, null);
                //System.Threading.Thread.Sleep(30000);
            }
        }
    }
}