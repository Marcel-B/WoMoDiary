using System;
using UIKit;
using WoMoDiary.iOS.ViewModels;

namespace WoMoDiary.iOS
{
    public partial class NewTripViewController : UIViewController
    {
        private NewTripViewModel viewModel;
        public NewTripViewController(IntPtr handle) : base(handle)
        {
            viewModel = new NewTripViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
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