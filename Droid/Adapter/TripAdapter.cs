using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using WoMoDiary.Models;

namespace WoMoDiary.Droid.Adapter
{
    public class TripAdapter : BaseAdapter
    {
        Context _context;
        IList<Trip> _trips;

        public TripAdapter(Context context, IList<Trip> trips)
        {
            _trips = trips;
            _context = context;
        }

        public override int Count => _trips.Count;

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
                var inflater = _context
                    .GetSystemService(Context.LayoutInflaterService)
                    .JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.TripCellLayout, parent, false);
                holder.Name = view.FindViewById<TextView>(Resource.Id.textViewCellTripName);
                view.Tag = holder;
            }
            holder.Name.Text = _trips[position].Name;
            return view;
        }

        class TripAdapterViewHolder : Java.Lang.Object
        {
            public TextView Name { get; set; }
        }
    }
}
