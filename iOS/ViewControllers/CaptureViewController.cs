using System;
using UIKit;
using Xamarin.Essentials;
using WoMoDiary.iOS.HelpersIOS;
using Foundation;
using AssetsLibrary;
using I18NPortable;

namespace WoMoDiary.iOS
{
    public partial class CaptureViewController : UIViewController
    {
        public UIImageView MyImageView { get => PhotoImage; set => PhotoImage = value; }

        public AboutViewModel ViewModel { get; set; }
        public CaptureViewController(IntPtr handle) : base(handle)
        {
            ViewModel = new AboutViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = ViewModel.Title;

            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
            {
                CameraButton.SetTitle("No Camera".Translate(), UIControlState.Disabled);
                CameraButton.SetTitleColor(UIColor.Gray, UIControlState.Disabled);
                CameraButton.Enabled = false;
            }
            else
            {
                CameraButton.SetTitle("Shoot".Translate(), UIControlState.Disabled);
                CameraButton.SetTitleColor(UIColor.Blue, UIControlState.Normal);
                CameraButton.Enabled = true;
            }

        }

        //partial void ReadMoreButton_TouchUpInside(UIButton sender) => ViewModel.OpenWebCommand.Execute(null);
        partial void ReadMoreButton_TouchUpInside(UIButton sender)
        {
            Camera.TakePicture(this, (Foundation.NSDictionary obj) =>
            {
                // https://developer.apple.com/library/ios/#documentation/uikit/reference/UIImagePickerControllerDelegate_Protocol/UIImagePickerControllerDelegate/UIImagePickerControllerDelegate.html#//apple_ref/occ/intfm/UIImagePickerControllerDelegate/imagePickerController:didFinishPickingMediaWithInfo:
                var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
                var meta = obj.ValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;
                MyImageView.Image = photo;
                // This bit of code saves to the Photo Album with metadata
                var library = new ALAssetsLibrary();
                library.WriteImageToSavedPhotosAlbum(photo.CGImage, meta, (assetUrl, error) =>
                {
                    Console.WriteLine("assetUrl:" + assetUrl);
                });
            });
        }
    }
}
