using Microsoft.Extensions.DependencyInjection;
using controller.Utils;

namespace controller
{
    public class stockController : abstractController
    {
        AccountController accountController;

        public stockController()
        {
            _db = ServiceProvider.GetRequiredService<Database>();
        }

        public stockController(AccountController accountController)
        {
            this.accountController = accountController;
        }
    }
}