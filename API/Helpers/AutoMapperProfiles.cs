using System;
using AutoMapper;
using API.Entities;
using API.DTOs;
using API.Extensions;

namespace API.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<AppUser, MemberDto>()
				.ForMember(d=>d.Age, o=>o.MapFrom(s=>s.DateofBirth.CalculateAge()))
				.ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));

			CreateMap<Photo, PhotoDto>();
		}
	}
}

