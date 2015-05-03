using System;
using NHibernate;

namespace WindowsTasksPlanning.Infrastructure.Impl
{
    public class DataBindingIntercepter : EmptyInterceptor
	{
		public ISessionFactory SessionFactory { set; get; }

        /// <summary>
        /// create a new instance of a class based on it's name
        /// </summary>
        /// <param name="className">The name of class to instantiate</param>
        /// <param name="entityMode">The entity mode to be used to instantiate the class</param>
        /// <param name="id">The id of the created instance</param>
        /// <returns>the created instance</returns>
		public override object Instantiate(string className, EntityMode entityMode, object id)
		{
		    switch (entityMode)
		    {
		        case EntityMode.Poco:
		        {
		            var type = Type.GetType(className);
		            if (type != null)
		            {
		                var instance= DataBindingFactory.Create(type);
		                SessionFactory.GetClassMetadata(className).SetIdentifier(instance,id, entityMode);
		                return instance;
		            }
		        }
		            break;
		    }
		    return base.Instantiate(className, entityMode, id);
		}

        public override string GetEntityName(object entity)
		{
			var markerInterface = entity as DataBindingFactory.IMarkerInterface;
			return markerInterface != null ? markerInterface.TypeName : base.GetEntityName(entity);
		}
	}
}