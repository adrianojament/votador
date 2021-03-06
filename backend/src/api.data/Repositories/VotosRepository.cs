﻿using api.data.Context;
using api.data.Repositories.Standard;
using api.domain.Entities;
using api.domain.Interfaces.Repositories;

namespace api.data.Repositories
{
    public class VotosRepository : BaseRepository<VotoEntity>, IVotosRepository
    {
        public VotosRepository(VotosContext context) : base(context)
        {           
            
            _dataset = context.Set<VotoEntity>();
        }
    }
}
