using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsAllVehicles
{
    IEnumerable<Vehicle> GetAllVehicles();
}
