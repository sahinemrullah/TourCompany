using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Interceptors
{
    public class InvoiceInsertInterceptor : DbCommandInterceptor
    {
        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {

            #region Not
            // Hocam buranın yanlış olduğunu biliyorum da çözemedim.
            #endregion
            if (command.CommandText.Contains("INSERT INTO [Invoices]"))
            {
                var val = (from i in eventData.Context!.Set<Invoice>()
                           where i.Date == DateTime.Today
                           group i.InvoiceID by i.Date into g
                           select new { Diff = g.Max() - g.Min() })
                           .FirstOrDefault();
                if (val == null)
                    command.Parameters["@p2"].Value = $"FTR{DateTime.Today:yyyyMMdd}001";
                else
                    command.Parameters["@p2"].Value = $"FTR{DateTime.Today:yyyyMMdd}{val.Diff + 2:000}";
            }
            return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        }
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            if (command.CommandText.Contains("INSERT INTO [Invoices]"))
            {
                var val = (from i in eventData.Context!.Set<Invoice>()
                           where i.Date == DateTime.Today
                           group i.InvoiceID by i.Date into g
                           select new { Diff = g.Max() - g.Min() })
                           .FirstOrDefault();
                if (val == null)
                    command.Parameters["@p2"].Value = $"FTR{DateTime.Today:yyyyMMdd}001";
                else
                    command.Parameters["@p2"].Value = $"FTR{DateTime.Today:yyyyMMdd}{val.Diff + 2:000}";
            }
            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
