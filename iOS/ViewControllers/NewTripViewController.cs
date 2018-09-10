using System;

using UIKit;
using WoMoDiary.Helpers;
using I18NPortable;

namespace WoMoDiary.iOS
{
    public partial class NewTripViewController : UIViewController
    {
        public ItemsViewModel ViewModel { get; set; }

        public NewTripViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupLanguage();
            ButtonSave.TouchUpInside += (sender, e) =>
            {
                var item = new Item
                {
                    Text = txtTitle.Text,
                    //Description = txtDesc.Text
                    Description = DateTime.Now.ToString()
                };
                ViewModel.AddItemCommand.Execute(item);
                NavigationController.PopToRootViewController(true);
            };
        }

        protected void SetupLanguage()
        {
            LabelTripName.Text = Strings.TRIP_NAME.Translate();
            LabelDescription.Text = Strings.DESCRIPTION.Translate();
            Title = Strings.NEW_TRIP.Translate();
            ButtonSave.SetTitle(Strings.SAVE.Translate(), UIControlState.Normal);
        }
    }
}
