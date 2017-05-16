﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Hardware;
using System.Threading;
using Flurl;
using Flurl.Http;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace PruSign.Droid
{
    [Activity(Label = "PruSign.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static readonly object _syncLock = new object();
        private SensorManager sensorManager;
        private Sensor sensor;
        private ShakeDetector shakeDetector;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            sensorManager = (SensorManager)GetSystemService(Context.SensorService);
            sensor = sensorManager.GetDefaultSensor(SensorType.Accelerometer);

            shakeDetector = new ShakeDetector();
            shakeDetector.Shaked += (sender, shakeCount) =>
                {
                    lock (_syncLock)
                    {
                        // Accion a realizar en el caso de que se detecte que el dispositivo ha sido agitado
                    }
                };


            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }



        protected override void OnStop()
        {
            base.OnStop();

            ThreadPool.QueueUserWorkItem(o => sendRestSignature());

        }

        private void sendRestSignature()
        {
            try
            {
                FileHelper fh = new FileHelper();
                SignatureDatabase db = new SignatureDatabase(fh.GetLocalFilePath("PruSign.db"));
                System.Threading.Tasks.Task<System.Collections.Generic.List<SignatureItem>> items = db.GetItemsNotDoneAsync();
                foreach (var item in items.Result)
                {
                    try
                    {
                        Post("https://127.0.0.1:8080/api/SignatureApi", item.SignatureObject);
                        item.Sent = true;
                        item.SentTimeStamp = System.DateTime.Now.Ticks;
                        db.SaveItemAsync(item);
                    }
                    catch (Exception ex)
                    {
                        // POR AHORA HAGAMOS ESTO EN CASO DE ERROR
                        item.Miscelanea = ex.Message;
                        db.SaveItemAsync(item);
                    }
                }
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
        }


        protected override void OnResume()
        {
            base.OnResume();

            sensorManager.RegisterListener(shakeDetector, sensor, SensorDelay.Ui);
        }

        protected override void OnPause()
        {
            base.OnPause();

            sensorManager.UnregisterListener(shakeDetector);
        }

        public static async void Post(string url, string jsonString)
        {
            var response = await url.PostJsonAsync(jsonString);
        }


    }
}
