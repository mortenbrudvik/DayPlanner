using System;

namespace ApplicationCore.Entities
{
    public interface ICreatedAndUpdated
    {
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
    }
}