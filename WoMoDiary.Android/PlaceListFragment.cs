using Android.OS;
using Android.Views;
using Android.Widget;
using ListFragment = Android.Support.V4.App.ListFragment;
using Toast = Android.Widget.Toast;
using ToastLength = Android.Widget.ToastLength;

using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Services;
using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Meta;
using System.Threading.Tasks;

namespace com.b_velop.WoMoDiary.Android
{
    public class PlaceListFragment : ListFragment
    {
        public PlacesViewModel ViewModel { get; set; }

        public PlaceListFragment()
        {
            ViewModel = ServiceLocator.Instance.Get<PlacesViewModel>();
            ViewModel.ErrorAction = ToastMessage;
        }
        private void ToastMessage(string mssg)
            => Toast.MakeText(Activity, mssg, ToastLength.Long).Show();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var store = AppStore.Instance;
            var trip = store.CurrentTrip;
            ViewModel.PullPlaces();
            ListAdapter = new PlaceAdapter(Activity, ViewModel.Places);
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            RegisterForContextMenu(ListView);
        }
        public override void OnResume()
        {
            base.OnResume();
            ViewModel.PullPlaces();
            ListAdapter = new PlaceAdapter(Activity, ViewModel.Places);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            if (v == ListView)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle(ViewModel.Places[info.Position].Name);
                menu.Add(Menu.None, 0, 0, Strings.EDIT);
                menu.Add(Menu.None, 1, 1, Strings.DELETE);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var menuItemIndex = item.ItemId;
            AppStore.Instance.CurrentPlace = ViewModel.Places[info.Position];
            if (menuItemIndex == 0)
            {
                Activity.StartActivity(typeof(EditPlaceActivity));
            }
            else
            {
                Task.Run(() => ViewModel.DeletePlace(info.Position));
            }
            //Toast.MakeText(Activity, $"Selected {menuItemIndex}", ToastLength.Short).Show();
            return true;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var localStore = AppStore.Instance;
            localStore.CurrentPlace = ViewModel.Places[position];
            Activity.StartActivity(typeof(PlaceDetailActivity));
        }
    }
}
