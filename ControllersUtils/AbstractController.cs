using System.Linq;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using utils_lib.EntitiesUtils;

namespace utils_lib.ControllersUtils
{
    public abstract class AbstractController<TEntity, TKey, TViewModel, TViewEditModel> : Controller,
        IController<TKey, TViewModel, TViewEditModel>
        where TEntity : class
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<TEntity, TKey> Repository;

        protected AbstractController(IRepository<TEntity, TKey> repository, IMapper mapper)
        {
            Mapper = mapper;
            Repository = repository;
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TViewModel>> Index()
        {
            return Ok(Mapper.ProjectTo<TViewModel>(Repository.All()));
        }

        [HttpPost]
        public ActionResult<TViewModel> Create(TViewModel viewModel)
        {
            var entity = Mapper.Map<TEntity>(viewModel);

            Repository.Create(entity);

            return Ok(Mapper.Map<TViewModel>(entity));
        }

        [HttpDelete("{id}")]
        public ActionResult<TViewModel> Delete(TKey id)
        {
            Repository.Delete(id);
            return Ok(Repository.FindById(id));
        }

        [HttpPut("{id}")]
        public ActionResult<TViewModel> Edit(TKey id, TViewEditModel viewEditModel)
        {
            var updateEntity = Mapper.Map<TViewEditModel>(viewEditModel);

            var entity = Repository.Update(id, updateEntity);

            return Ok(Mapper.Map<TViewModel>(entity));
        }
    }
}