using Pomelo.Data.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.Models;
using System.Text;
using System.Globalization;

namespace Loyalty.Repository
{
    public class MembershipRepository
    {
        //public string _connectionString = "host=localhost;port=3306;database=loyalty;uid=root;pwd=password;";
        //public string _connectionString = "host=209.198.193.73;port=3800;database=loyalty;uid=root;pwd=password;";
        public string _connectionString = "host=us-cdbr-iron-east-04.cleardb.net;port=3306;database=heroku_0aaed3e5ac3fbc1;uid=b2fc2195b8e3a5;pwd=642f1680662180d;";
        public List<Membership> GetMembership(string _userid)
        {
            List<Membership> lstMembership = new List<Membership>();
            Membership _membership;
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select * from membership where userid='" + _userid + "'", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _membership = new Membership();
                    _membership.MembershipID = reader.GetString(0);
                    _membership.UserID = reader.GetString(1);
                    _membership.MemberID = reader.GetString(2);
                    _membership.LoyaltyCardNo = reader.GetString(3);
                    _membership.ExpirationDate = Convert.ToDateTime(reader.GetString(4));
                    _membership.MemberSiteURL = reader.GetString(5);
                    lstMembership.Add(_membership);
                }
                reader.Close();
                return lstMembership;
            }
        }


        public List<Membership> GetMembershipbyID(string _membershipid)
        {
            List<Membership> lstMembership = new List<Membership>();
            Membership _membership;
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select * from membership where membershipid='" + _membershipid + "'", conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _membership = new Membership();
                    _membership.MembershipID = reader.GetString(0);
                    _membership.UserID = reader.GetString(1);
                    _membership.MemberID = reader.GetString(2);
                    _membership.LoyaltyCardNo = reader.GetString(3);
                    _membership.ExpirationDate = Convert.ToDateTime(reader.GetString(4));
                    _membership.MemberSiteURL = reader.GetString(5);
                    lstMembership.Add(_membership);
                }
                reader.Close();
                return lstMembership;
            }
        }


        public void AddMembership(Membership _membership, string userid)
        {
            MySqlConnection conn;
            MySqlCommand cmd;
            using (conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string MembershipID = GetUniqueId();
                _membership.UserID = userid;

                string ExpDate = _membership.ExpirationDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                using (cmd = new MySqlCommand("Insert into membership (membershipid,userid,memberid,loyaltycardno,expirationdate,membersiteurl) values ('" + MembershipID + "','" + _membership.UserID + "','" + _membership.MemberID + "','" + _membership.LoyaltyCardNo + "','" + ExpDate + "', '" + _membership.MemberSiteURL + "')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateMembership(Membership _membership)
        {
            MySqlConnection conn;
            MySqlCommand cmd;
            using (conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string ExpDate = _membership.ExpirationDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                using (cmd = new MySqlCommand("Update membership set memberid='" + _membership.MemberID + "',loyaltycardno='" + _membership.LoyaltyCardNo + "',expirationdate='" + ExpDate + "', membersiteurl='" + _membership.MemberSiteURL + "' where membershipid='" + _membership.MembershipID + "'", conn))
                {
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public string GetUniqueId()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, false));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            //if (lowerCase)
            //    return builder.ToString().ToLower();
            return builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
