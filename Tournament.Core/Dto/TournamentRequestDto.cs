﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dto
{
    public record TournamentRequestDto
    {
        [Required(ErrorMessage = "Tournament Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for Title is 60 characters.")]
        public string? Title { get; init; }

        public DateTime StartDate { get; init; }
    }
}
