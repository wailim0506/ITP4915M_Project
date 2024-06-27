using System.Windows.Forms;
using LMCIS.System_page;
using templatev1;
using Xunit;
using Xunit.Abstractions;

namespace TestController
{
    public class TestForm
    {
        // test login form
        [Fact]
        public void TestLoginForm()
        {
            Form loginForm = new Login();
            loginForm.Show();
            Assert.True(loginForm.Visible);
        }
    }
}