using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using WoMoDiary.ViewModels;
using Android.App;
using System.Security.Cryptography.X509Certificates;

namespace WoMoDiary
{
    public class TripAdapter : BaseAdapter
    {
        public ObservableCollection<Trip> Trips { get; set; }
        Context context;

        public TripAdapter(Context context, ObservableCollection<Trip> trips)
        {
            Trips = trips;
            this.context = context;
            Trips.CollectionChanged += Trips_CollectionChanged;
        }

        void Trips_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ((Activity)context).RunOnUiThread(() => NotifyDataSetChanged());
        }


        public override int Count => Trips.Count;

        public override Java.Lang.Object GetItem(int position)
            => position;

        public override long GetItemId(int position)
            => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            TripAdapterViewHolder holder = null;
            if (view != null) holder = view.Tag as TripAdapterViewHolder;

            if (holder == null)
            {
                holder = new TripAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService)
                                      .JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.tripAdapterCellLayout, parent, false);
                holder.TripName = view.FindViewById<TextView>(Resource.Id.textViewTripName);
                holder.TripTimespan = view.FindViewById<TextView>(Resource.Id.textViewTripTimeSpan);
                view.Tag = holder;
            }

            holder.TripName.Text = Trips[position].Name;
            holder.TripTimespan.Text = Trips[position].Created.ToString("D");
            return view;
        }

        internal class TripAdapterViewHolder : Java.Lang.Object
        {
            public TextView TripName { get; set; }
            public TextView TripTimespan { get; set; }
        }
    }
}
