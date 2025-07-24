using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Configuration;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string LogosContainerName { get; set; } = default!;
    public string AccountKey { get; set; } = default!;
    public string AccountName {  get; set; } = default!;
}
