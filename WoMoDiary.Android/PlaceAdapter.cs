using System;
using System.Collections.Generic;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WoMoDiary.Domain;
using System.Collections.ObjectModel;

namespace WoMoDiary.Android
{
    public class PlaceAdapter : BaseAdapter
    {
        ObservableCollection<Place> Places;
        Context context;
        public PlaceAdapter(Context content, ObservableCollection<Place> places)
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
                view = inflater.Inflate(Resource.Layout.placeAdapterCellLayout, parent, false);
                holder.PlaceName = view.FindViewById<TextView>(Resource.Id.textViewPlaceNameCell);
                holder.PlaceDescription = view.FindViewById<TextView>(Resource.Id.textViewPlaceDescriptionCell);
                holder.TypeName = view.FindViewById<ImageView>(Resource.Id.imageViewPlaceType);
                holder.RatingName = view.FindViewById<ImageView>(Resource.Id.imageViewPlaceRating);
                view.Tag = holder;
            }

            var val = Places[position].Rating > 2 ? Resource.Drawable.thumb_up : Resource.Drawable.thumb_down;
            var v = 0;
            switch (Places[position].Type)
            {
                case PlaceType.Hotel:
                    v = Resource.Drawable.Hotel;
                    break;
                case PlaceType.CampingPlace:
                    v = Resource.Drawable.Camping;
                    break;
                case PlaceType.MotorhomePlace:
                    v = Resource.Drawable.Camping;
                    break;
                case PlaceType.Restaurant:
                    v = Resource.Drawable.Restaurant;
                    break;
                case PlaceType.SightSeeing:
                    v = Resource.Drawable.SightSeeing;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            holder.PlaceName.Text = Places[position].Name;
            holder.PlaceDescription.Text = Places[position].Description;
            holder.RatingName.SetImageResource(val);
            holder.TypeName.SetImageResource(v);

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
