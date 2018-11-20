using System;
using System.Net;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace Movie_InterfaceAPI
{
    public partial class Main : Form
    {
        public ImdbEntity_S Entity_S = new ImdbEntity_S();
        public ImdbEntity_M Entity_M = new ImdbEntity_M();
        public SeenListUC SeenListUserControl;
        public WishListUC WishListUserControl;

        public Main()
        {
            if (Properties.Settings.Default.WishList == null)
                Properties.Settings.Default.WishList = new List<string>();
            if (Properties.Settings.Default.SeenList == null)
                Properties.Settings.Default.SeenList = new List<string>();

            Properties.Settings.Default.Save();

            this.InitializeComponent();
        }
        private void SearchTb_Enter(object sender, EventArgs e)
        {
            if (SearchTb.Text == "Search for movie title...")
                SearchTb.Text = string.Empty;
        }
        private void SearchTb_Leave(object sender, EventArgs e)
        {
            if (SearchTb.Text == string.Empty)
                SearchTb.Text = "Search for movie title...";
        }
        public bool TitleInWishList(string imdbId)
        {
            if (Properties.Settings.Default.WishList.Contains(imdbId))
                return true;
            return false;
        }
        public bool TitleInSeenList(string imdbId)
        {
            if (Properties.Settings.Default.SeenList.Contains(imdbId))
                return true;
            return false;
        }
        public int TitlesWished()
        {
            return Properties.Settings.Default.WishList.Count;
        }
        private void ThreadBW_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MoviesFLP.Invoke(new MethodInvoker(delegate
            {
                MoviesFLP.Controls.Clear();
            }));

            string url = string.Empty;

            if (MultipleCb.Checked)
            {
                url = OMDB.multipleAddress + SearchTb.Text.Trim() + "&apikey=" + OMDB.api;
            }
            else
            {
                url = OMDB.singleAddress + SearchTb.Text.Trim() + "&apikey=" + OMDB.api;
            }

            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(url);
                List<string> imdbId = new List<string>();

                JavaScriptSerializer oJS = new JavaScriptSerializer();

                if (MultipleCb.Checked)
                {
                    Entity_M = oJS.Deserialize<ImdbEntity_M>(json);
                    var movies = oJS.Serialize(Entity_M);

                    if (Entity_M.Response == "True")
                    {
                        foreach (var movie in Entity_M.Search)
                        {
                            MovieUC MovieUserControl = new MovieUC(movie.imdbID, movie.Title, movie.Year, movie.Poster,
                                movie.Type, TitleInWishList(movie.imdbID), TitleInSeenList(movie.imdbID), this);

                            imdbId.Add(movie.imdbID);

                            MoviesFLP.Invoke(new MethodInvoker(delegate
                            {
                                MoviesFLP.Controls.Add(MovieUserControl);
                            }));
                        }
                    }
                    else
                    {
                        MessageBox.Show("There was an error requesting movie data", "Error");
                    }
                }
                else
                {
                    Entity_S = oJS.Deserialize<ImdbEntity_S>(json);

                    if (Entity_S.Response == "True")
                    {
                        MovieUC MovieUserControl = new MovieUC(Entity_M.imdbID, Entity_S.Title, Entity_S.Year, Entity_S.Poster,
                            Entity_S.Type, TitleInWishList(Entity_S.imdbID), TitleInSeenList(Entity_S.imdbID), this);

                        imdbId.Add(Entity_M.imdbID);

                        MoviesFLP.Invoke(new MethodInvoker(delegate
                        {
                            MoviesFLP.Controls.Add(MovieUserControl);
                        }));
                    }
                    else
                    {
                        MessageBox.Show("There was an error requesting movie data", "Error");
                    }
                }
                SeenListUserControl = new SeenListUC(NumberOfMoviesSeenInSearchCategory(imdbId), this);
                MoviesFLP.Invoke(new MethodInvoker(delegate
                {
                    MoviesFLP.Controls.Add(SeenListUserControl);
                }));

                WishListUserControl = new WishListUC(Properties.Settings.Default.WishList.Count, this);
                MoviesFLP.Invoke(new MethodInvoker(delegate
                {
                    MoviesFLP.Controls.Add(WishListUserControl);
                }));
            }
        }
        private void ThreadBW_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.LoadPb.Visible = false;
            this.MoviesFLP.Visible = true;
        }
        public void UpdateResultResult()
        {
            this.LoadPb.Visible = true;
            this.MoviesFLP.Visible = false;
            this.ThreadBW.RunWorkerAsync();
        }
        public int NumberOfMoviesSeenInSearchCategory(List<string> imdbId)
        {
            int movieMatched = 0;

            for (int t = 0; t < imdbId.Count; t++)
            {
                for (int i = 0; i < Properties.Settings.Default.SeenList.Count; i++)
                {
                    if (Properties.Settings.Default.SeenList[i] == imdbId[t])
                    {
                        movieMatched++;
                    }
                }
            }
            return movieMatched;
        }
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (SearchTb.Text == "!Log")
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\ChangeLog.txt");
                return;
            }

            if (SearchTb.Text == string.Empty || SearchTb.Text == "Search for movie title...")
                MessageBox.Show("Invalid search input", "Error");
            else
                UpdateResultResult();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            SeenListUserControl = new SeenListUC(0, this);
            MoviesFLP.Controls.Add(SeenListUserControl);
            WishListUserControl = new WishListUC(Properties.Settings.Default.WishList.Count, this);
            MoviesFLP.Controls.Add(WishListUserControl);
        }
    }
}
