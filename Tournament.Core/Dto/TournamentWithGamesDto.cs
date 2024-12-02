﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dto
{
    public record TournamentWithGamesDto : TournamentDto
    {
        public IEnumerable<GameDto> Games { get; init; } = Enumerable.Empty<GameDto>();
    }
}
