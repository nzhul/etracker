using App.Models;
using App.Models.Employees;
using App.Models.InputModels;
using App.Web.Models;
using AutoMapper;
using System.Linq;

namespace App.Web.Infrastructure.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<ApplicationUser, EditClientInputModel>().ForMember(x => x.Phone, opt => opt.MapFrom(u => u.PhoneNumber));

			CreateMap<EditClientInputModel, ApplicationUser>()
				.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(u => u.Phone))
				.ForMember(x => x.ProfileImage, opt => opt.Ignore());

			CreateMap<Report, ReportViewModel>()
				.ForMember(x => x.EmployeeName, opt => opt.MapFrom(u => u.Employee.Name + " " + u.Employee.SurName))
				.ForMember(x => x.EmployeeTeams, opt => opt.MapFrom(u => string.Join(", ", u.Employee.Teams.Select(t => t.Name))))
				.ForMember(x => x.Role, opt => opt.MapFrom(u => u.Employee.Role.Name));

			CreateMap<Employee, EmployeeViewModel>()
				.ForMember(x => x.ManagerName, opt => opt.MapFrom(u => u.Manager.Name))
				.ForMember(x => x.ReportsCount, opt => opt.MapFrom(u => u.Reports.Count))
				.ForMember(x => x.Teams, opt => opt.MapFrom(u => string.Join(", ", u.Teams.Select(t => t.Name))))
				.ForMember(x => x.RoleName, opt => opt.MapFrom(u => u.Role.Name));
		}
	}
}