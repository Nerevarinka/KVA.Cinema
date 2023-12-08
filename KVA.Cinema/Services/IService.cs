namespace KVA.Cinema.Services
{
    using System;
    using System.Collections.Generic;

    public interface IService<TEntityNecessaryData, TEntityDisplayedData>
        where TEntityNecessaryData : struct
        where TEntityDisplayedData : struct
    {
        IEnumerable<TEntityDisplayedData> ReadAll();

        void Create(TEntityNecessaryData entityData);

        void Update(Guid id, TEntityNecessaryData newEntityData);

        void Delete(Guid id);

        bool IsEntityExist(string name);
    }
}
