using System.Collections.Generic;

namespace Movie_InterfaceAPI
{
    public class OMDB
    {
        public static readonly string api = "713b8d09";
        public static readonly string multipleAddress = "http://www.omdbapi.com/?s=";
        public static readonly string singleAddress = "http://www.omdbapi.com/?t=";
        public static readonly string imdbAddress = "http://www.omdbapi.com/?i=";
    }
    public class ImdbEntity_M
    {
        public List<ImdbEntity_M> Search { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }
    }
    public class ImdbEntity_S
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Response { get; set; }

        public void Data()
        {
            //Properties.Settings.Default.WishList.Clear();
            //Properties.Settings.Default.SeenList.Clear();
        }
        public void AddToWishList(string Title)
        {
            if (!Properties.Settings.Default.WishList.Contains(Title))
            {
                Properties.Settings.Default.WishList.Add(Title);
                Properties.Settings.Default.Save();
            }
        }
        public void RemoveFromWishList(string Title)
        {
            if (Properties.Settings.Default.WishList.Contains(Title))
            {
                Properties.Settings.Default.WishList.Remove(Title);
                Properties.Settings.Default.Save();
            }
        }
        public void AddToSeenList(string Title)
        {
            if (!Properties.Settings.Default.SeenList.Contains(Title))
            {
                Properties.Settings.Default.SeenList.Add(Title);
                Properties.Settings.Default.Save();
            }
        }
        public void RemoveFromSeenList(string Title)
        {
            if (Properties.Settings.Default.SeenList.Contains(Title))
            {
                Properties.Settings.Default.SeenList.Remove(Title);
                Properties.Settings.Default.Save();
            }
        }
    }
}
