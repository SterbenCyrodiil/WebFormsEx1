using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsEx1.Entities;

namespace WebFormsEx1
{
    /// <summary>
    /// The form to be used 
    /// </summary>
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// Page_S the load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RefreshGridview();
            }
        }

        /// <summary>
        /// This method is responsible for receiving, processing and inserting information from the form into the DB.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        protected void SubmitArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;
            double price = 0;
            if (!double.TryParse(txtPrice.Text, out price))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(Insert a valid number please)", true);
            }
            else
            {
                if (name == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(Insert a name please)", true);
                }
                else
                {
                    InsertArticle(name, price);
                    RefreshGridview();
                    ClearText(); 
                }
            }

        }

        /// <summary>
        /// Updates the article.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        protected void UpdateArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;
            double price;
            if (!double.TryParse(txtPrice.Text, out price))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(Insert a valid number please)", true);
            }
            else
            {
                if (name == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(Insert a name please)", true);
                }
                else
                {
                    UpdatePrice(name, price);
                    RefreshGridview();
                    ClearText();
                }
            }
        }

        /// <summary>
        /// Searches the article.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        protected void SearchArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;

            if (name == null)
            {

            }
            else
            {
                Article article = GetArticle(name);
                if (article == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Write the name')", true);
                }
                else
                {
                    txtName.Text = name;
                    txtPrice.Text = article.Price.ToString();
                }
            }

        }
        /// <summary>
        /// Deletes the article.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        protected void DeleteArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;

            if (name == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Write the name')", true);
            }
            else
            {
                DeleteIntro(name);
                RefreshGridview();
                ClearText(); 
            }
        }

        /// <summary>
        /// When the Index changes in the Drop Drown List, places the information of said article in the boxes
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        protected void DDL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Article article = GetArticle(DDL1.SelectedValue);
            if (article != null)
            {
                txtName.Text = article.Name;
                txtPrice.Text = article.Price.ToString(); 
            }
        }

        /// <summary>
        /// Gets the articles as list, from the DB, using Linq
        /// </summary>
        /// <returns>A list of Articles.</returns>
        private List<Article> GetArticlesAsList()
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                return db.Article.ToList();
            }
        }

        /// <summary>
        /// Gets the articles as queryable, from the DB, using Linq
        /// </summary>
        /// <returns>An IQueryable with the Articles</returns>
        private IQueryable<Article> GetArticlesAsQueryable()
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                IQueryable<Article> articles = db.Article;
                return articles;
            }

        }

        /// <summary>
        /// Gets an article searching by its unique identifier,from the DB, using Linq
        /// </summary>
        /// <param name="name">Name of the article</param>
        /// <returns>The article which is being searched</returns>
        private Article GetArticle(string name)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                return db.Article.Where(x => x.Name.Equals(name)).FirstOrDefault();
            }

        }

        /// <summary>
        /// Inserts a new article into the DB, using Linq.
        /// </summary>
        /// <param name="name">The name of the article</param>
        /// <param name="price">The price of the article</param>
        private void InsertArticle(string name, double price)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                Article check = GetArticle(name);
                if (check != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Article with the same name already exists')", true);ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not insert, does not exist')", true);
                }
                else
                {
                    Article article = new Article() { Name = name, Price = price };
                    db.Article.Add(article);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Updates the price of an article
        /// </summary>
        /// <param name="name">The name of the article to be updated</param>
        /// <param name="price">The new price of the article</param>
        private void UpdatePrice(string name, double price)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                Article articleUp = db.Article.Where(x => x.Name.Equals(name)).FirstOrDefault();
                if (articleUp == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Doesn't exist!')", true);
                }
                else
                {
                    articleUp.Price = price;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Deletes an article from the DB, using Linq.
        /// </summary>
        /// <param name="name">The name of the article</param>
        private void DeleteIntro(string name)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                Article articleD = db.Article.Where(x => x.Name.Equals(name)).FirstOrDefault();
                if (articleD == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not delete, not found')", true);
                }
                else
                {
                    db.Article.Remove(articleD);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Refreshes the gridview and the Dropdown list
        /// </summary>
        private void RefreshGridview()
        {
            List<Article> articles = GetArticlesAsList();
            GridView1.DataSource = articles;
            GridView1.DataBind();
            List<string> articleNames = new List<string>();
            foreach (Article article in articles)
                articleNames.Add(article.Name);
            DDL1.DataSource = articleNames;
            DDL1.DataBind();

        }

        /// <summary>
        /// Clears the text from the text boxes
        /// </summary>
        private void ClearText()
        {
            txtName.Text = "";
            txtPrice.Text = "";
        }

        
    }
}