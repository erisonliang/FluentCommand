using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FluentCommand
{
    /// <summary>
    /// An <see langword="interface"/> defining a data command.
    /// </summary>
    public interface IDataCommand : IDataQuery
    {
        /// <summary>
        /// Set the data command with the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL statement.</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to a data command.
        /// </returns>
        IDataCommand Sql(string sql);

        /// <summary>
        /// Set the data command with the specified stored procedure name.
        /// </summary>
        /// <param name="storedProcedure">Name of the stored procedure.</param>
        /// <returns>
        /// A fluent <see langword="interface" /> to a data command.
        /// </returns>
        IDataCommand StoredProcedure(string storedProcedure);


        /// <summary>
        /// Sets the wait time before terminating the attempt to execute a command and generating an error.
        /// </summary>
        /// <param name="timeout">TThe time in seconds to wait for the command to execute.</param>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand CommandTimeout(int timeout);


        /// <summary>
        /// Adds the parameters to the underlying command.
        /// </summary>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand Parameter(IEnumerable<DbParameter> parameters);

        /// <summary>
        /// Adds the parameter to the underlying command.
        /// </summary>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand Parameter(DbParameter parameter);

        /// <summary>
        /// Adds a new parameter with the <see cref="IDataParameter"/> fluent object.
        /// </summary>
        /// <param name="configurator">The <see langword="delegate"/>  to configurator the parameter.</param>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand Parameter<TParameter>(Action<IDataParameter<TParameter>> configurator);

        /// <summary>
        /// Adds a new parameter with the specified <paramref name="name"/> and <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter value.</typeparam>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value to be added.</param>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand Parameter<TParameter>(string name, TParameter value);

        /// <summary>
        /// Adds a new out parameter with the specified <paramref name="name" /> and <paramref name="callback" />.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter value.</typeparam>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="callback">The callback used to get the out value.</param>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand ParameterOut<TParameter>(string name, Action<TParameter> callback);

        /// <summary>
        /// Adds a new out parameter with the specified <paramref name="name" />, <paramref name="value" /> and <paramref name="callback" />.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter value.</typeparam>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value to be added.</param>
        /// <param name="callback">The callback used to get the out value.</param>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand ParameterOut<TParameter>(string name, TParameter value, Action<TParameter> callback);

        /// <summary>
        /// Adds a new return parameter with the specified <paramref name="callback" />.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter value.</typeparam>
        /// <param name="callback">The callback used to get the out value.</param>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand Return<TParameter>(Action<TParameter> callback);

        /// <summary>
        /// Uses <see cref="T:System.Runtime.Caching.MemoryCache"/> to insert and retrieve cached results for the command.
        /// </summary>
        /// <param name="policy">A <see cref="T:System.Runtime.Caching.CacheItemPolicy"/> that contains eviction details for the cache entry..</param>
        /// <returns>A fluent <see langword="interface"/> to the data command.</returns>
        IDataCommand UseCache(System.Runtime.Caching.CacheItemPolicy policy);

        /// <summary>
        /// Expires cached items that have been cached using the current DataCommand.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// A fluent <see langword="interface" /> to the data command.
        /// </returns>
        /// <remarks>
        /// Cached keys are created using the current DataCommand state.  When any Query opertion is
        /// executed with a cache policy, the results are cached.  Use this method with the same parameters
        /// to expire the cached item.
        /// </remarks>
        IDataCommand ExpireCache<TEntity>();

        /// <summary>
        /// Executes the command against the connection and sends the resulting <see cref="IDataQuery"/> for reading multiple results sets.
        /// </summary>
        /// <param name="queryAction">The query action delegate to pass the open <see cref="IDataQuery"/> for reading multiple results.</param>
        void QueryMultiple(Action<IDataQuery> queryAction);

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>The value of the first column of the first row in the result set.</returns>
        TValue QueryValue<TValue>();

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="convert">The <see langword="delegate"/> to convert the value..</param>
        /// <returns>
        /// The value of the first column of the first row in the result set.
        /// </returns>
        TValue QueryValue<TValue>(Func<object, TValue> convert);

        /// <summary>
        /// Executes the command against a connection.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        int Execute();

        /// <summary>
        /// Executes the command against the connection and sends the resulting <see cref="IDataReader"/> to the readAction delegate.
        /// </summary>
        /// <param name="readAction">The read action delegate to pass the open <see cref="IDataReader"/>.</param>
        void Read(Action<IDataReader> readAction);
    }
}