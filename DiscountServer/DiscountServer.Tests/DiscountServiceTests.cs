using Xunit;

public class DiscountServiceTests
{
    [Fact]
    public void GenerateCodes_ShouldReturnCorrectCount()
    {
        var storage = new DiscountStorage();
        var service = new DiscountService(storage);
        var codes = service.GenerateCodes(10, 8);
        Assert.Equal(10, codes.Count);
        Assert.All(codes, code => Assert.Equal(8, code.Length));
    }

    [Fact]
    public void UseCode_ShouldMarkCodeAsUsed()
    {
        var storage = new DiscountStorage();
        var service = new DiscountService(storage);
        var code = service.GenerateCodes(1, 8).First();
        var result = service.UseCode(code);
        Assert.Equal(0, result);
        Assert.Equal(2, service.UseCode(code));
    }
}
