namespace controller
{
    public abstract class abstractController //abstract class for controller
    {
        protected static readonly string connString = Database.GetConnectionString();

        protected Database _db;
    }
}