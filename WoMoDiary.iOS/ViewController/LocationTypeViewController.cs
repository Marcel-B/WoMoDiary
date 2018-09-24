using System;
using UIKit;
using System.Collections.Generic;
using WoMoDiary.Models;
using WoMoDiary.ViewModels;
using WoMoDiary.Services;

namespace WoMoDiary.iOS
{
    public partial class LocationTypeViewController : UIViewController
    {
        SaveLocationTypeViewModel viewModel;
        public LocationTypeViewController(IntPtr handle) : base(handle)
        {
            viewModel = new SaveLocationTypeViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PickerViewLocationType.Model = new LocationTypePickerViewModel(PickerChangedEvent);
            ButtonSave.TouchUpInside += (sender, e) =>
            {
                if (sender is UIButton button)
                {
                    viewModel.SaveLocationTypeCommand.Execute(null);
                }
            };

            //TextFieldDescription.EditingChanged += (sender, e) =>
            //{
            //    viewModel.Place.Description = ((UITextField)sender).Text;
            //};

            ButtonThumbUp.TouchUpInside += (object sender, EventArgs e) =>
            {
                var color = ((UIButton)sender).BackgroundColor;
                var tint = ((UIButton)sender).TintColor;

                ButtonThumbDown.BackgroundColor = color;
                ButtonThumbDown.TintColor = tint;

                ButtonThumbUp.BackgroundColor = UIColor.Green;
                ButtonThumbUp.TintColor = UIColor.Black;

                viewModel.Place.IsGood = true;
            };
            ButtonThumbDown.TouchUpInside += (sender, e) =>
            {
                var color = ((UIButton)sender).BackgroundColor;
                var tint = ((UIButton)sender).TintColor;

                ButtonThumbUp.BackgroundColor = color;
                ButtonThumbUp.TintColor = tint;

                ButtonThumbDown.BackgroundColor = UIColor.Green;
                ButtonThumbDown.TintColor = UIColor.Black;

                viewModel.Place.IsGood = false;
            };

            PickerViewLocationType.Model.Selected(PickerViewLocationType, 0, 0);
        }

        private void PickerChangedEvent(object sender, PickerChangedEventArgs args)
        {
            viewModel.Place = args.Place;
        }

        private class LocationTypePickerViewModel : UIPickerViewModel
        {
            public IPlace SelectedType { get; set; }

            private readonly IList<IPlace> _locationTypes;
            private Action<object, PickerChangedEventArgs> pickerChanged;
            public LocationTypePickerViewModel(Action<object, PickerChangedEventArgs> pickerChanged)
            {
                _locationTypes = new List<IPlace>
                {
                    new CampingPlace(),
                    new Hotel(),
                    new Restaurant(),
                    new NicePlace(),
                };// LocationTypes().Locations;
                this.pickerChanged = pickerChanged;
            }

            public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
                => _locationTypes.Count;

            public override string GetTitle(UIPickerView pickerView, nint row, nint component)
                => _locationTypes[(int)row].Name;

            public override nint GetComponentCount(UIPickerView pickerView)
                => 1;

            public override void Selected(UIPickerView pickerView, nint row, nint component)
            {
                var SelectedPlace = _locationTypes[(int)row];
                pickerChanged?.Invoke(this, new PickerChangedEventArgs { Place = SelectedPlace });
            }
        }

        private class PickerChangedEventArgs : EventArgs
        {
            public IPlace Place { get; set; }
        }
    }
}