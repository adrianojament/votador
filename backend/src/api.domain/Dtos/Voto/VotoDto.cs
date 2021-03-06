﻿using api.domain.Dtos.Recurso;
using api.domain.Dtos.Usuario;
using System;

namespace api.domain.Dtos.Voto
{
    public class VotoDto
    {
        public Guid Id { get; set; }
        public Guid RecursoId { get; set; }
        public RecursoDtoVoto Recurso { get; set; }
        public Guid UsuarioId { get; set; }
        public string Comentario { get; set; }
        public UsuarioDtoVoto Usuario { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AtualizadoEm { get; set; }
    }
}
