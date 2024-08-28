using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Commons.Extensions.Attributes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Commons.Identity
{
    public abstract class CommonsDbContext<TUser>
        : IdentityDbContext<TUser, CommonsRole, string>
        , ICommonsDbContext
        where TUser : CommonsUser
    {
        private DbContextOptions _options;

        public CommonsDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<CommonsFunction> AspNetFunctions { get; set; }
        public DbSet<CommonsRoleFunction> AspNetRoleFunctions { get; set; }
        public DbSet<CommonsRole> AspNetRoles { get; set; }


        /// <summary>
        /// Return a list with all workspaces for the app.
        /// Example: Institutes, Cuarters...
        /// Note: Return empty list if you are not using WorkSpaces.
        /// </summary>
        /// <returns></returns>
        public abstract List<IWorkSpace> GetIWorkSpaces();
        public virtual Task SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Iterate through all EF Entity types
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                #region Convert UniqueKeyAttribute on Entities to UniqueKey in DB
                var properties = entityType.GetProperties().ToList();
                if (properties.Count != 0)
                {
                    foreach (var property in properties)
                    {
                        var uniqueKeys = GetUniqueKeyAttributes(entityType, property);
                        if (uniqueKeys != null)
                        {
                            foreach (var uniqueKey in uniqueKeys.Where(x => x.Order == 0))
                            {
                                // Single column Unique Key
                                if (String.IsNullOrWhiteSpace(uniqueKey.GroupId))
                                {
                                    entityType.AddIndex(property).IsUnique = true;
                                }
                                // Multiple column Unique Key
                                else
                                {
                                    var mutableProperties = new List<IMutableProperty>();
                                    properties.ToList().ForEach(x =>
                                    {
                                        var uks = GetUniqueKeyAttributes(entityType, x);
                                        if (uks != null)
                                        {
                                            foreach (var uk in uks)
                                            {
                                                if ((uk != null) && (uk.GroupId == uniqueKey.GroupId))
                                                {
                                                    mutableProperties.Add(x);
                                                }
                                            }
                                        }
                                    });
                                    entityType.AddIndex(mutableProperties).IsUnique = true;
                                }
                            }
                        }
                    }
                }
                #endregion Convert UniqueKeyAttribute on Entities to UniqueKey in DB
            }

            base.OnModelCreating(modelBuilder);

        }

        private static IEnumerable<UniqueAttribute> GetUniqueKeyAttributes(IMutableEntityType entityType, IMutableProperty property)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException(nameof(entityType));
            }
            else if (entityType.ClrType == null)
            {
                throw new ArgumentNullException(nameof(entityType.ClrType));
            }
            else if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            else if (property.Name == null)
            {
                throw new ArgumentNullException(nameof(property.Name));
            }
            var propInfo = entityType.ClrType.GetProperty(
                property.Name,
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly);
            if (propInfo == null)
            {
                return null;
            }
            return propInfo.GetCustomAttributes<UniqueAttribute>();
        }
    }


}
