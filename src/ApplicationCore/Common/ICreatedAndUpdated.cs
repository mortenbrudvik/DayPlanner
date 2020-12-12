using System;

namespace ApplicationCore.Common
{
    public interface ICreatedAndUpdated
    {
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
    }
}