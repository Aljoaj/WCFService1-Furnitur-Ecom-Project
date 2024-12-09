using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	SqlConnection con = new SqlConnection(@"server=LAPTOP-8DV8N8U0\SQLEXPRESS;database=Projectasp;Integrated security=true");
	public string balance_check(int user, int amnt)
    {
		string sel= "select Balance from Acctab where Usid=" +user;
		SqlCommand cmd = new SqlCommand(sel, con);
		con.Open();
		SqlDataReader dr = cmd.ExecuteReader();
		if(dr.Read())
        {
			int bal = Convert.ToInt32(dr["Balance"]);
			con.Close();
			if(bal>=amnt)
            {
				string upbal = "update Acctab set Balance = Balance-"+amnt+"where Usid="+user+"";
				SqlCommand cmd2 = new SqlCommand(upbal, con);
				con.Open();
				int updis = cmd2.ExecuteNonQuery();
				con.Close();
				if(updis>0)
                {
					return "Success";
                }
				else
                {
					return "Error";
                }
            }
			else
            {
				return "Insufficient Balance";
            }
        }
		con.Close();
		return "User not found";
	}
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}
}
