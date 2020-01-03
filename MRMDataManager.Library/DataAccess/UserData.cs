using MRMDataManager.Library.Internal.DataAccess;
using MRMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 13
namespace MRMDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();
            // 13 Create anonymus type to pass into LoadData, may work in same assembly, will not across different assemblies
            // 13 Instead of passing in concrete type cast string type id to anonymus type with value of id
            var p = new { Id = Id };
            // 13 Return type of UserModel, pass in an anonymus type (dynamic)
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "TRMData");
            return output;
        }
    }
}
