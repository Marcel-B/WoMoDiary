using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;

namespace WoMoDiary.Android
{
    public class PlaceAdapter : BaseAdapter
    {
        IList<Place> Places;
        Context context;
        public PlaceAdapter(Context content, IList<Place> places)
        {
            Places = places;
            this.context = content;
        }

        public override int Count => Places.Count;

        public override Java.Lang.Object GetItem(int position)
        => position;

        public override long GetItemId(int position)
        => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            PlaceAdapterViewHolder holder = null;
            if (view != null) holder = view.Tag as PlaceAdapterViewHolder;

            if (holder == null)
            {
                holder = new PlaceAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService)
                                      .JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.tripAdapterCellLayout, parent, false);
                holder.PlaceName = view.FindViewById<TextView>(Resource.Id.textViewTripName);
                holder.PlaceDescription = view.FindViewById<TextView>(Resource.Id.textViewTripTimeSpan);
                view.Tag = holder;
            }

            holder.PlaceName.Text = Places[position].Name;
            holder.PlaceDescription.Text = Places[position].Description;
            return view;
        }
        internal class PlaceAdapterViewHolder : Java.Lang.Object
        {
            public TextView PlaceName { get; set; }
            public TextView PlaceDescription { get; set; }
        }
    }
}
