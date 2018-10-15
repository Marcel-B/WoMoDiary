using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using System.Collections.ObjectModel;
using Android.App;

namespace WoMoDiary
{
    public class PlaceAdapter : BaseAdapter
    {
        ObservableCollection<Place> Places;
        Context context;
        public PlaceAdapter(Context content, ObservableCollection<Place> places)
        {
            Places = places;
            this.context = content;
            Places.CollectionChanged -= Places_CollectionChanged;
            Places.CollectionChanged += Places_CollectionChanged;
        }

        void Places_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ((Activity)context).RunOnUiThread(() => NotifyDataSetChanged());
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
                view = inflater.Inflate(Resource.Layout.placeAdapterCellLayout, parent, false);
                holder.PlaceName = view.FindViewById<TextView>(Resource.Id.textViewPlaceNameCell);
                holder.PlaceDescription = view.FindViewById<TextView>(Resource.Id.textViewPlaceDescriptionCell);
                holder.TypeName = view.FindViewById<ImageView>(Resource.Id.imageViewPlaceType);
                holder.RatingName = view.FindViewById<ImageView>(Resource.Id.imageViewPlaceRating);
                view.Tag = holder;
            }
            var currentPlace = Places[position];
            holder.PlaceName.Text = currentPlace.Name;
            holder.PlaceDescription.Text = currentPlace.Description;
            holder.RatingName.SetImageResource(currentPlace.ToRating());
            holder.TypeName.SetImageResource(currentPlace.ToCategory());

            return view;
        }
        internal class PlaceAdapterViewHolder : Java.Lang.Object
        {
            public TextView PlaceName { get; set; }
            public TextView PlaceDescription { get; set; }
            public ImageView TypeName { get; set; }
            public ImageView RatingName { get; set; }
        }
    }
}
