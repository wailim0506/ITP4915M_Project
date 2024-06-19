namespace controller
{
    public class stockController : abstractController
    {
        AccountController accountController;

        public stockController()
        {
        }

        public stockController(AccountController accountController)
        {
            this.accountController = accountController;
        }
    }
}