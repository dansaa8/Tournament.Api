using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Game, GameUpdateDto>().ReverseMap();
            CreateMap<Game, GameCreateDto>().ReverseMap(); 
            
            CreateMap<TournamentDetails, TournamentDto>();
            CreateMap<TournamentDetails, TournamentWithGamesDto>();
            CreateMap<TournamentDetails, TournamentUpdateDto>().ReverseMap();
            CreateMap<TournamentDetails, TournamentCreateDto>().ReverseMap();   
        }
    }
}
