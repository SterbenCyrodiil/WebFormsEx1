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
                refreshGridview();
            }
        }


        protected void SubmitArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;
            double price = 0;
            if (!double.TryParse(txtPrice.Text, out price))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Insert a valid number please", true);
            }
            else
            {
                if (name == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Insert a name please", true);
                }
                else
                {
                    InsertArticle(name, price);
                    refreshGridview();
                    clearText(); 
                }
            }

        }

        protected void UpdateArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;
            double price;
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
                    refreshGridview();
                    clearText();
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
                refreshGridview();
                clearText(); 
            }
        }

        protected void DDL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), 
            //    "alertMessage", "alert('"+DDL1.SelectedValue+"')", true);
            Article article = getArticleListItem(DDL1.SelectedValue);
            if (article != null)
            {
                txtName.Text = article.Name;
                txtPrice.Text = article.Price.ToString(); 
            }
        }

        private List<Article> GetArticlesAsList()
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                return db.Article.ToList();
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
                Article check = GetArticle(name);
                if (check != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not update, not found')", true);ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Could not insert, does not exist')", true);
                }
                else
                {
                    Article article = new Article() { Name = name, Price = price };
                    db.Article.Add(article);
                    db.SaveChanges();
                }
            }
        }

        private void UpdatePrice(string name, double price)
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                Article articleUp = db.Article.Where(x => x.Name.Equals(name)).FirstOrDefault();
                if (articleUp == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already exists')", true);
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

        private Article getArticleListItem(string name)
        {
            return GetArticle(name);
        }

        private void refreshGridview()
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

        private void clearText()
        {
            txtName.Text = "";
            txtPrice.Text = "";
        }

        
    }
}