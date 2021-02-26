using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsEx1.Entities;

namespace WebFormsEx1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridView1.DataSource = GetArticlesAsList();
                GridView1.DataBind();
            }
        }


        protected void SubmitArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;
            double price = 0;
            if (!double.TryParse(txtPrice.Text, out price))
            {

            }
            else
            {
                if (name == null)
                {

                }
                else
                {
                    InsertArticle(name, price);
                    GridView1.DataSource = GetArticlesAsList();
                    GridView1.DataBind();
                }
            }

        }

        protected void UpdateArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;
            double price = 0;
            if (!double.TryParse(txtPrice.Text, out price))
            {

            }
            else
            {
                if (name == null)
                {

                }
                else
                {
                    UpdatePrice(name, price);
                    GridView1.DataSource = GetArticlesAsList();
                    GridView1.DataBind();
                }
            }
        }

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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Not found')", true);
                }
                else
                {
                    txtName.Text = name;
                    txtPrice.Text = article.Price.ToString();
                }
            }

        }
        protected void DeleteArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;

            if (name == null)
            {
                
            }
            else
            {
                deleteIntro(name);
                GridView1.DataSource = GetArticlesAsList();
                GridView1.DataBind();
            }

        }

        private List<Article> GetArticlesAsList()
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                return db.Article.Where(x => true).ToList();
            }
        }

        private IQueryable<Article> GetArticlesAsQueryable()
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                IQueryable<Article> articles = db.Article;
                return articles;
            }

        }

        private Article GetArticle(string name)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                return db.Article.Where(x => x.Name.Equals(name)).FirstOrDefault();
            }

        }

        private void InsertArticle(string name, double price)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                Article article = new Article() { Name = name, Price = price };
                db.Article.Add(article);
                db.SaveChanges();
            }
        }

        private void UpdatePrice(string name, double price)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                Article articleUp = db.Article.Where(x => x.Name.Equals(name)).FirstOrDefault();
                if (articleUp == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not update, not found')", true);
                }
                else
                {
                    articleUp.Price = price;
                    db.SaveChanges();
                }
            }
        }

        private void deleteIntro(string name)
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

    }
}