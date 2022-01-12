using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace utils_lib.ControllersUtils
{
    public interface IController<in TKey, TViewModel, in TViewEdit>
    {
        ActionResult<IQueryable<TViewModel>> Index();
        ActionResult<TViewModel> Create(TViewModel viewModel);
        ActionResult<IList<TViewModel>> Create(IList<TViewModel> viewModel);
        ActionResult<TViewModel> Edit(TKey id, TViewEdit viewEditModel);
        ActionResult<TViewModel> Delete(TKey id);
    }
}
