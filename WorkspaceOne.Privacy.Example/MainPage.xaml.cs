using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspaceOne.Privacy.Forms;
using Xamarin.Forms;
using PrivacyController = WorkspaceOne.Privacy.Forms.Privacy;

namespace WorkspaceOne.Privacy.Example
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            previewButton.Clicked += HandlePreviewButtonClicked;
            privacyButton.Clicked += HandlePrivacyButtonClicked;
            resetButton.Clicked += HandleResetButtonClicked;
        }

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

        private void HandlePreviewButtonClicked(object sender, EventArgs e)
        {
            PrivacyController.Instance.PreviewPrivacy(GetConfig(), (AWPrivacyResult result) =>
            {
                Debug.WriteLine(result);
            });
        }

        private void HandlePrivacyButtonClicked(object sender, EventArgs e)
        {
            PrivacyController.Instance.StartPrivacyFlow(GetConfig(), (AWPrivacyResult result) =>
            {
                Debug.WriteLine(result);
            });
        }

        private void HandleResetButtonClicked(object sender, EventArgs e)
        {
            PrivacyController.Instance.Reset();
        }
    }
}
