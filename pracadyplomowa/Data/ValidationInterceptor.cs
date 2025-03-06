using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using pracadyplomowa.Models.Entities.Powers;

namespace pracadyplomowa.Data
{
    public class ValidationInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context != null)
            {
                var changedEntities = context.ChangeTracker
                    .Entries()
                    .Where(_ => _.State == EntityState.Added || 
                                _.State == EntityState.Modified);

                var errors = new List<ValidationResult>(); // all errors are here
                foreach (var e in changedEntities)
                {
                    var vc = new ValidationContext(e.Entity, null, null);
                    Validator.TryValidateObject(
                        e.Entity, vc, errors, validateAllProperties: true);
                }
                if(errors.Count > 0) throw new ValidationException(string.Join("; ", errors.Select(er => er.ErrorMessage).ToList()));
            }

            await base.SavingChangesAsync(eventData, result, cancellationToken);
            return result;
        }
    }
}