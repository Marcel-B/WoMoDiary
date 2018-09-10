using System;
using Foundation;
using UIKit;

namespace WoMoDiary.iOS.HelpersIOS
{
    public static class Camera
    {
        static UIImagePickerController picker;
        static Action<NSDictionary> _callback;

        static void Init()
        {
            if (picker != null)
                return;

            picker = new UIImagePickerController
            {
                Delegate = new CameraDelegate()
            };
        }

        class CameraDelegate : UIImagePickerControllerDelegate
        {
            public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
            {
                var cb = _callback;
                _callback = null;
                picker.DismissViewController(true, () =>
                {
                    
                });
                cb(info);
            }
        }

        public static void TakePicture(UIViewController parent, Action<NSDictionary> callback)
        {
            Init();
            picker.SourceType = UIImagePickerControllerSourceType.Camera;
            _callback = callback;
            parent.PresentViewController(picker, true, null);
        }

        public static void SelectPicture(UIViewController parent, Action<NSDictionary> callback)
        {
            Init();
            picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            _callback = callback;
            parent.PresentViewController(picker, true, () =>
            {
                var current = parent as CaptureViewController;
            });
        }
    }
}
