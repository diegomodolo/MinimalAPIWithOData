using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace MinimalAPIWithOData.Model;

public static class EdmModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
            
        builder.EntitySet<Customer>("Customers");
        builder.EntitySet<Order>("Orders");
        builder.ComplexType<Address>();

        return builder.GetEdmModel();
    }
}