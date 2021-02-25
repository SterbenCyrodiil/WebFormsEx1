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
           
        }

        protected void SubmitArticle(object sender, EventArgs e)
        {
            string name = txtName.Text;
            double price = 0;
            if(double.TryParse(txtPrice.Text, out price)){
                InsertArticle(name, price); 
            }
           
        }

        private List<Article> GetArticles()
        {
            using (AcademiaNETEntities db = new AcademiaNETEntities())
            {
                return  db.Article.Where(x => true).ToList();
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