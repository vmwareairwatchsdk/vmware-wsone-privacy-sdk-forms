﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using WorkspaceOne.Privacy.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkspaceOne.Privacy.Example
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
