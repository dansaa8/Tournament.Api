using AutoMapper;
using Services.Contracts;
using Tournament.Core.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Dto.Queries;
using Tournament.Core.Entities;
using Tournament.Core.Exceptions;
using Tournament.Core.Req;

namespace Tournaments.Services;

public class GameService : IGameService
{
    private IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private const int MAX_GAMES_PER_TOURNAMENT = 10;

    public GameService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<GameDto> GetGameByIdAsync(int id)
    {
        Game? game = await _uow.GameRepository.GetGameByIdAsync(id);

        if (game == null)
        {
            throw new NotFoundException($"Game with id {id} was not found.");
        }

        return _mapper.Map<GameDto>(game);
    }

    public async Task<(IEnumerable<GameDto> gameDtos, MetaData metadata)> GetGamesAsync(
        GameQueryParameters queryParams)
    {
        var pagedList = await _uow.GameRepository.GetGamesAsync(queryParams);
        var gameDtos = _mapper.Map<IEnumerable<GameDto>>(pagedList.Items);
        return (gameDtos, pagedList.MetaData);
    }

    public async Task<GameDto> CreateGameAsync(GameCreateDto gameCreateDto, int tournamentId)
    {
        var tournament = await _uow.TournamentRepository.GetTournamentByIdAsync(
            tournamentId, includeGames: true);

        if (tournament == null)
        {
            throw new NotFoundException($"Tournament with id {tournamentId} was not found.");
        }

        if (tournament.Games.Count >= MAX_GAMES_PER_TOURNAMENT)
        {
            throw new InvalidOperationException(
                $"Tournament with id {tournamentId} cannot have more than {MAX_GAMES_PER_TOURNAMENT} games.");
        }

        var newGameEntity = _mapper.Map<Game>(gameCreateDto);
        newGameEntity.TournamentId = tournamentId;
        _uow.GameRepository.Create(newGameEntity);
        
        await _uow.CompleteAsync();
        return _mapper.Map<GameDto>(newGameEntity);
    }
}