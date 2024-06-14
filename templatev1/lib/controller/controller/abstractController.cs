using MySqlConnector;

namespace controller
{
    public abstract class abstractController //abstract class for controller
    {
        protected static readonly string connString = Database.GetConnectionString();
        protected MySqlDataAdapter adr;

        protected Database _db;
    }
}