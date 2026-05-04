using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsActiveSeasonalStorages
{
    IEnumerable<SeasonalStorage> GetActiveStorages();
}