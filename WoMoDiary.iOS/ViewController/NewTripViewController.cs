using System;
using I18NPortable;
using UIKit;
using WoMoDiary.Meta;
using WoMoDiary.ViewModels;

namespace WoMoDiary.iOS
{
    public partial class NewTripViewController : UIViewController
    {
        private NewTripViewModel viewModel;
        public NewTripViewController(IntPtr handle) : base(handle)
        {
            viewModel = new NewTripViewModel();
        }

        private void Localize()
        {
            Title = Strings.NEW_TRIP.Translate();
            TextFieldTripName.Placeholder = Strings.ENTER_TRIP_NAME;
            TextFieldDescription.Placeholder = Strings.ENTER_DESCRIPTION;
            ButtonSaveTrip.SetTitle(Strings.SAVE, UIControlState.Normal);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Localize();
            ButtonSaveTrip.TouchUpInside += (sender, e) =>
            {
                viewModel.SaveTripCommand.Execute(null);
            };
            //TextFieldTripName.EditingDidEnd += (sender, e) =>
            //{

            //};
            TextFieldTripName.EditingChanged += (sender, e) =>
            {
                viewModel.TripName = TextFieldTripName.Text;
                ButtonSaveTrip.Enabled = viewModel.SaveTripCommand.CanExecute(null);
            };
            TextFieldDescription.EditingChanged += (sender, e) =>
            {
                viewModel.Description = ((UITextField)sender).Text;
                ButtonSaveTrip.Enabled = viewModel.SaveTripCommand.CanExecute(null);
            };
        }
    }
}