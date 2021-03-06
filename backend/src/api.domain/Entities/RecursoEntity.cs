﻿using api.domain.Entities.Standard;
using System.Collections.Generic;

namespace api.domain.Entities
{
    public class RecursoEntity : BaseEntity
    {
        public string Tarefa { get; set; }
        public virtual IEnumerable<VotoEntity> Votos { get; set; }
    }
}
