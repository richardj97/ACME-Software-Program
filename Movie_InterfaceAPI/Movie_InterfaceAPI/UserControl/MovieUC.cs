using System;
using System.Windows.Forms;
using System.Globalization;

namespace Movie_InterfaceAPI
{
    public partial class MovieUC : UserControl
    {
        private string title;
        private string imdbId;
        private Main Main;

        public MovieUC(string imdbId, string title, string year, string posterUrl, string desc, bool isInWishList, bool isInSeenList, Main Main)
        {
            this.InitializeComponent();
            this.Main = Main;
            this.TitleLb.Text = title + " (" + year + ") " + "\n\nType: " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(desc);
            this.PosterPb.ImageLocation = posterUrl;
            this.title = title;
            this.imdbId = imdbId;

            if (isInWishList)
            {
                WishListBtn.Text = "- Wish List";
            }
            else
            {
                WishListBtn.Text = "+ Wish List";
            }

            if (isInSeenList)
            {
                SeenBtn.Image = Properties.Resources.icons8_eye_23_Copy;
            }
            else
            {
                SeenBtn.Image = Properties.Resources.icons8_eye_23;
            }
        }
        private void MoreInfoLl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MovieInfo MovieInfoFrm = new MovieInfo(imdbId, title);
            MovieInfoFrm.ShowDialog();
        }
        private void WishListBtn_Click(object sender, EventArgs e)
        {
            if (!Main.TitleInWishList(imdbId))
            {
                Main.Entity_S.AddToWishList(imdbId);
                MessageBox.Show(this, title + " has been added to the wish list", "Success");
                WishListBtn.Text = "- Wish List";
            }
            else
            {
                Main.Entity_S.RemoveFromWishList(imdbId);
                MessageBox.Show(this, title + " has been removed to the wish list", "Success");
                WishListBtn.Text = "+ Wish List";
            }
            Main.UpdateResultResult();
        }
        private void SeenBtn_Click(object sender, EventArgs e)
        {
            if (!Main.TitleInSeenList(imdbId))
            {
                Main.Entity_S.AddToSeenList(imdbId);
                MessageBox.Show(this, title + " has been added to the seen list", "Success");
                SeenBtn.Image = Properties.Resources.icons8_eye_23_Copy;
            }
            else
            {
                Main.Entity_S.RemoveFromSeenList(imdbId);
                MessageBox.Show(this, title + " has been removed from the seen list", "Success");
                SeenBtn.Image = Properties.Resources.icons8_eye_23;
            }
            Main.UpdateResultResult();
        }
    }
}
