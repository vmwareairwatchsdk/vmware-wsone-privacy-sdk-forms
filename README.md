# **vm**ware Workspace ONE Privacy for Xamarin.Forms

This documentation will cover the [installation](#installation), [setup](#setup) and [usage](#usage) of the privacy module for Workspace ONE in Xamarin Forms.

For details on Privacy SDK refer to [For iOS](https://code.vmware.com/docs/10005/sdk-privacy-module-for-ios--swift--developer-guide/GUID-A8CA007C-29FD-4A11-AD2A-9843A0032015.html?h=Privacy) and [For Android](https://docs.vmware.com/en/VMware-Workspace-ONE-UEM/services/SDK_Android_Privacy/GUID-14C5AF5B-2E54-4E49-ABBA-B59360BD77F9.html) 

## Installation

The SDK should be installed using **Nuget**.

- **WorkspaceOne.Privacy.Forms**: This is the package to be used in your Xamarin Forms app. It will provide interfaces for the initialization, setup and usage of the Workspace ONE Privacy module from your Xamarin Forms app.

Add this nuget package to your Xamarin.Forms project and to your iOS and Android project of the the Xamarin.Forms app as well.

Add the appropriate packages to your solution for each app project. Then continue to the [setup](#setup) step for [Android](#android) and [iOS](#ios).


## Setup

Before using the privacy module, just like many other Xamarin Forms packages it's dependencies need to be initialized first.

### Android

In the `MainActivity.cs`'s `OnCreate` (where most other packages get initialized as well) add the following code:

```
    protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Privacy.Android.Forms.Privacy.Instance.Init(global::Android.App.Application.Context, "Example");

            LoadApplication(new App());
        }

```


Replace *"Example"* with the name of the Android preferences file for you project.

### iOS

In the `AppDelegate.cs`'s `FinishedLaunching` (just where most other packages get initialized as well) add the following code:

```
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Privacy.iOS.Forms.Privacy.Instance.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
```
### Forms

There is no extra initialization step required in your Xamarin.Forms app and you can continue to [usage](#usage).

## Usage

In your Xamarin Forms app you can use the privacy module at any point in time you see fit during the run time of your app.

### Privacy Configuration

You can creatre your own privacy configuration with miss f pre-defined content and customs content.

```

    private AWPrivacyConfig GetConfig()
        {
            var privacyConfig = new AWPrivacyConfig
            {
                AllowFeatureAnalytics = true,
                CustomerPrivacyPolicyLink = "www.vmware.com"
            };

            privacyConfig.ApplicationName = "Sample Application";

            //Data Defaults START------------------------------------------------------------------------------------------------------------------------// 

            // Get App Default Data Setup.
            var appData = PrivacyController.Instance.AppDataDefaults;
           
            // Add New Privacy Content if Required
            appData.Add(new AWPrivacyContent("Browsing History", "Browsing History Text", PrivacyContentImageType.Camera));

            privacyConfig.DataCollectionItems = appData.ToArray();

            //Data Defaults END------------------------------------------------------------------------------------------------------------------------// 

            

            //App Permission START------------------------------------------------------------------------------------------------------------------------// 

            privacyConfig.AppPermissionTitle = "Sample App Permissions";
           
            var appPermissions = new List<AWPrivacyContent>
            {
                new AWPrivacyContent ("Camera", "Camera required for taking pictures",PrivacyContentImageType.Camera),
                new AWPrivacyContent ("Contact", "Contacts required for calling", PrivacyContentImageType.Contacts),
                new AWPrivacyContent ("Location", "Location required for GPS", PrivacyContentImageType.LocationServices),
                new AWPrivacyContent ("Network", "Required for networking", PrivacyContentImageType.Network),
                new AWPrivacyContent ("Storage", "Required to save files", PrivacyContentImageType.Storage),
                new AWPrivacyContent ("Hardware", "Required to access the microphone", PrivacyContentImageType.DeviceHardware),
            };

            privacyConfig.AppPermissionItems = appPermissions.ToArray();

            //App Permission END------------------------------------------------------------------------------------------------------------------------// 

            //Company Policy START------------------------------------------------------------------------------------------------------------------------//

            privacyConfig.EnterprisePolicyShow = true;
            privacyConfig.EnterprisePolicyTitle = "Company Policy";
            privacyConfig.EnterprisePolicyDescription = "Company Policy";
            privacyConfig.EnterprisePolicyLink = "https://www.vmware.com";
            //Company Policy END------------------------------------------------------------------------------------------------------------------------// 



           
            privacyConfig.DataSharingShow = true;


            //Data Sharing START------------------------------------------------------------------------------------------------------------------------//
            privacyConfig.DataSharingTitle = "Data Sharing Title";
            privacyConfig.DataSharingHeaderTitle = "Data Sharing Title";
            //Data Sharing  END------------------------------------------------------------------------------------------------------------------------// 

            privacyConfig.AllowExit = false;
           
            return privacyConfig;
        }

```

### Privacy Flow

To start the Privacy Flow your need to call the following code.

```
    PrivacyController.Instance.PreviewPrivacy(GetConfig(), (AWPrivacyResult result) =>
    {
        Debug.WriteLine(result);
    });
```


### Privacy Preview

To preview the configured Privacy content

```
    PrivacyController.Instance.StartPrivacyFlow(GetConfig(), (AWPrivacyResult result) =>
    {
        Debug.WriteLine(result);
    });
```

### Reset

You can clear the Privacy content using reset.

```
PrivacyController.Instance.Reset();
```
