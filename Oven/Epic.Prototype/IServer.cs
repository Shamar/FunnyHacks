using System;

namespace Epic
{
    public interface IServer
    {
        TValue Read<TEntity, TValue>(QueryBase<TEntity, TValue> query);
        
        Exception Execute<TEntity>(CommandBase<TEntity> entity);
    }
}

