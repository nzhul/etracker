using App.Data.Repositories;
using App.Models;
using App.Models.Employees;
using App.Models.Roles;
using App.Models.Teams;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace App.Data
{
	public class UoWData : IUoWData
	{
		private DbContext context;
		private IDictionary<Type, object> repositories;

		public UoWData()
			: this(new ApplicationDbContext())
		{
		}

		public UoWData(DbContext context)
		{
			this.Context = context;
			this.repositories = new Dictionary<Type, object>();
		}

		public DbContext Context
		{
			get
			{
				return this.context;
			} 
			private set
			{
				this.context = value;
			}
		}

		public IRepository<ApplicationUser> Users
		{
			get { return this.GetRepository<ApplicationUser>(); }
		}

		public IRepository<Employee> Employees
		{
			get
			{
				return this.GetRepository<Employee>();
			}
		}

		public IRepository<Report> Reports
		{
			get
			{
				return this.GetRepository<Report>();
			}
		}

		public IRepository<Team> Teams
		{
			get
			{
				return this.GetRepository<Team>();
			}
		}

		public IRepository<CompanyRole> Roles
		{
			get
			{
				return this.GetRepository<CompanyRole>();
			}
		}

		public IRepository<ReportingToken> Tokens
		{
			get
			{
				return this.GetRepository<ReportingToken>();
			}
		}

		public int SaveChanges()
		{
			return this.context.SaveChanges();
		}

		private IRepository<T> GetRepository<T>() where T : class
		{
			var typeOfRepository = typeof(T);
			if (!this.repositories.ContainsKey(typeOfRepository))
			{
				var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
				this.repositories.Add(typeOfRepository, newRepository);
			}

			return (IRepository<T>)this.repositories[typeOfRepository];
		}
	}
}