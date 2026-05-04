using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsCustomerSeasonalStorages
{
    IEnumerable<SeasonalStorage> GetCustomerStorages(Guid customerId);
}