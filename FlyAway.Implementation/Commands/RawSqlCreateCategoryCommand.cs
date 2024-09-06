using FlyAway.Application.Commands;
using FlyAway.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyAway.Implementation.Commands
{
    internal class RawSqlCreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly IDbConnection dbConnection;

        public RawSqlCreateCategoryCommand(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public int Id => 51;

        public string Name => "Create Category using Raw SQL";

        public void Execute(CategoryDto request)
        {
            var query = "INSERT INTO CATEGORIES (name) VALUES '" + request.Name + "'";
            var command = dbConnection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
    }
}
