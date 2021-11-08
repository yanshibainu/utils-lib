using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using utils_lib.EntitiesUtils;

namespace utils_lib.Controllers
{
    public abstract class AbstractController<TEntity, TView, TViewEdit> : Controller, IController<TEntity, TView, TViewEdit>
        where TEntity : class
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<TEntity, Guid> _repository;
        protected AbstractController(IRepository<TEntity, Guid> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<TView> Create(TView viewModel)
        {
            var entity = _mapper.Map<TEntity>(viewModel);

            var id = _repository.Create(entity);

            return Ok(_mapper.Map<TView>(_repository.FindById(id)));
        }
        [HttpDelete("{id}")]
        public ActionResult<TEntity> Delete(Guid id)
        {
            _repository.Delete(id);
            return Ok(_repository.FindById(id));
        }
        [HttpPut("{id}")]
        public ActionResult<TView> Edit(Guid id, TViewEdit viewEditModel)
        {
            var entity = _mapper.Map<TViewEdit>(viewEditModel);

            _repository.Update(id, entity);

            return Ok(_mapper.Map<TView>(_repository.FindById(id)));
        }
        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<TView>> Index()
        {
            return Ok(_mapper.ProjectTo<TView>(_repository.All()));
        }
    }
}
