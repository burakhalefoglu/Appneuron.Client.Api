using Core.Entities;
using Entities.Dtos;
using System;

namespace Entities.Concrete
{
    public class ChurnClientPredictionResult: DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public DateTime ChurnPredictionDate { get; set; }
        public ClientsOfferModelDto[] ClientsOfferModelDto { get; set; }
        public bool Status = true;
    }
}
