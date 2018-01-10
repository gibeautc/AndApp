using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Globals;

namespace dustoff
{
    [Activity(Label = "Home", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            SeekBar ot = FindViewById<SeekBar>(Resource.Id.officeTempSeek);
            TextView showTemp = FindViewById<TextView>(Resource.Id.officeTempSet);
            TextView logText = FindViewById<TextView>(Resource.Id.log);
            Button updateButton = FindViewById<Button>(Resource.Id.update);
            Switch heatSwitch=FindViewById<Switch>(Resource.Id.switch1);
            showTemp.Text = ot.Progress.ToString();
            float set=55;
            ot.ProgressChanged += (sender, e) =>
            {
                set = (float)ot.Progress / 100;
                set = set * 20;
                set = set + 55;
                showTemp.Text = string.Format("Set to: {0}", set);
                Globals.Vars.setpoint = set;
                
            };
            heatSwitch.CheckedChange += (sender, e) =>
              {
                  if (e.IsChecked)
                  {
                      Globals.Vars.HeatBool = true;
                  }
                  else
                  {
                      Globals.Vars.HeatBool = false;
                  }
              };
            updateButton.Click += (sender, e) =>
             {
                 Globals.Vars.setpoint = set;
              
                 logText.Text = "";
                 int res = Core.WebService.Push(set:set);
                 if(res == 4)
                 {
                     logText.Text = "Web Exception";
                 }
             };

        }
    }
}

