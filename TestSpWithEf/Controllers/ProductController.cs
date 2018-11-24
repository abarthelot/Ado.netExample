using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSpWithEf.Models;

namespace TestSpWithEf.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<Product> lstproduct = new List<Product>();
            using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=MvcCrudDB;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("spGetAllProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Price = Convert.ToDecimal(rdr["Price"].ToString());
                    product.Quantity = Convert.ToInt32(rdr["Quantity"].ToString());
                    lstproduct.Add(product);
                }
                con.Close();
            }
            return View();
        }
    }
}