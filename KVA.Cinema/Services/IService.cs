namespace KVA.Cinema.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal interface IService<TEntityCreateViewModel, TEntityDisplayViewModel>
        where TEntityCreateViewModel : class
        where TEntityDisplayViewModel : class
    {
        IEnumerable<TEntityDisplayViewModel> ReadAll();

        void CreateAsync(TEntityCreateViewModel entityData);

        void Update(Guid id, TEntityCreateViewModel newEntityData);

        void Delete(Guid id);

        bool IsEntityExist(string name);
    }
}
