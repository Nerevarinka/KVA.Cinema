namespace KVA.Cinema.Services
{
    using System;
    using System.Collections.Generic;

    internal interface IService<TEntityCreateViewModel, TEntityDisplayViewModel>
        where TEntityCreateViewModel : class
        where TEntityDisplayViewModel : class
    {
        IEnumerable<TEntityDisplayViewModel> ReadAll();

        void Create(TEntityCreateViewModel entityData);

        void Update(Guid id, TEntityCreateViewModel newEntityData);

        void Delete(Guid id);

        bool IsEntityExist(string name);
    }
}
