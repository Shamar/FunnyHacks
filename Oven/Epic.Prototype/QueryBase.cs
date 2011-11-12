using System;

namespace Epic
{
    [Serializable]
    public abstract class QueryBase<TEntity, TValue>
    {
        public abstract bool IsTarget(TEntity entity);
        
        public abstract TValue Query(TEntity entity);
    }
}

