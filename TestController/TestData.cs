using System.Collections.Generic;

public class User
{
    public string Id { get; set; }
    public string Password { get; set; }
}

public class TestData
{
    public List<User> Customers { get; set; } = new List<User>
    {
        new User { Id = "LMS00001", Password = "password123" },
        new User { Id = "LMS00002", Password = "1234567890" },
        new User { Id = "LMC00003", Password = "5432154321" },
        new User { Id = "LMC00004", Password = "0000000000" }
    };

    public List<User> OrderProcessingClerks { get; set; } = new List<User>
    {
        new User { Id = "LMS00008", Password = "asdfghjkl0" },
        new User { Id = "LMS00007", Password = "1234567890" },
        new User { Id = "LMS00006", Password = "1234567890" },
        new User { Id = "LMS00002", Password = "0987654321" },
        new User { Id = "LMS00003", Password = "xyz789!@#" },
        new User { Id = "LMS00001", Password = "password123" },
        new User { Id = "LMS00004", Password = "qwer5678" },
        new User { Id = "LMS00005", Password = "1234567890" }
    };

    public List<User> Deliverymen { get; set; } = new List<User>
    {
        new User { Id = "LMS00009", Password = "1234567890" },
        new User { Id = "LMS00010", Password = "0987654321" },
        new User { Id = "LMS00011", Password = "qwertyuiop" }
    };
    
}