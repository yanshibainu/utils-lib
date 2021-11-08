using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace utils_lib.Controllers
{
    public interface IController<TEntity, TView, TViewEdit>
          where TEntity : class
    {
        ActionResult<IQueryable<TView>> Index();
        ActionResult<TView> Create(TView viewModel);
        ActionResult<TView> Edit(Guid id, TViewEdit viewEditModel);
        ActionResult<TEntity> Delete(Guid id);
    }
}
