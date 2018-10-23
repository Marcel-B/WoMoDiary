using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Network;

namespace com.b_velop.WoMoDiary.iOS
{
    public partial class NewPlaceLocationTypeViewController : UIViewController
    {

        public NewPlaceLocationTypeViewController(IntPtr handle) : base(handle)
        {
            // Set by MapViewController - Singleton are not available in ServiceLocator ...
            //ViewModel = ServiceLocator.Instance.Get<NewPlaceViewModel>();
        }

        public NewPlaceViewModel ViewModel { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PickerViewLocationType.Model = new LocationTypePickerViewModel(PickerChangedEvent);

            Localize();
            SetStates();
            SetControllEvents();
        }

        private void PickerChangedEvent(object sender, PickerChangedEventArgs args)
        {
            ViewModel.Type = args.Place.Type;
        }

        private void SetStates()
        {
            PickerViewLocationType.Model.Selected(PickerViewLocationType, 0, 0);
        }

        private void SetControllEvents()
        {
            ButtonSave.TouchUpInside += (sender, e) =>
            {
                if (sender is UIButton button)
                {
                    ViewModel.SavePlaceCommand.Execute(null);
                    var controllers = NavigationController.ViewControllers;
                    NavigationController.SetViewControllers(controllers.SkipLast(2).ToArray(), true);
                }
            };

            ButtonThumbUp.TouchUpInside += (object sender, EventArgs e) =>
            {
                var color = ((UIButton)sender).BackgroundColor;
                var tint = ((UIButton)sender).TintColor;

                ButtonThumbDown.BackgroundColor = color;
                ButtonThumbDown.TintColor = tint;

                ButtonThumbUp.BackgroundColor = UIColor.Green;
                ButtonThumbUp.TintColor = UIColor.Black;

                ViewModel.Rating = 5;
            };

            ButtonThumbDown.TouchUpInside += (sender, e) =>
            {
                var color = ((UIButton)sender).BackgroundColor;
                var tint = ((UIButton)sender).TintColor;

                ButtonThumbUp.BackgroundColor = color;
                ButtonThumbUp.TintColor = tint;

                ButtonThumbDown.BackgroundColor = UIColor.Green;
                ButtonThumbDown.TintColor = UIColor.Black;

                ViewModel.Rating = 0;
            };
        }

        private void Localize()
        {
            Title = Strings.CATEGORY;
            LabelSelectPlaceCategory.Text = Strings.SELECT_PLACE_CATEGORY;
            ButtonSave.SetTitle(Strings.SAVE, UIControlState.Normal);
        }


    }


    public class LocationTypePickerViewModel : UIPickerViewModel
    {
        public Place SelectedType { get; set; }

        private readonly IList<Place> _locationTypes;
        private Action<object, PickerChangedEventArgs> pickerChanged;
        public LocationTypePickerViewModel(Action<object, PickerChangedEventArgs> pickerChanged)
        {
            /*
        MotorhomePlace,
        CampingPlace,
        Restaurant,
        Poi,
        Hotel
                */
            _locationTypes = new List<Place>
                {
                    new MotorhomePlace(),
                    new CampingPlace(),
                    new Restaurant(),
                    new Poi(),
                    new Hotel()
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

    public class PickerChangedEventArgs : EventArgs
    {
        public Place Place { get; set; }
    }
}