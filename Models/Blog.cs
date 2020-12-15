﻿using Brief.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Brief.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public BriefUser Creator { get; set; }
        public string CreatorID { get; set; }
        public string CreatorName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        //public string Comments { get; set; }
        public DateTime TimeCreated { get; set; }

        public int SaveDetails()
        {
            SqlConnection con = new SqlConnection(GetConString.ConString());
            string query = "INSERT INTO Blogs(Title, Content, CreatorName, TimeCreated) values ('" + Title + "','" + Content + "','" + CreatorName + "','" + TimeCreated + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}
