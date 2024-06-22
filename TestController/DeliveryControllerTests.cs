using Xunit;
using Moq;
using System.Collections.Generic;
using controller;
using controller.Utilities;

public class DeliveryControllerTests
{
    private Mock<Database> _mockDatabase;
    private DeliveryController _controller;

    public DeliveryControllerTests()
    {
        _mockDatabase = new Mock<Database>();
        _controller = new DeliveryController();
    }
}