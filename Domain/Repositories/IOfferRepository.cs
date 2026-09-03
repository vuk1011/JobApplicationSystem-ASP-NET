using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<Offer> GetByJobApplicationId(long jobApplicationId);
        Offer? GetByIdWithJobApplication(long id);
    }
}
