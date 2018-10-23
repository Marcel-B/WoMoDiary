using System;
using System.Linq;
using UIKit;

using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class EditPlaceViewController : UIViewController
    {
        public EditPlaceViewController(IntPtr handle) : base(handle)
        {
            ViewModel = ServiceLocator.Instance.Get<EditPlaceViewModel>();
            //ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            ViewModel.ErrorAction = ErrorMessage;
            ViewModel.UpdateReady = UpdateReady;
        }

        private void UpdateReady(bool success)
        {
            if (!success) return;
            var controllers = NavigationController.ViewControllers;
            NavigationController.SetViewControllers(controllers.SkipLast(1).ToArray(), true);
        }

        private void ErrorMessage(string mssg)
        {

        }

        public EditPlaceViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.FetchPlace();
            Localize();
            InitController();
            SetControllEvents();
        }
        private void Localize()
        {
            ButtonSave.SetTitle(Strings.SAVE, UIControlState.Normal);
        }
        private void PickerChangedEvent(object sender, PickerChangedEventArgs args)
        {
            ViewModel.SelectedPlaceType = args.Place.Type;
        }
        private void InitController()
        {
            TextFieldName.Text = ViewModel.Name;
            TextFieldDescription.Text = ViewModel.Description;
            TextFieldRating.Text = ViewModel.Rating;
            PickerViewTypes.Model = new LocationTypePickerViewModel(PickerChangedEvent);
            PickerViewTypes.Select((int)ViewModel.SelectedPlaceType, 0, true);
            PickerViewTypes.UserInteractionEnabled = false;
        }
        private void SetControllEvents()
        {
            ButtonSave.TouchUpInside += (sender, e) =>
            {
                ViewModel.SavePlaceCommand.Execute(null);
            };
            TextFieldName.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textField)
                    ViewModel.Name = textField.Text;
            };
            TextFieldDescription.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textField)
                    ViewModel.Description = textField.Text;
            };
            TextFieldRating.AllEditingEvents += (sender, e) =>
            {
                if (sender is UITextField textField)
                    ViewModel.Rating = textField.Text;
            };
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                    TextFieldName.Text = ViewModel.Name;
                    break;
                case "Description":
                    TextFieldDescription.Text = ViewModel.Description;
                    break;
                case "Rating":
                    TextFieldRating.Text = ViewModel.Rating;
                    break;
                case "SelectedPlaceType":
                    PickerViewTypes.Select((int)ViewModel.SelectedPlaceType, 0, true);
                    break;
                default:
                    return;
            }
        }
    }
}