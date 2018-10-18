using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;

using System;
using UIKit;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class NewTripViewController : UIViewController
    {
        public NewTripViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<NewTripViewModel>();
        }

        public NewTripViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Localize();
            SetControllEvents();
        }

        private void Localize()
        {
            Title = Strings.NEW_TRIP;
            TextFieldTripName.Placeholder = Strings.ENTER_TRIP_NAME;
            TextFieldDescription.Placeholder = Strings.ENTER_DESCRIPTION;
            ButtonSaveTrip.SetTitle(Strings.SAVE, UIControlState.Normal);
        }

        private void SetControllEvents()
        {
            ButtonSaveTrip.TouchUpInside += (sender, e) =>
            {
                ViewModel.SaveTripCommand.Execute(null);
            };
            TextFieldTripName.EditingChanged += (sender, e) =>
            {
                ViewModel.TripName = TextFieldTripName.Text;
                ButtonSaveTrip.Enabled = ViewModel.SaveTripCommand.CanExecute(null);
            };
            TextFieldDescription.EditingChanged += (sender, e) =>
            {
                ViewModel.Description = ((UITextField)sender).Text;
                ButtonSaveTrip.Enabled = ViewModel.SaveTripCommand.CanExecute(null);
            };
        }
    }
}